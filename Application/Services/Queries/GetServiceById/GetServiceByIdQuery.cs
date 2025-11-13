using Domain.Models.Service;
using MediatR;

namespace Application.Services.Queries.GetServiceById;

public record class GetServiceByIdQuery(int Id) : IRequest<Service>;
