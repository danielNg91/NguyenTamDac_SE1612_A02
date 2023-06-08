using Api.Models.Request;
using Api.Models.Response;
using Api.Utils;
using Application.Exceptions;
using BusinessObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository;
using System.Security.Claims;

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
        if (credentials.EmailAddress.Equals(_appSettings.Value.AdminAccount.EmailAddress) &&
            credentials.Password.Equals(_appSettings.Value.AdminAccount.Password))
        {
            var admin = new LoginResponse
            {
                EmployeeId = 999,
                EmailAddress = credentials.EmailAddress,
                Role = PolicyName.ADMIN
            };
            return Ok(admin);
        }

        var user = await _empRepo.FoundOrThrow(
            u => u.EmailAddress.Equals(credentials.EmailAddress) && u.Password.Equals(credentials.Password),
            new ForbiddenException());
        var response = Mapper.Map(user, new LoginResponse());
        response.Role = PolicyName.USER;
        return Ok(response);
    }
}
