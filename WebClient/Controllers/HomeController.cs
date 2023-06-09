using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WebClient.Datasource;
using WebClient.Models;

namespace WebClient.Controllers;
public class HomeController : BaseController
{
    public HomeController(IOptions<AppSettings> appSettings, IApiClient apiClient) : base(appSettings, apiClient)
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error403()
    {
        return View();
    }
}
