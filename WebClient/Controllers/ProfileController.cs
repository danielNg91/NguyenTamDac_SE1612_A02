using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebClient.Datasource;
using WebClient.Models;
using WebClient.Utils;

namespace WebClient.Controllers;

[Authorize(Roles = PolicyName.USER)]
public class ProfileController : BaseController
{
    public ProfileController(IOptions<AppSettings> appSettings, IApiClient apiClient) : base(appSettings, apiClient)
    {
    }

    public async Task<IActionResult> Index()
    {
        var user = await ApiClient.GetAsync<Employee>($"{EmployeeUrl}/{CurrentUserId}");
        return View(user);
    }

    public async Task<IActionResult> Update()
    {
        var user = await ApiClient.GetAsync<Employee>(ProfileUrl);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateEmp req)
    {
        try
        {
            await ApiClient.PutAsync<object, UpdateEmp>($"{EmployeeUrl}/{CurrentUserId}", req);
            return RedirectToAction("Index");
        }
        catch
        {
            TempData["Message"] = "Server Error";
            return RedirectToAction("Update", new { CurrentUserId });
        }
    }
}
