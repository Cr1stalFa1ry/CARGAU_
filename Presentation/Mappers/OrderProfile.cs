using AutoMapper;
using Domain.Entities;
using Domain.Models.Order;

namespace Presentation.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderEntity, Order>()
            .ForMember(dest => dest.CreatedAt, 
                opt => opt.MapFrom(src => src.CreatedAt.ToOffset(TimeSpan.FromHours(3))))
            .ForMember(dest => dest.UpdatedAt, 
                opt => opt.MapFrom(src => (src.UpdatedAt ?? src.CreatedAt).ToOffset(TimeSpan.FromHours(3))));;
    }
}
