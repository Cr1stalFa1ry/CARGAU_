using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;


namespace Infrastructure.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder
            .HasKey(car => car.Id);

        builder
            .HasOne(car => car.Owner)
            .WithMany(owner => owner.Cars)
            .HasForeignKey(car => car.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(car => car.Orders)
            .WithOne(order => order.Car)
            .HasForeignKey(order => order.CarId);
    }
}
