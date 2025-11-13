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
                .FirstOrDefaultAsync(user => user.Id == request.newOwnerId, cancellationToken)
                ?? throw new ArgumentNullException($"Пользователь {request.newOwnerId} не найден.");

            var numberRowsUpdated = await _context.Cars
                .Where(car => car.Id == request.carId)
                .ExecuteUpdateAsync(car => car
                    .SetProperty(car => car.Owner, newOnwer)
                    , cancellationToken
                );
            if (numberRowsUpdated == 0)
                throw new ArgumentException("Не удалось поменять владельца автомобиля.");
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException("Операция смены владельца авто была отменена", ex);
        }
        catch (ArgumentNullException)
        {
            throw;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"Ошибка при изменении владельца авто {request.newOwnerId}", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Неожиданная ошибка при изменении владельца авто {request.newOwnerId}", ex);
        }       
    }
}
