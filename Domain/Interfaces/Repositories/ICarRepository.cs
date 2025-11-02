using Domain.Models.Car;
using Domain.Models.Service;

namespace Domain.Interfaces.Repositoties;

public interface ICarRepository
{
    Task Add(Car car, CancellationToken cancellationToken);
    Task<List<Car>> Get(int page, int pageSize, CancellationToken cancellationToken);
    Task<Car?> GetById(Guid id, CancellationToken cancellationToken);
    Task<List<Service>> GetServicesByCarId(Guid id, CancellationToken cancellationToken);
    Task UpdateOwner(Car updateCar);
    Task UpdatePrice(Car updateCar);
    Task Delete(Guid id, CancellationToken cancellationToken);
}
