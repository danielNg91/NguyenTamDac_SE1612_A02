using Api.Models;
using Api.Models;
using Application.Exceptions;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers;

[Route("api/v1/participates")]
public class ParticipateController : BaseController
{
    private readonly IRepository<ParticipatingProject> _parRepo;

    public ParticipateController(IRepository<ParticipatingProject> parRepo)
    {
        _parRepo = parRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetPars()
    {
        return Ok(await _parRepo.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePar([FromBody] CreateParticipate req)
    {
        var entity = Mapper.Map(req, new ParticipatingProject());
        await _parRepo.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }
}
