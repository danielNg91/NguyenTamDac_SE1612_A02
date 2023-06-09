using Microsoft.AspNetCore.Authentication.Cookies;
using WebClient.Utils;

namespace WebClient;

public static class AuthConfiguration
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyName.ADMIN,
                policy => policy.RequireRole(PolicyName.ADMIN));
            options.AddPolicy(PolicyName.USER,
                policy => policy.RequireRole(PolicyName.USER));
        });
        return services;
    }

    public static IServiceCollection AddAppAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.ExpireTimeSpan = System.TimeSpan.FromDays(1);
            options.Cookie.HttpOnly = true;
            options.LoginPath = "/Login/Index";
            options.AccessDeniedPath = "/Home/Error403";
        });
        return services;
    }    
}
