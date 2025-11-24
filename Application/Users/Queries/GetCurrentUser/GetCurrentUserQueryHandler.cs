using System;
using AutoMapper;
using Domain.Interfaces.DbContext;
using Domain.Interfaces.Users.Services;
using Domain.Models.User;
using Domain.ResponseModels.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserResponse>
{
    private readonly ITuningStudioDbContext _dbContext;
    private readonly IUserContextService _contextService;
    private readonly IMapper _mapper;
    public GetCurrentUserQueryHandler(
        ITuningStudioDbContext dbContext,
        IUserContextService contextService,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _contextService = contextService;
        _mapper = mapper;
    }
    public async Task<UserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUserId = _contextService.GetCurrentUserId();
            if (currentUserId == null)
            {
                throw new ArgumentNullException("Текущего пользователя нету, нужно войти в профиль.");
            }

            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Id == currentUserId, cancellationToken)
                ?? throw new ArgumentNullException("Пользователь не существует.");

            return _mapper.Map<UserResponse>(userEntity);
        }
        catch (ArgumentNullException)
        {
            throw;
        }
        catch (OperationCanceledException ex)
        {
            throw new TaskCanceledException($"Операция была отменена.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Произошла непредвиденная ошибка: {ex.Message}", ex);
        }
    }
}
