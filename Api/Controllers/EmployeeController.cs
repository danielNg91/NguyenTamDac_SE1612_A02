using Api.Models;
using Api.Utils;
using Application.Exceptions;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository;

namespace Api.Controllers;

[Route("api/v1/employees")]
public class EmployeeController : BaseController
{
    private readonly IRepository<Employee> _empRepo;
    private readonly IOptions<AppSettings> _appSettings;

    public EmployeeController(IRepository<Employee> empRepo, IOptions<AppSettings> appSettings)
    {
        _empRepo = empRepo;
        _appSettings = appSettings;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmps()
    {
        return Ok(await _empRepo.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmp([FromBody] CreateEmpl req)
    {
        await ValidateRegisterFields(req);
        Employee entity = Mapper.Map(req, new Employee());
        await _empRepo.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmp(int id)
    {
        var target = await _empRepo.FoundOrThrow(c => c.EmployeeID == id, new NotFoundException());
        return Ok(target);
    }

    private async Task ValidateRegisterFields(CreateEmpl req)
    {
        if (req.EmailAddress.Equals(_appSettings.Value.AdminAccount.EmailAddress))
        {
            throw new BadRequestException("Email already existed");
        }

        var isEmailExisted = (await _empRepo.FirstOrDefaultAsync(u => u.EmailAddress == req.EmailAddress)) != null;
        if (isEmailExisted)
        {
            throw new BadRequestException("Email already existed");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmp(int id, [FromBody] UpdateEmp req)
    {
        var target = await _empRepo.FoundOrThrow(c => c.EmployeeID == id, new NotFoundException());
        Employee entity = Mapper.Map(req, target);
        await _empRepo.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmp(int id)
    {
        var target = await _empRepo.FoundOrThrow(c => c.EmployeeID == id, new NotFoundException());
        await _empRepo.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
