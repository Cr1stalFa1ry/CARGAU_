using MediatR;

namespace Application.Cars.Commands.UpdatePriceCar;

public record class UpdatePriceCarCommand(Guid id, decimal newPrice) : IRequest;
