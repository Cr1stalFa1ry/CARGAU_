using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Domain.Interfaces.Users.PasswordHasher;
using Domain.Interfaces.DbContext;
using Domain.Interfaces.Users.Jwt;
using Domain.Models.User;
using Domain.Entities;
using AutoMapper;
using Domain.Interfaces.Users.IRefreshTokenProvider;

namespace Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, (string refreshToken, string jwt)>
{
    private readonly ITuningStudioDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenProvider _rtProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    public RegisterUserCommandHandler(
        ITuningStudioDbContext dbContext,
        ILogger<RegisterUserCommandHandler> logger,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IRefreshTokenProvider rtProvider,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _rtProvider = rtProvider;
        _mapper = mapper;
    }

    public async Task<(string refreshToken, string jwt)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var passwordHash = _passwordHasher.Generate(request.Password);
            var user = User.Create(
                Guid.NewGuid(),
                request.UserName,
                request.Email,
                (Roles)request.RoleId,
                request.PhoneNumber,
                request.FirstName,
                request.LastName,
                request.DateOfBirth
            );

            var jwt = _jwtProvider.GenerateToken(user);
            var refreshToken = _rtProvider.GenerateRefreshToken(user);
            var refreshTokenEntity = _mapper.Map<RefreshTokenEntity>(refreshToken);

            var roleEntity = await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken)
                ?? throw new InvalidOperationException("Роль не найдена.");

            var userEntity = _mapper.Map<UserEntity>(user);
            userEntity.PasswordHash = passwordHash;
            userEntity.Role = roleEntity;

            await _dbContext.Users.AddAsync(userEntity, cancellationToken);
            
            await _dbContext.RefreshTokens.AddAsync(refreshTokenEntity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return (refreshToken.Token, jwt);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
