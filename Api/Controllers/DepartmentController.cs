using Api.Models;
using Api.Models;
using Application.Exceptions;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers;

[Route("api/v1/departments")]
public class DepartmentController : BaseController
{
    private readonly IRepository<Department> _deptRepo;

    public DepartmentController(IRepository<Department> deptRepo)
    {
        _deptRepo = deptRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepts()
    {
        return Ok(await _deptRepo.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateDept([FromBody] CreateDept req)
    {
        var entity = Mapper.Map(req, new Department());
        await _deptRepo.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }
}
