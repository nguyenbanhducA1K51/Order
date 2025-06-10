using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OrderDbConnection");

    builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(connectionString));

    builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
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

app.Run();

