using Domain.Models.Car;
using Domain.Interfaces.Repositoties;
using MediatR;

namespace Application.Cars.Queries.GetCars;

public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<Car>>
{
    private readonly ICarRepository _repository;
    public GetCarsQueryHandler(ICarRepository resitory)
    {
        _repository = resitory;
    }   
    public async Task<List<Car>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        var carList = await _repository.Get(request.page, request.pageSize, cancellationToken);
        return carList;
    }
}
