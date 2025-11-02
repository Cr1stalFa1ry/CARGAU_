using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.DbContext;
using Domain.Entities;
using Infrastructure.Configurations;

namespace Infrastructure.Context;

public class TuningStudioDbContext(DbContextOptions<TuningStudioDbContext> options) 
    : DbContext(options), ITuningStudioDbContext
{
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
