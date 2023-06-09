using Api;
using Api.Utils;
using AutoWrapper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
services.AddAutoMapper(Assembly.GetExecutingAssembly());

// db config
services.AddDbContext<ProjectManagementContext>(options =>
{
    var settings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
    Console.WriteLine(settings);
    options.UseSqlServer(settings!.Value.ConnectionStrings.FUProjectManagement);
});

// app api
const string CORS_POLICY = "CorsPolicy";
services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY, builder =>
    {
        var settings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
        builder.WithOrigins(settings!.Value.FrontendUrl)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelStateFilter>();
});
services.AddControllers().AddOData(
    option => option.Select()
                    .Filter()
                    .Count()
                    .OrderBy()
                    .Expand()
                    .SetMaxTop(100)
                    .AddRouteComponents("odata", ODataConfiguration.GetEdmModel())
);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// app specs
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddSingleton<AuthenticationEvent>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { IsApiOnly = false, ShowIsErrorFlagForSuccessfulResponse = true });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(CORS_POLICY);

app.MapControllers();

app.Run();
