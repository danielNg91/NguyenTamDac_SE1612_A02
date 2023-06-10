using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebClient.Datasource;
using WebClient.Models;
using WebClient.Utils;

namespace WebClient.Controllers;

[Authorize(Roles = PolicyName.ADMIN)]
public class EmployeeController : BaseController
{
    public EmployeeController(IOptions<AppSettings> appSettings, IApiClient apiClient) : base(appSettings, apiClient)
    {
    }

    public async Task<IActionResult> Index()
    {
        var emps = await ApiClient.GetAsync<List<Employee>>(EmployeeUrl);
        return View(emps);
    }

    public async Task<IActionResult> Create()
    {
        ViewData["Departments"] = await GetDeptList();
        ViewData["Status"] = GetStatusList();
        return View();
    }

    private List<SelectListItem> GetStatusList()
    {
        var selectListStatus = new List<SelectListItem>();
        selectListStatus.Add(new SelectListItem { Text = "Inactive", Value = "0" });
        selectListStatus.Add(new SelectListItem { Text = "Active", Value = "1" });
        return selectListStatus;
    }

    private async Task<List<SelectListItem>> GetDeptList()
    {
        var depts = await ApiClient.GetAsync<List<Department>>(DepartmentUrl);
        var selectListDepts = new List<SelectListItem>();
        foreach (var dept in depts)
        {
            selectListDepts.Add(new SelectListItem { Text = dept.DepartmentName, Value = dept.DepartmentID.ToString() });
        }
        return selectListDepts;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmpl req)
    {
        try
        {
            await ApiClient.PostAsync<object, CreateEmpl>(EmployeeUrl, req);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            TempData["Message"] = "Email exist";
            return RedirectToAction("Create");
        }
    }

    public async Task<IActionResult> Detail(int id)
    {
        var emp = await ApiClient.GetAsync<Employee>($"{EmployeeUrl}/{id}");
        return View(emp);
    }

    public async Task<IActionResult> Update(int id)
    {
        var emp = await ApiClient.GetAsync<Employee>($"{EmployeeUrl}/{id}");
        ViewData["Departments"] = await GetDeptList();
        ViewData["Status"] = GetStatusList();
        return View(Mapper.Map(emp, new UpdateEmp()));
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, UpdateEmp req)
    {
        try
        {
            await ApiClient.PutAsync<object, UpdateEmp>($"{EmployeeUrl}/{id}", req);
            return RedirectToAction("Index");
        }
        catch
        {
            TempData["Message"] = "Server Error";
            return RedirectToAction("Update", new { id });
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var emp = await ApiClient.GetAsync<Employee>($"{EmployeeUrl}/{id}");
        return View(emp);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmp(int id)
    {
        try
        {
            await ApiClient.DeleteAsync<object>($"{EmployeeUrl}/{id}");
            return RedirectToAction("Index");
        }
        catch
        {
            TempData["Message"] = "Flower has ordered, cannot delete";
            return RedirectToAction("Delete", new { id });
        }
    }
}
