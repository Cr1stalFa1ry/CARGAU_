using System.ComponentModel.DataAnnotations;
using Domain.Models.Order;
using MediatR;

namespace Application.Orders.Queries.GetOrders;

public record class GetOrdersQuery : IRequest<List<Order>>
{
    [Required]
    public int Page { get; init; }
    [Required]
    public int PageSize { get; init; }
}