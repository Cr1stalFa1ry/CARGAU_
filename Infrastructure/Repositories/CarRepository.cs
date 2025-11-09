using Domain.Interfaces.Repositoties;
using Domain.Models.Car;
using Domain.Models.Service;
using Infrastructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Infrastructure.Repositories;

public class CarRepository : ICarRepository
{
    private readonly TuningStudioDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    public CarRepository(
        TuningStudioDbContext context,
        ILogger<CarRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task Add(Car car, CancellationToken cancellationToken)
    {
        var carEntity = new CarEntity
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            YearRelease = car.YearRelease,
            Price = car.Price
        };

        await _context.Cars.AddAsync(carEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _context.Cars
                .Where(car => car.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<List<Car>> Get(int page, int pageSize, CancellationToken cancellationToken)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        try
        {
            var entityList = await _context.Cars
                .AsNoTracking()
                .OrderBy(car => car.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var carList = entityList
                .Select(car =>
                    Car.Create(
                        car.Id,
                        car.Brand,
                        car.Model,
                        car.Color,
                        car.Mileage,
                        car.YearRelease,
                        car.Price
                    )
                ).ToList();

            return carList;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        } 
    }

    public async Task<Car?> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var carEntity = await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(car => car.Id == id, cancellationToken)
                ?? throw new ArgumentNullException($"Car with id: {id} not found(");

            var car = Car.Create
            (
                carEntity.Id,
                carEntity.Brand,
                carEntity.Model,
                carEntity.Color,
                carEntity.Mileage,
                carEntity.YearRelease,
                carEntity.Price
            );
            

            return car;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        } 
    }

    public async Task<List<Service>> GetServicesByCarId(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var serviceEntities = await _context.Orders
                .Include(order => order.SelectedServices)
                .Where(order => order.CarId == id)
                .SelectMany(order => order.SelectedServices)
                .ToListAsync(cancellationToken);

            if (serviceEntities.Count == 0)
                return new List<Service>();

            var mappedServices = serviceEntities.Select(_mapper.Map<Service>).ToList();

            return mappedServices;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public Task UpdateOwner(Car updateCar)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePrice(Car updateCar)
    {
        throw new NotImplementedException();
    }
}
