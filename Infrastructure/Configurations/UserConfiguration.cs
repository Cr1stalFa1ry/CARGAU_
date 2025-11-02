using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(user => user.Id);

        // builder
        //     .Property(user => user.Role)
        //     .HasConversion<string>();

        builder
            .HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.RoleId);

        builder
            .HasMany(user => user.Cars)
            .WithOne(car => car.Owner)
            .HasForeignKey(car => car.OwnerId);

        builder
            .HasMany(user => user.Orders)
            .WithOne(order => order.Client)
            .HasForeignKey(order => order.ClientId);

        builder
            .HasMany(user => user.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId);
    }
}
