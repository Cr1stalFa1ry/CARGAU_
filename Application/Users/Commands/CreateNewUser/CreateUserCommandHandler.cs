using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Domain.Interfaces.Users.PasswordHasher;
using Domain.Interfaces.DbContext;
using Domain.Interfaces.Users.Jwt;
using Domain.Models.User;
using Domain.Entities;
using AutoMapper;

namespace Application.Users.Commands.CreateNewUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, (string, string)>
{
    private readonly ITuningStudioDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    public CreateUserCommandHandler(
        ITuningStudioDbContext context,
        ILogger<CreateUserCommandHandler> logger,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task<(string, string)> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _passwordHasher.Generate(request.Password);
        var user = User.Create(Guid.NewGuid(), request.UserName, request.Email, (Roles)request.Role);

        // еще потом нужно будет сделать токен обновления
        var jwt = _jwtProvider.GenerateToken(user);

        var roleEntity = await _context.Roles
            .SingleOrDefaultAsync(r => r.Id == request.Role, cancellationToken) 
            ?? throw new InvalidOperationException("role not found");

        var userEntity = _mapper.Map<UserEntity>(request);
        userEntity.PasswordHash = passwordHash;
        userEntity.Role = roleEntity;

        // можно использовать транзакцию, просто еще нужно будет создавать токен обновления
        await _context.Users.AddAsync(userEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return (string.Empty, jwt);
    }
}
