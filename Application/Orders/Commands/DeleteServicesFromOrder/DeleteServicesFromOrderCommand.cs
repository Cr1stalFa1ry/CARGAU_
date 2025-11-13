using MediatR;

namespace Application.Orders.Commands.DeleteServicesFromOrder;

public record class DeleteServicesFromOrderCommand(Guid OrderId, List<int> ServicesIdsToDelete) : IRequest;
