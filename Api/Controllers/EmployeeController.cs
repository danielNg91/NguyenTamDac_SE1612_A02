using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository;

namespace Api.Controllers;

[Route("api/v1/customers")]
public class EmployeeController : BaseController
{
    private readonly IRepository<Employee> _empRepo;
    private readonly IOptions<AppSettings> _appSettings;

    public EmployeeController(IRepository<Employee> empRepo, IOptions<AppSettings> appSettings)
    {
        _empRepo = empRepo;
        _appSettings = appSettings;
    }


}
