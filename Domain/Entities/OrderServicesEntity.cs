using System;

namespace Domain.Entities;

public class OrderServiceEntity
{
    public Guid OrderId { get; set; }
    public OrderEntity Order { get; set; } = null!;
    
    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;
}
