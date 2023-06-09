using Api.Utils;
using Application.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMapper _mapper;
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    public int CurrentUserId => GetCurrentUserId();

    public bool IsAdmin => IsCurrentUserAdmin();

    private int GetCurrentUserId()
    {
        if (!User.Identity.IsAuthenticated)
        {
            throw new UnauthorizedException();
        }
        return int.Parse(User.FindFirst(x => x.Type == "id").Value);
    }

    private bool IsCurrentUserAdmin()
    {
        return User.IsInRole(nameof(Role.Admin));
    }
}
