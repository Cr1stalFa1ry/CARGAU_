using System.ComponentModel.DataAnnotations;
using Domain.Models.Order;
using MediatR;

namespace Application.Orders.Commands.UpdateStatusOrder;

public record class UpdateStatusOrderCommand : IRequest<bool>
{
    [Required]
    public Guid OrderId { get; init; }
    [Required]
    [Range(1, 7, ErrorMessage = "Id статуса должен быть от 1 до 7")]
    public int Status { get; init; }
}
