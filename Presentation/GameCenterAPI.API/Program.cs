using GameCenterAPI.API.Extensions;
using GameCenterAPI.Application;
using GameCenterAPI.Application.Features.Auth.Validations;
using GameCenterAPI.Domain.Entities.Identity;
using GameCenterAPI.Infrastructure;
using GameCenterAPI.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//logging
builder.Logging.AddConsole();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    //opt.AddDefaultPolicy(policy => policy.WithHeaders("http://localhost:5173").AllowAnyMethod().AllowAnyOrigin());
});

builder.Services
    .AddIdentity<AppUser, AppRole>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddMongoDbStores<AppUser, AppRole, string>(builder.Configuration.GetConnectionString("MongoDb"), "game-center-db")
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero,
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
        };
    });

var app = builder.Build();

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
