using TennisPlayerDomain.Entities;
using Serilog;
using TennisPlayerDomain.Interfaces;
using TennisPlayerInfrastructure.Repositories;
using TennisPlayerApplication.Interfaces;
using TennisPlayerApplication.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() 
    .WriteTo.File("Logs/TennisPlayersManager.log", rollingInterval: RollingInterval.Day) // Logs to a file
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.Configure<DatasetConfig>(
    builder.Configuration.GetSection("DatasetConfig"));

// Register HttpClient
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger to use the XML documentation file
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tennis Player Manager API",
        Version = "v1"
    });
});
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
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
