using System;

namespace Domain.Models.Service;

public class Service
{
    private Service(int id, string nameService, decimal price, string summary, TuningType type)
    {
        Name = nameService;
        Price = price;
        Summary = summary;
        Id = id;
        Type = type;
    }

    private Service(string nameService, decimal price, string summary, TuningType type)
    {
        Name = nameService;
        Price = price;
        Summary = summary;
        Type = type;
    }

    public Service() { }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TuningType Type { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Service Create(int id, string nameService, decimal price, string summary, TuningType type)
    {
        return new Service(id, nameService, price, summary, type);
    }
    public Service Create(string nameService, decimal price, string summary, TuningType type)
    {
        return new Service(nameService, price, summary, type);
    }
}
    
