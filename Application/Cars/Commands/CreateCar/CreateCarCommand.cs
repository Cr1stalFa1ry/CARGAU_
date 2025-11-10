using System.ComponentModel.DataAnnotations;
using Domain.Models.Car;
using MediatR;

namespace Application.Cars.Commands.CreateCar;
public record CreateCarCommand : IRequest<Guid>
{
    [Required]
    public Guid OwnerId { get; set; }
    [Required]
    public string Brand { get; init; } = string.Empty;
    [Required]
    public string Model { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public int Mileage { get; init; }
    [Required]
    public string YearRelease { get; init; } = string.Empty;
    [Required]
    public decimal Price { get; init; }
    public CarCondition Condition { get; init; }
}

