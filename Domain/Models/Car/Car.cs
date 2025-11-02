namespace Domain.Models.Car;

public class Car
{
    private Car(
        Guid id,
        string brand,
        string model,
        string color,
        int mileage,
        string yearRelease,
        decimal price
        )
    {
        Id = id;
        Brand = brand;
        Model = model;
        Color = color;
        Mileage = mileage;
        YearRelease = yearRelease;
        Price = price;
    }

    public Car() { }
    public Guid Id { get; set; }

    /// <summary>
    /// Основные параметры авто
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

    public Guid OwnerId { get; set; }

    public static Car Create(Guid id, string brand, string model, string color, int mileage, string yearRelease, decimal price)
    {
        /// <summary>
        /// Валидация параметров авто
        /// </summary>
        if (string.IsNullOrEmpty(brand))
            throw new ArgumentException("Строка с названием бренда должна быть не пустой");
        if (string.IsNullOrEmpty(model))
        { }
        if (string.IsNullOrEmpty(yearRelease))
        { }
        if (price >= 0)
        { }

        return new Car(id, brand, model, color, mileage, yearRelease, price);
    }
}
