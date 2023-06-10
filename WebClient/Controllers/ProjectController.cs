using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebClient.Datasource;
using WebClient.Models;
using WebClient.Utils;

namespace WebClient.Controllers;


public class ProjectController : BaseController
{
    public ProjectController(IOptions<AppSettings> appSettings, IApiClient apiClient) : base(appSettings, apiClient)
    {
    }

    [Authorize(Roles = $"{PolicyName.ADMIN},{PolicyName.USER}")]
    public async Task<IActionResult> Index()
    {
        if (!IsAdmin)
        {
            var projs = await ApiClient.GetAsync<List<CompanyProject>>($"{ParticipantUrl}?empId={CurrentUserId}");
            return View(projs);
        }
        var entitites = await ApiClient.GetAsync<List<CompanyProject>>(ProjectUrl);
        return View(entitites);
    }

    [Authorize(Roles = PolicyName.ADMIN)]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [Authorize(Roles = PolicyName.ADMIN)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProject req)
    {
        try
        {
            await ApiClient.PostAsync<object, CreateProject>(ProjectUrl, req);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            TempData["Message"] = "Server Error";
            return RedirectToAction("Create");
        }
    }

    [Authorize(Roles = $"{PolicyName.ADMIN},{PolicyName.USER}")]
    public async Task<IActionResult> Detail(int id)
    {
        var entity = await ApiClient.GetAsync<CompanyProject>($"{ProjectUrl}/{id}");
        return View(entity);
    }

    [Authorize(Roles = PolicyName.ADMIN)]
    public async Task<IActionResult> Update(int id)
    {
        var entity = await ApiClient.GetAsync<CompanyProject>($"{ProjectUrl}/{id}");
        return View(Mapper.Map(entity, new UpdateProject()));
    }

    [Authorize(Roles = PolicyName.ADMIN)]
    [HttpPost]
    public async Task<IActionResult> Update(int id, UpdateProject req)
    {
        try
        {
            await ApiClient.PutAsync<object, UpdateProject>($"{ProjectUrl}/{id}", req);
            return RedirectToAction("Index");
        }
        catch
        {
            TempData["Message"] = "Server Error";
            return RedirectToAction("Update", new { id });
        }
    }

    [Authorize(Roles = PolicyName.ADMIN)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await ApiClient.GetAsync<CompanyProject>($"{ProjectUrl}/{id}");
        return View(entity);
    }

    [Authorize(Roles = PolicyName.ADMIN)]
    [HttpPost]
    public async Task<IActionResult> DeleteProj(int id)
    {
        try
        {
            await ApiClient.DeleteAsync<object>($"{ProjectUrl}/{id}");
            return RedirectToAction("Index");
        }
        catch
        {
            TempData["Message"] = "Flower has ordered, cannot delete";
            return RedirectToAction("Delete", new { id });
        }
    }
}
