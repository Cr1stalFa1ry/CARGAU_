using Domain.Models.Service;
using MediatR;

namespace Application.Cars.Queries.GetServicesByCar;

public record class GetServicesByCarQuery(Guid id) : IRequest<List<Service>>;