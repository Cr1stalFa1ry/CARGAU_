using AutoMapper;
using Domain.Entities;
using Domain.Models.Service;

namespace Presentation.Mappers;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceEntity>()
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, 
                opt => opt.MapFrom(src => src.CreatedAt.ToOffset(TimeSpan.FromHours(3))))
            .ForMember(dest => dest.UpdatedAt, 
                opt => opt.MapFrom(src => (src.UpdatedAt ?? src.CreatedAt).ToOffset(TimeSpan.FromHours(3))));;

        CreateMap<ServiceEntity, Service>()
            .ForMember(dest => dest.CreatedAt, 
                opt => opt.MapFrom(src => src.CreatedAt.ToOffset(TimeSpan.FromHours(3))))
            .ForMember(dest => dest.UpdatedAt, 
                opt => opt.MapFrom(src => (src.UpdatedAt ?? src.CreatedAt).ToOffset(TimeSpan.FromHours(3))));
    }
}
