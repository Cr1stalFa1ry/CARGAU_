using AutoMapper;
using Domain.Interfaces.DbContext;
using Domain.Models.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Queries.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<Service>>
{
    private readonly ITuningStudioDbContext _dbContext;
    private readonly IMapper _mapper;
    private int _page;
    private int _pageSize;
    public GetServicesQueryHandler(ITuningStudioDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<Service>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        _page = Math.Max(1, request.Page);
        _pageSize = Math.Clamp(request.PageSize, 1, 100);
        try
        {
            var serviceEntities = await _dbContext.Services
                .AsNoTracking()
                .OrderBy(service => service.Id)
                .Skip((_page - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync(cancellationToken);

            return serviceEntities
                .Select(_mapper.Map<Service>)
                .ToList();
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
