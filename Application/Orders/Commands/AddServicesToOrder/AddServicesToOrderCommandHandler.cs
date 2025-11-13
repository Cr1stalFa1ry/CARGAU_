using Domain.Interfaces.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.AddServicesToOrder;

public class AddServicesToOrderCommandHandler : IRequestHandler<AddServicesToOrderCommand>
{
    private readonly ITuningStudioDbContext _dbContext;
    public AddServicesToOrderCommandHandler(ITuningStudioDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(AddServicesToOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = await _dbContext.Orders
                .Include(order => order.SelectedServices)
                .FirstOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken)
                ?? throw new ArgumentNullException($"Заказ {request.OrderId} не был найден.");
            
            var existingServiceIds = orderEntity.SelectedServices.Select(service => service.Id).ToList();

            var serviceIdsToAdd = await _dbContext.Services
                .Where(service => request.ServicesToAdd.Contains(service.Id) && !existingServiceIds.Contains(service.Id))
                .ToListAsync(cancellationToken);

            if (!serviceIdsToAdd.Any())
                return;

            foreach (var service in serviceIdsToAdd)
            {
                if (!orderEntity.SelectedServices.Any(os => os.Id == service.Id))
                {
                    orderEntity.SelectedServices.Add(service);
                }
            }

            orderEntity.TotalPrice = orderEntity.SelectedServices
                .Sum(service => service.Price);

            orderEntity.UpdatedAt = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
            
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException("Операция поиска услуг была отменена", ex);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ошибка базы данных при поиске услуг", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Непредвиденная ошибка при поиске услуг: {ex.Message}", ex);
        }
    }
}
