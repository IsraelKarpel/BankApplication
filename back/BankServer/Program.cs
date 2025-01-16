using BankServer.Application.Managers;
using BankServer.Application.Mappers;
using BankServer.Application.Services.ActionServices;
using BankServer.Application.Services.RepositoryServices;
using BankServer.Application.Services.TokenServices;
using BankServer.Application.Services.ValidationServices;
using BankServer.Common;
using BankServer.Infrastructure.LoggerService;
using BankServer.Infrastructure.RepositoryService;
using BankServer.Infrastructure.RequestProcessor;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Replace with your React app's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//DI
builder.Services.AddHttpClient();

builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IRequestProcessor, RequestProcessor>();

var urlSettings = builder.Configuration.GetSection("UrlSettings");
builder.Services.Configure<UrlSettings>(urlSettings);

builder.Services.AddScoped<ITransactionMapper, TransactionMapper>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddScoped<IRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>(); // Resolve IConfiguration
    var logger = provider.GetRequiredService<ILoggerService>(); // Resolve ILoggerService
    var connectionString = configuration.GetConnectionString("BankDatabase"); // Get connection string
                                                                              // Validate the connection string
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'BankDatabase' is not configured in appsettings.json.");
    }
    return new SQLRepository(connectionString, logger); // Pass resolved logger
});
builder.Services.AddScoped<ITransactionManager, TransactionManager>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();

var app = builder.Build();

app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseRouting();

app.UseAuthorization();

app.MapControllers(); // Use this for APIs only

app.Run();
