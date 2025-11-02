using Domain.Models.Car;
using MediatR;

namespace Application.Cars.Queries.GetCars;

public record class GetCarsQuery(int page, int pageSize) : IRequest<List<Car>>;
