using MediatR;

namespace Application.Cars.Commands.CreateCar;
public record CreateCarCommand(string Brand, string Model, string Color, int Mileage, string YearRelease, decimal Price) : IRequest<Guid>;

