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

    public ProjectController(IRepository<CompanyProject> projRepo)
    {
        _projRepo = projRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        return Ok(await _projRepo.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProject req)
    {
        var entity = Mapper.Map(req, new CompanyProject());
        await _projRepo.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProj(int id)
    {
        var target = await _projRepo.FoundOrThrow(c => c.CompanyProjectID == id, new NotFoundException());
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
}
