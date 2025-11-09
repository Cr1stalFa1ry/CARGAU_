using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Orders.Commands.DeleteOrder;

public record class DeleteOrderByIdCommand : IRequest<bool>
{
    [Required]
    public Guid OrderId { get; init; }
}
