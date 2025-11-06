using Domain.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Application.Cars.Commands.ChangeOwnerCar;

public class ChangeOwnerCarCommandHandler : IRequestHandler<ChangeOwnerCarCommand>
{
    private ITuningStudioDbContext _context;
    private ILogger _logger;
    public ChangeOwnerCarCommandHandler(
        ITuningStudioDbContext context,
        ILogger<ChangeOwnerCarCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(ChangeOwnerCarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newOnwer = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == request.newOwnerId)
                ?? throw new ArgumentNullException($"Пользователь с ID: {request.newOwnerId} не найден.");

            var numberRowsUpdated = await _context.Cars
                .Where(car => car.Id == request.carId)
                .ExecuteUpdateAsync(car => car
                    .SetProperty(car => car.Owner, newOnwer)
                );
            if (numberRowsUpdated == 0)
                throw new ArgumentException("Не удалось поменять владельца автомобиля.");
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogInformation(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }        
    }
}
