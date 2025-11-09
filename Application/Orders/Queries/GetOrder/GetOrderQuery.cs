using System.ComponentModel.DataAnnotations;
using Domain.Models.Order;
using MediatR;

namespace Application.Orders.Queries.GetOrder;

public record class GetOrderQuery : IRequest<Order>
{
    [Required]
    public Guid OrderId { get; init; }
}
