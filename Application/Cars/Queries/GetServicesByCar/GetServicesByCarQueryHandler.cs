using Domain.Interfaces.Repositoties;
using Domain.Models.Service;
using MediatR;

namespace Application.Cars.Queries.GetServicesByCar;

public class GetServicesByCarQueryHandler : IRequestHandler<GetServicesByCarQuery, List<Service>>
{
    private readonly ICarRepository _repository;
    public GetServicesByCarQueryHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Service>> Handle(
        GetServicesByCarQuery request,
        CancellationToken cancellationToken)
    {
        var listServices = await _repository.GetServicesByCarId(request.id, cancellationToken);
        return listServices;
    }
}
