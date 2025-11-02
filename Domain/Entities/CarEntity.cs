using Domain.Models.Car;

namespace Domain.Entities;

public class CarEntity : BaseEntity<Guid>
{
    /// <summary>
    /// Основные характеристики автомобиля
    /// </summary>
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public string YearRelease { get; set; } = string.Empty;
    public decimal Price { get; set; }

    /// <summary>
    /// Состояние автомобиля
    /// </summary>
    public CarCondition Condition { get; set; } = CarCondition.Excellent;

    /// <summary>
    /// Связыне сущности и вторичные ключи
    /// </summary>
    public UserEntity? Owner { get; set; }
    public Guid OwnerId { get; set; }
    public List<OrderEntity> Orders { get; set; } = [];
}
