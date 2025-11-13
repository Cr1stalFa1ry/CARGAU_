using System;
using AutoMapper;
using Domain.Interfaces.DbContext;
using Domain.Models.User;
using Domain.ResponseModels.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUsers;

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, List<UserResponse>>
{
    private readonly ITuningStudioDbContext _dbContext;
    private readonly IMapper _mapper;
    private int _page;
    private int _pageSize;
    public GetUsersCommandHandler(ITuningStudioDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<UserResponse>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        _page = Math.Max(1, request.Page);
        _pageSize = Math.Clamp(request.PageSize, 1, 100);
        try
        {
            var userEntities = await _dbContext.Users
                .AsNoTracking()
                .OrderBy(user => user.Id)
                .Skip((_page - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync(cancellationToken);

            var users = userEntities
                .Select(_mapper.Map<UserResponse>)
                .ToList();

            return users;
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
