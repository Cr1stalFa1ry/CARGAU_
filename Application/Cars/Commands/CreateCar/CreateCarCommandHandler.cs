using Domain.Interfaces.Repositoties;
using Domain.Models.Car;
using MediatR;

namespace Application.Cars.Commands.CreateCar;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
{
    private readonly ICarRepository _repository;
    public CreateCarCommandHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCarCommand request,
        CancellationToken cancellationToken)
    {
        var newCar = Car.Create
        (
            Guid.NewGuid(),
            request.OwnerId,
            request.Brand,
            request.Model,
            request.Color,
            request.Mileage,
            request.YearRelease,
            request.Price,
            request.Condition
        );
        
        await _repository.AddAsync(newCar, cancellationToken);

        return newCar.Id;
    }
}
