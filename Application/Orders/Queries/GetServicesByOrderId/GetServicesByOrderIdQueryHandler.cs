using System;
using AutoMapper;
using Domain.Interfaces.DbContext;
using Domain.Models.Order;
using Domain.Models.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetServicesByOrderId;

public class GetServicesByOrderIdQueryHandler : IRequestHandler<GetServicesByOrderIdQuery, List<Service>>
{
    private readonly ITuningStudioDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetServicesByOrderIdQueryHandler(ITuningStudioDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<Service>> Handle(GetServicesByOrderIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceEntities = await _dbContext.Orders
                .AsNoTracking()
                .Where(order => order.Id == request.OrderId)
                .Include(order => order.SelectedServices)
                .SelectMany(order => order.SelectedServices)
                .ToListAsync(cancellationToken);

            var services = serviceEntities
                .Select(_mapper.Map<Service>)
                .ToList();

            return services;
        }
        catch (ArgumentNullException ex)
        {
            throw new ApplicationException($"Возникла ошибка при передаче null объекта {ex.Source} в метод", ex);
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException($"Операция получения списка услуг заказа {request.OrderId} была отменена.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Произошла ошибка при получении списка услгу заказа {request.OrderId}", ex);
        }
    }
}
