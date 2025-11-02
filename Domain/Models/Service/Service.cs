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
    public int Id { get; }
    public string Name { get; } = string.Empty;
    public string Summary { get; } = string.Empty;
    public decimal Price { get; }
    public TuningType Type { get; }

    public Service Create(int id, string nameService, decimal price, string summary, TuningType type)
    {
        return new Service(id, nameService, price, summary, type);
    }
    public Service Create(string nameService, decimal price, string summary, TuningType type)
    {
        return new Service(nameService, price, summary, type);
    }
}
    
