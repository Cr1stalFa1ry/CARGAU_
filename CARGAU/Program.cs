using System.Reflection;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Domain.Interfaces.Repositoties;
using Application.Cars.Commands.CreateCar;
using MediatR;
using CARGAU.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("Application"))
);
builder.Services.AddControllers();
builder.Services.AddLogging();

builder.Services.AddInfrastructure(builder.Configuration);

/// <summary>
/// Добавление сервисов в DI
/// <summary>
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// использование swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
