using AutoMapper;
using Domain.Interfaces.Mappers;
using Domain.Entities;
using Domain.Models.User;
using Application.Users.Commands.CreateNewUser;

namespace Presentation.Mappers;

public class UserProfile : Profile, IUserProfile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (Roles)src.Role!.Id));

        CreateMap<CreateUserCommand, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }
}
