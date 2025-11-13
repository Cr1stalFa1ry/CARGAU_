using System;
using Domain.Entities;
using Domain.Interfaces.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
{
    private readonly ITuningStudioDbContext _dbContext;
    public CreateServiceCommandHandler(ITuningStudioDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceEntity = new ServiceEntity
            {
                Name = request.Name,
                Summary = request.Summary,
                Price = request.Price,
                Type = request.Type
            };

            await _dbContext.Services.AddAsync(serviceEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return serviceEntity.Id;
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException($"Создание услуги {request.Name} было прервано.", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ошибка сохранения услуги в базу данных", ex);
        }
    }
}
