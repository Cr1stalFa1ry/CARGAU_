using System;
using AutoMapper;
using Domain.Interfaces.DbContext;
using Domain.Models.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Service>
{
    private readonly ITuningStudioDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetServiceByIdQueryHandler(ITuningStudioDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Service> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceEntity = await _dbContext.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(service => service.Id == request.Id, cancellationToken)
                ?? throw new ArgumentNullException($"Услуга с id: {request.Id} не найдена.");

            return _mapper.Map<Service>(serviceEntity);
        }
        catch (ArgumentNullException ex)
        {
            throw new ApplicationException(ex.Message);
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
