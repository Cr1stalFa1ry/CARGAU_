using System;
using Domain.Interfaces.DbContext;
using Domain.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.DeleteOrder;

public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, bool>
{
    private readonly ITuningStudioDbContext _context;
    public DeleteOrderByIdCommandHandler(ITuningStudioDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _context.Orders
                .Where(order => order.Id == request.OrderId)
                .ExecuteDeleteAsync(cancellationToken);

            return res > 0;
        }
        catch (OperationCanceledException ex)
        {
            throw new ApplicationException($"Операция была отменена: {ex.Message}");
        }
        catch (ArgumentNullException ex)
        {
            throw new ApplicationException($"Аргумент со значением null: {ex.Message}");
        }
    }
}
