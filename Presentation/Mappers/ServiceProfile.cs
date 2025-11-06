using AutoMapper;
using Domain.Entities;
using Domain.Models.Service;

namespace Presentation.Mappers;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceEntity>();
    }
}
