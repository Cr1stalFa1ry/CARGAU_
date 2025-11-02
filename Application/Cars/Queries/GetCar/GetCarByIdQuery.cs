using Domain.Models.Car;
using MediatR;

namespace Application.Cars.Queries.GetCar;

public record class GetCarByIdQuery(Guid id) : IRequest<Car>;
