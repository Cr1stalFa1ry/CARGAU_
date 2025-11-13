using Domain.Models.Service;
using MediatR;

namespace Application.Orders.Commands.AddServicesToOrder;

public record class AddServicesToOrderCommand(Guid OrderId, List<int> ServicesToAdd) : IRequest;
