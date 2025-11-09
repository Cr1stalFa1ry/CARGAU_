using Domain.Models.Car;
using MediatR;

namespace Application.Cars.Queries.GetCars;

public record class GetCarsQuery(int Page, int PageSize) : IRequest<List<Car>>;
