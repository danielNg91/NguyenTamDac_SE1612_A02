using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Text.Json;
using WebClient.Datasource;
using WebClient.Utils;

namespace WebClient.Controllers;
public class BaseController : Controller
{
    protected readonly AppSettings _appSettings;
    protected readonly string BaseUri;
    protected readonly string LoginUrl;
    protected readonly string ProfileUrl;
    protected readonly string EmployeeUrl;
    protected readonly string DepartmentUrl;
    protected readonly string ProjectUrl;
    protected readonly string ParticipantUrl;
    protected readonly IApiClient ApiClient;

    public BaseController(IOptions<AppSettings> appSettings, IApiClient apiClient)
    {
        _appSettings = appSettings.Value;
        BaseUri = _appSettings.BaseUri;
        LoginUrl = _appSettings.LoginUrl;
        ProfileUrl = _appSettings.ProfileUrl;
        EmployeeUrl = _appSettings.EmployeeUrl;
        DepartmentUrl = _appSettings.DepartmentUrl;
        ProjectUrl = _appSettings.ProjectUrl;
        ParticipantUrl = _appSettings.ParticipantUrl;
        ApiClient = apiClient;
    }

    private IMapper _mapper;
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    public int CurrentUserId => GetCurrentUserId();
    
    public bool IsAdmin => IsCurrentUserAdmin();

    private bool IsCurrentUserAdmin()
    {
        return User.IsInRole(nameof(Role.Admin));
    }

    private int GetCurrentUserId()
    {
        if (!User.Identity.IsAuthenticated)
        {
            Redirect("Login");
            return 0;
        }
        return int.Parse(User.FindFirst(x => x.Type == "id").Value);
    }
}
