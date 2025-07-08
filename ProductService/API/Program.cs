var builder = WebApplication.CreateBuilder(args);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

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

string aspnetcoreUrls = "http://0.0.0.0:80"; 

foreach (var mapping in envMappings)
{
    var value = Environment.GetEnvironmentVariable(mapping.Key);
    if (!string.IsNullOrEmpty(value))
    {
        builder.Configuration[mapping.Value] = value;
        
        if (mapping.Key == "aspnetcore-urls")
        {
            aspnetcoreUrls = value;
        }
    }
}
builder.WebHost.UseUrls(aspnetcoreUrls);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();
app.MapGet("/", () => Results.Ok("Hello from the root!"));
app.Run();
