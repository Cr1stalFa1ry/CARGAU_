using Domain.Models.Service;
using MediatR;

namespace Application.Services.Commands.CreateService;

public record CreateServiceCommand(string Name, string Summary, decimal Price, TuningType Type) : IRequest<int>;
