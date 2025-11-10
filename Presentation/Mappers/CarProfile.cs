using System;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Car;

namespace Presentation.Mappers;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarEntity>()
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore());

        CreateMap<CarEntity, Car>();
    }
}
