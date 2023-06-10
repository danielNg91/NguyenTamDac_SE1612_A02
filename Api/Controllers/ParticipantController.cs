using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers;

[Route("api/v1/participants")]
public class ParticipantController : BaseController
{
    private readonly IRepository<ParticipatingProject> _parRepo;
    private readonly IRepository<CompanyProject> _projRepo;


    public ParticipantController(IRepository<ParticipatingProject> parRepo, IRepository<CompanyProject> projRepo)
    {
        _parRepo = parRepo;
        _projRepo = projRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetPars(int? empId)
    {
        if (empId != null)
        {
            var projIds = (await _parRepo.WhereAsync(p => p.EmployeeID == empId)).Select(p => p.CompanyProjectID);
            return Ok(await _projRepo.WhereAsync(p => projIds.Contains(p.CompanyProjectID)));
        };
        return Ok(await _parRepo.ToListAsync());
    }
}
