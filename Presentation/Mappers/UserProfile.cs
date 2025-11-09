using AutoMapper;
using Domain.Interfaces.Mappers;
using Domain.Entities;
using Domain.Models.User;
using Application.Users.Commands.RegisterUser;
using AutoMapper.Internal.Mappers;

namespace Presentation.Mappers;

public class UserProfile : Profile, IUserProfile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (Roles)src.Role!.Id))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<User, UserEntity>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
            // .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new RoleEntity
            // {
            //     Id = (int)src.Role,
            //     Name = src.Role.ToString()
            // }));

        CreateMap<RegisterUserCommand, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }
}
