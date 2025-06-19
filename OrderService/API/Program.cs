using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);


var envMappings = new Dictionary<string, string>
{
    {"aspnetcore-urls", "ASPNETCORE_URLS"},
    {"dbconn", "ConnectionStrings:DefaultConnection"},
    {"jwtkey", "JwtSettings:Secret"},
    {"apikey", "ApiSettings:Key"},
    {"logpath", "Logging:LogPath"},
    {"redisconn", "ConnectionStrings:Redis"},
    {"blobconn", "ConnectionStrings:BlobStorage"}
};

foreach (var mapping in envMappings)
{
    var value = Environment.GetEnvironmentVariable(mapping.Key);
    if (!string.IsNullOrEmpty(value))
    {
        builder.Configuration[mapping.Value] = value;
    }

    if (mapping.Key == "aspnetcore-urls" && string.IsNullOrEmpty(value))
    {
        builder.Configuration[mapping.Value] = "http://0.0.0.0";
    }
}


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
var app = builder.Build();

var actionProvider = app.Services.GetService<IActionDescriptorCollectionProvider>();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
if (actionProvider != null)
{
    var actionDescriptors = actionProvider.ActionDescriptors.Items;
    var controllerNames = actionDescriptors
        .Select(x => x.RouteValues["controller"])
        .Where(x => !string.IsNullOrEmpty(x))
        .Distinct()
        .ToList();

    logger.LogInformation($"Found {controllerNames.Count} controllers:");
    foreach (var name in controllerNames)
    {
        logger.LogInformation($"- {name}");
    }
}


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

