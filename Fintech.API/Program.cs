
using Fintech.Application.Interfaces;
using Fintech.Domain.Interfaces;
using Fintech.Domain.Settings;
using Fintech.Infrastructure.Data;
using Fintech.Infrastructure.Repositories;
using Fintech.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<FintechDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<ITokenService>(sp =>
{
    var jwtSettings = sp.GetRequiredService<IOptions<JwtSettings>>().Value;
    return new TokenService(jwtSettings.Secret);
});
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FintechDbContext>();
    FintechDbContextSeed.SeedAsync(context).GetAwaiter().GetResult();
}

app.Run();



