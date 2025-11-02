using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(role => role.Id);

        // builder.HasData(
        //     new RoleEntity { Id = 1, Name = "Admin" },
        //     new RoleEntity { Id = 2, Name = "User" }
        // );
    }
}

