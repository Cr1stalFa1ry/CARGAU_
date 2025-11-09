using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Models.User;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TuningStudioDbContext _context;
    private readonly IMapper _mapper;
 
    public UserRepository(TuningStudioDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken)
            ?? throw new InvalidOperationException("Пользователь с таким адресом электронной почты не существует.");

        return _mapper.Map<User>(userEntity);
    }
}
