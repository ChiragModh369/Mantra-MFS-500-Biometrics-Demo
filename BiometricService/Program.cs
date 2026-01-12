using BiometricService.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// ✅ ADD THIS BLOCK
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Fingerprint Service
builder.Services.AddSingleton<IFingerprintService, MantraFingerprintService>();

// Configure CORS for Node.js communication
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNodeJS", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowNodeJS");
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("=================================");
Console.WriteLine("C# Fingerprint Service");
Console.WriteLine("Port: 5000");
Console.WriteLine("=================================");

app.Run();
