using Domain.Interfaces.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
{
    private readonly ITuningStudioDbContext _dbContext;
    public UpdateServiceCommandHandler(ITuningStudioDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var updatedRows = await _dbContext.Services
                .Where(service => service.Id == request.Id)
                .ExecuteUpdateAsync(service => service
                    .SetProperty(service => service.Name, request.Name)
                    .SetProperty(service => service.Summary, request.Summary)
                    .SetProperty(service => service.Price, request.Price)
                    .SetProperty(service => service.Type, request.Type),
                    cancellationToken);

            if (updatedRows == 0)
            {
                throw new KeyNotFoundException($"Услуга с ID {request.Id} не найдена");
            }
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ошибка при сохранении изменений в базе данных", ex);
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException("Операция обновления услуги была отменена", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Непредвиденная ошибка при обновлении услуги: {ex.Message}", ex);
        }
    }
}
