using System.Text.Json;
using API.Filler;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);
string aspnetcoreUrls = "http://0.0.0.0:5100"; 

// aspnetcoreUrls=Environment.GetEnvironmentVariable("aspnetcore-urls");

builder.WebHost.UseUrls(aspnetcoreUrls);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"\n[DEBUG] Connection String: {connectionString}\n");
    builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(connectionString));

    builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Service API",
        Version = "v1",
        Description = "API for managing orders."
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RequestLoggingFilter>();
});
var app = builder.Build();
// app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseExceptionHandler("/error");

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            Message = "An error occurred while processing your request",
            StatusCode = context.Response.StatusCode,
            RequestId = context.TraceIdentifier
        }));
    });
});
app.MapGet("/db-test", async (OrderDbContext dbContext) => 
{
    try 
    {
        var canConnect = await dbContext.Database.CanConnectAsync();
        return Results.Ok(new { 
            Success = canConnect,
            Message = canConnect ? "Database connected!" : "Could not connect to database"
        });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Database connection failed: {ex.Message}");
    }
});

var actionProvider = app.Services.GetService<IActionDescriptorCollectionProvider>();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(new CompactJsonFormatter(), 
        "Logs/logs.json",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API v1");
        options.RoutePrefix = "swagger"; 
    });
    
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();
app.MapGet("/", () => Results.Ok("Hello from root!"));
app.Run();

