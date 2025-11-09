using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public record class CreateOrderCommand(Guid ClientId, Guid CarId) : IRequest<Guid>;
