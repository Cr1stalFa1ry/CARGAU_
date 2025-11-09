using System;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.User;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IMapper _mapper;
    private readonly TuningStudioDbContext _context;
    public RefreshTokenRepository(TuningStudioDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task AddToken(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        var refreshTokenEntity = _mapper.Map<RefreshTokenEntity>(refreshToken);

        await _context.RefreshTokens.AddAsync(refreshTokenEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteToken(Guid userId, CancellationToken cancellationToken = default)
    {
        var res = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .ExecuteDeleteAsync();

        return res > 0;
    }
}
