using Domain.Models.Service;
using MediatR;

namespace Application.Orders.Queries.GetServicesByOrderId;

public record class GetServicesByOrderIdQuery(Guid OrderId) : IRequest<List<Service>>;
