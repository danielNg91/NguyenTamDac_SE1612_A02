using Api.Models;
using Application.Exceptions;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers;

[Route("api/v1/projects")]
public class ProjectController : BaseController
{
    private readonly IRepository<CompanyProject> _projRepo;
    private readonly IRepository<ParticipatingProject> _parRepo;
    private readonly IRepository<Employee> _empRepo;


    public ProjectController(
        IRepository<CompanyProject> projRepo,
        IRepository<ParticipatingProject> parRepo,
        IRepository<Employee> empRepo
    )
    {
        _projRepo = projRepo;
        _parRepo = parRepo;
        _empRepo = empRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        return Ok(await _projRepo.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProject req)
    {
        if (DateTime.Compare(req.EstimatedStartDate, req.ExpectedEndDate) > 0)
        {
            throw new BadRequestException("StartDate cannot be later than EndDate");
        }
        var entity = Mapper.Map(req, new CompanyProject());
        await _projRepo.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProj(int id)
    {
        var target = await _projRepo.FirstOrDefaultAsync(c => c.CompanyProjectID == id, new string[] { "ParticipatingProjects" });
        if (target == null)
        {
            throw new NotFoundException();
        }
        return Ok(target);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProj(int id, [FromBody] UpdateProject req)
    {
        var target = await _projRepo.FoundOrThrow(c => c.CompanyProjectID == id, new NotFoundException());
        var entity = Mapper.Map(req, target);
        await _projRepo.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProj(int id)
    {
        var target = await _projRepo.FoundOrThrow(c => c.CompanyProjectID == id, new NotFoundException());
        await _projRepo.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("{id}/participants")]
    public async Task<IActionResult> GetPars(int id)
    {
        return Ok(await _parRepo.WhereAsync(p => p.CompanyProjectID == id, new string[] { "Employee" }));
    }

    [HttpPost("{id}/participants")]
    public async Task<IActionResult> CreatePar(int id, [FromBody] CreateParticipant req)
    {
        if (DateTime.Compare(req.StartDate, req.EndDate) > 0)
        {
            throw new BadRequestException("StartDate cannot be later than EndDate");
        }

        var emp = await _empRepo.FoundOrThrow(e => e.EmployeeID == req.EmployeeID, new BadRequestException("Employee not exists"));
        var target = await _parRepo.FirstOrDefaultAsync(c => c.CompanyProjectID == id && c.EmployeeID == req.EmployeeID);
        if (target != null)
        {
            throw new BadRequestException("Employee has been added to the project");
        }
        var entity = Mapper.Map(req, new ParticipatingProject());
        entity.CompanyProjectID = id;
        await _parRepo.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }


    [HttpGet("{id}/participants/{empId}")]
    public async Task<IActionResult> GetPar(int id, int empId)
    {
        var target = await _parRepo.FoundOrThrow(c => c.CompanyProjectID == id && c.EmployeeID == empId, new NotFoundException());
        return Ok(target);
    }

    [HttpPut("{id}/participants/{empId}")]
    public async Task<IActionResult> UpdatePar(int id, int empId, [FromBody] UpdateParticipant req)
    {
        var target = await _parRepo.FoundOrThrow(c => c.CompanyProjectID == id && c.EmployeeID == empId, new NotFoundException());
        var entity = Mapper.Map(req, target);
        await _parRepo.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}/participants/{empId}")]
    public async Task<IActionResult> DeletePar(int id, int empId)
    {
        var target = await _parRepo.FoundOrThrow(c => c.CompanyProjectID == id && c.EmployeeID == empId, new NotFoundException());
        await _parRepo.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
