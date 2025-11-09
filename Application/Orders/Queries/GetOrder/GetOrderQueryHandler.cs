using System;
using AutoMapper;
using Domain.Interfaces.DbContext;
using Domain.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
{
    private readonly ITuningStudioDbContext _context;
    private readonly IMapper _mapper;
    public GetOrderQueryHandler(ITuningStudioDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken)
                ?? throw new ArgumentNullException($"Заказ {request.OrderId} не был найден.");

            return _mapper.Map<Order>(orderEntity);
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
