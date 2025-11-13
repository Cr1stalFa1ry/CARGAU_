using System;
using Domain.Interfaces.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.DeleteServicesFromOrder;

public class DeleteServicesFromOrderCommandHandler : IRequestHandler<DeleteServicesFromOrderCommand>
{
    private readonly ITuningStudioDbContext _dbContext;
    public DeleteServicesFromOrderCommandHandler(ITuningStudioDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(DeleteServicesFromOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = await _dbContext.Orders
               .Include(order => order.SelectedServices)
               .FirstOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken)
               ?? throw new ArgumentNullException($"Не найден заказ {request.OrderId} при удалении услуг.");

            foreach (var serviceId in request.ServicesIdsToDelete)
                orderEntity.SelectedServices.RemoveAll(service => service.Id == serviceId);

            orderEntity.TotalPrice = orderEntity.SelectedServices.Sum(service => service.Price);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException("Операция удаления услуг была отменена", ex);
        }
        catch (ArgumentNullException)
        {
            throw;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"Ошибка при удалении услуг в заказе {request.OrderId}", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Неожиданная ошибка при удалении услуг заказа {request.OrderId}", ex);
        }
    }
}
