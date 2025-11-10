using Domain.Entities;
using Domain.Interfaces.DbContext;
using Domain.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly ITuningStudioDbContext _context;
    public CreateOrderCommandHandler(ITuningStudioDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var carExists = await _context.Cars
            .AnyAsync(car => car.Id == request.CarId);
        if (!carExists)
            throw new ArgumentException($"Car with ID {request.CarId} not found");

        var clientExists = await _context.Users
            .AnyAsync(u => u.Id == request.ClientId, cancellationToken);
        if (!clientExists)
            throw new ArgumentException($"Client with ID {request.ClientId} not found");
            
        try
        {
            var orderEntity = new OrderEntity
            {
                Id = Guid.NewGuid(),
                ClientId = request.ClientId,
                CarId = request.CarId,
                UpdatedAt = DateTimeOffset.UtcNow,
                Status = OrderStatus.New
            };

            await _context.Orders.AddAsync(orderEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return orderEntity.Id;
        }
        catch (OperationCanceledException ex)
        {
            throw new ApplicationException($"Операция была прервана: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Произошла какая то ошибка при создании заказа: {ex.Message}");
        }
    }
}
