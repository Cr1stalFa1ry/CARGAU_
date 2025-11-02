using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder
            .HasKey(service => service.Id);

        builder
            .Property(service => service.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .HasMany(service => service.Orders)
            .WithMany(order => order.SelectedServices)
            .UsingEntity(j => j.ToTable("OrderServices"));
    }
}
