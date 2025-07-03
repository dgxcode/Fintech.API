using Fintech.API.Extensions;
using Fintech.Application.Interfaces;
using Fintech.Domain.Interfaces;
using Fintech.Domain.Settings;
using Fintech.Infrastructure.Data;
using Fintech.Infrastructure.Repositories;
using Fintech.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

DotNetEnv.Env.Load();
Console.WriteLine($"JWT_SECRET = {Environment.GetEnvironmentVariable("JWT_SECRET")}");


var builder = WebApplication.CreateBuilder(args);

// Configura JWT via .env
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                 ?? throw new InvalidOperationException("JWT_SECRET not configured");

builder.Services.Configure<JwtSettings>(options =>
{
    options.Secret = jwtSecret;
    options.ExpirationHours = 2;
    options.Issuer = "FintechAPI";
    options.Audience = "FintechAPIUsers";
});

// Configuração do appsettings.json + env + user-secrets
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

// DbContext
builder.Services.AddDbContext<FintechDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Serviços
builder.Services.AddSingleton<ITokenService>(sp =>
{
    var jwtSettings = sp.GetRequiredService<IOptions<JwtSettings>>().Value;
    return new TokenService(jwtSettings.Secret);
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.ApplyMigrationsAndSeedDatabase();

app.Run();
