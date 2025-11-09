using Domain.Entities;
using Domain.Models.Order;

namespace Domain.Entities;

public class OrderEntity : BaseEntity<Guid>
{
    public UserEntity? Client { get; set; }
    public Guid ClientId { get; set; }
    public CarEntity? Car { get; set; }
    public Guid CarId { get; set; }
    public List<ServiceEntity> SelectedServices { get; set; } = [];
    public decimal TotalPrice { get; set; } = 0;
    public OrderStatus Status { get; set; }
}
