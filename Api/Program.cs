using Api;
using Api.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

//services.AddScoped<ProjectManagementContext>();
// db config
services.AddDbContext<ProjectManagementContext>(options =>
{
    var settings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
    Console.WriteLine(settings);
    options.UseSqlServer(settings!.Value.ConnectionStrings.FUProjectManagement);
});


// app api
services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelStateFilter>();
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// app specs
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
