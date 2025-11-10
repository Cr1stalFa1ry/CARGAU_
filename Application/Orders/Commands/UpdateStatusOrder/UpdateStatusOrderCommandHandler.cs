using System;
using Domain.Interfaces.DbContext;
using Domain.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.UpdateStatusOrder;

public class UpdateStatusOrderCommandHandler : IRequestHandler<UpdateStatusOrderCommand, bool>
{
    private readonly ITuningStudioDbContext _context;
    public UpdateStatusOrderCommandHandler(ITuningStudioDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(UpdateStatusOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var updateResult = await _context.Orders
                .Where(order => order.Id == request.OrderId)
                .ExecuteUpdateAsync(order => order
                .SetProperty(order => order.Status, (OrderStatus)request.Status), cancellationToken);

            return updateResult > 0;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Возникла ошибка при обновлении статуса заказа: {ex.Message}");
        }
    }
}
