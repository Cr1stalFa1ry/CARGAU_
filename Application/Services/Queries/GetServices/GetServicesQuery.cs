using Domain.Models.Service;
using MediatR;

namespace Application.Services.Queries.GetServices;

public record class GetServicesQuery(int Page, int PageSize) : IRequest<List<Service>>;
