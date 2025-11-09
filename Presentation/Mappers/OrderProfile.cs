using AutoMapper;
using Domain.Entities;
using Domain.Models.Order;

namespace Presentation.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderEntity, Order>()
            .ForMember(dest => dest.SelectedServices, opt => opt.Ignore());
    }
}
