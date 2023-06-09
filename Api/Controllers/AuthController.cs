using Api.Models.Request;
using Api.Models.Response;
using Api.Utils;
using Application.Exceptions;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository;

namespace Api.Controllers;


[Route("api/v1/auth")]
public class AuthController : BaseController
{
    private readonly IRepository<Employee> _empRepo;
    private readonly IOptions<AppSettings> _appSettings;

    public AuthController(IOptions<AppSettings> appSettings, IRepository<Employee> userRepository)
    {
        _appSettings = appSettings;
        _empRepo = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
    {
        LoginResponse resp;
        if (IsAdminLogin(credentials))
        {
            resp = new LoginResponse
            {
                EmployeeId = 999,
                EmailAddress = credentials.EmailAddress,
                Role = PolicyName.ADMIN
            };
            return Ok(resp);
        }

        var user = await this._empRepo.FoundOrThrow(
            u => u.EmailAddress.Equals(credentials.EmailAddress) && u.Password.Equals(credentials.Password),
            new ForbiddenException());
        resp = Mapper.Map(user, new LoginResponse());
        resp.Role = PolicyName.USER;
        return Ok(resp);
    }

    private bool IsAdminLogin(LoginCredentials credentials)
    {
        if (credentials.EmailAddress.Equals(_appSettings.Value.AdminAccount.EmailAddress) &&
            credentials.Password.Equals(_appSettings.Value.AdminAccount.Password))
        {
            return true;
        }
        return false;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var user = await this._empRepo.FoundOrThrow(e => e.EmployeeID == CurrentUserId, new NotFoundException());
        return Ok(user);
    }
}
