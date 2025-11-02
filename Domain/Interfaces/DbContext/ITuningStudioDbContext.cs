using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain.Interfaces.DbContext;

public interface ITuningStudioDbContext
{
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
}