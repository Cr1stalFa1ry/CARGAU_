using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CARGAU.Jwt;
using CARGAU.Extensions;
using Infrastructure.Repositories;
using Infrastructure.Context;
using Application.Cars.Commands.CreateCar;
using Application.Services;
using Domain.Interfaces.Repositoties;
using Domain.Interfaces.Users.Jwt;
using Domain.Interfaces.Users.PasswordHasher;
using Presentation.Mappers;
using Domain.Interfaces.Users.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Users.IRefreshTokenProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("Application"))
);
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

/// <summary>
/// Добавление сервисов в DI
/// <summary>
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

/// Добавление профилей маппинга
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<RoleProfile>();
    cfg.AddProfile<ServiceProfile>();
    cfg.AddProfile<RefreshTokenProfile>();
    cfg.AddProfile<OrderProfile>();
    cfg.AddProfile<CarProfile>();
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// использование swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
