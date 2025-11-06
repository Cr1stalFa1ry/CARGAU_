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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("Application"))
);
builder.Services.AddControllers();
builder.Services.AddLogging();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

/// <summary>
/// Добавление сервисов в DI
/// <summary>
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

/// Добавление профилей маппинга
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<RoleProfile>();
    cfg.AddProfile<ServiceProfile>();
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// использование swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
