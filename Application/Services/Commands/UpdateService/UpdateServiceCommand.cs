using Domain.Models.Service;
using MediatR;

namespace Application.Services.Commands.UpdateService;

public record class UpdateServiceCommand(
    int Id, string Name, string Summary, decimal Price, TuningType Type) : IRequest;
