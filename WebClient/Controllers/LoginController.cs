using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Security.Claims;
using WebClient.Datasource;
using WebClient.Models;
using WebClient.Utils;

namespace WebClient.Controllers;


public class LoginController : BaseController
{

    public LoginController(IOptions<AppSettings> appSettings, IApiClient apiClient) : base(appSettings, apiClient)
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCredentials credentials)
    {
        try
        {
            var response = await ApiClient.PostAsync<LoginResponse, LoginCredentials>(LoginUrl, credentials);
            await SetIdentity(response.EmployeeId.ToString(), response.EmailAddress, response.Role);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
            TempData["Message"] = "Incorrect Email or Password";
            return RedirectToAction("Index");
        }
    }

    private async Task SetIdentity(string userId, string email, string role)
    {
        var claims = new List<Claim>
                    {
                        new Claim("id", userId),
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.Role, role)
                    };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = false });
    }
}
