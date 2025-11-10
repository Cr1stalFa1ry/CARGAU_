using System;
using Domain.Interfaces.Repositoties;
using Domain.Models.Car;
using MediatR;

namespace Application.Cars.Queries.GetCar;

public class GetCarByIdQueryHandler
    : IRequestHandler<GetCarByIdQuery, Car?>
{
    private readonly ICarRepository _repository;
    public GetCarByIdQueryHandler(ICarRepository repository)
    {
        _repository = repository;
    }
    public async Task<Car?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.id, cancellationToken);
    }
}
