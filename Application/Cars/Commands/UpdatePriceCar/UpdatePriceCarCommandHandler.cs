using Domain.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Application.Cars.Commands.UpdatePriceCar;

public class UpdatePriceCarCommandHandler : IRequestHandler<UpdatePriceCarCommand>
{
    private ITuningStudioDbContext _context;
    private ILogger _logger;
    public UpdatePriceCarCommandHandler(
        ITuningStudioDbContext context,
        ILogger<UpdatePriceCarCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
        
    public async Task Handle(UpdatePriceCarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var numberRowsUpdated = await _context.Cars
                .Where(car => car.Id == request.id)
                .ExecuteUpdateAsync(car => car
                    .SetProperty(car => car.Price, request.newPrice)
                );

            if (numberRowsUpdated == 0)
                throw new ArgumentException("Не получилось обновить цену автомобиля");
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
