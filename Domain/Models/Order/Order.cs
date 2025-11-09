using Domain.Models.Service;

namespace Domain.Models.Order;

public class Order
{
    public Order(
        Guid id,
        Guid clientId,
        Guid carId,
        OrderStatus status,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt,
        decimal totalPrice = 0)
    {
        Id = id;
        ClientId = clientId;
        CarId = carId;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        TotalPrice = totalPrice;
    }

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CarId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public List<Service.Service> SelectedServices { get; set; } = [];
    public decimal TotalPrice { get; set; }
}
