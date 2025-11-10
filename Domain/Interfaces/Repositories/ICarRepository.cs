using Domain.Models.Car;
using Domain.Models.Service;

namespace Domain.Interfaces.Repositoties;

public interface ICarRepository
{
    Task AddAsync(Car car, CancellationToken cancellationToken);
    Task<List<Car>> GetAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Service>> GetServicesByCarIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateOwnerAsync(Car updateCar, CancellationToken cancellationToken);
    Task UpdatePriceAsync(Car updateCar, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
