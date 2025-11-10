using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Cars.Commands.UpdatePriceCar;

public record class UpdatePriceCarCommand : IRequest
{
    [Required]
    public Guid Id { get; init; }
    [Required]
    public decimal NewPrice { get; init; }
}
