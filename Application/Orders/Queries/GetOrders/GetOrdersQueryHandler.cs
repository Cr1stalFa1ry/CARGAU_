using System;
using AutoMapper;
using Domain.Interfaces.DbContext;
using Domain.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<Order>>
{
    private readonly ITuningStudioDbContext _context;
    private readonly IMapper _mapper;
    private int Page;
    private int PageSize;
    public GetOrdersQueryHandler(ITuningStudioDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        Page = Math.Max(1, request.Page);
        PageSize = Math.Clamp(request.PageSize, 1, 100); // всегда от 1 до 100
        try
        {
            var orderEntities = await _context.Orders
                .AsNoTracking()
                .OrderBy(order => order.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);

            var orders = orderEntities
                .Select(entity => _mapper.Map<Order>(entity))
                .ToList();

            return orders;
        }
        catch (OperationCanceledException ex)
        {
            throw new ApplicationException($"Операция отменена: {ex.Message}");
        }
        catch (ArgumentNullException ex)
        {
            throw new ApplicationException($"Аргумент со значением null: {ex.Message}");
        }
    }
}
