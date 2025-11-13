using System;
using AutoMapper;
using Domain.Entities;
using Domain.Models.User;

namespace Presentation.Mappers;

public class RefreshTokenProfile : Profile
{
    public RefreshTokenProfile()
    {
        CreateMap<RefreshToken, RefreshTokenEntity>()
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}
