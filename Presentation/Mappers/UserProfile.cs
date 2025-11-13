using AutoMapper;
using Domain.Interfaces.Mappers;
using Domain.Entities;
using Domain.Models.User;
using Application.Users.Commands.RegisterUser;
using AutoMapper.Internal.Mappers;
using Domain.ResponseModels.User;

namespace Presentation.Mappers;

public class UserProfile : Profile, IUserProfile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (Roles)src.RoleId!))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<User, UserEntity>()
            // Прямые поля
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))

            // Пароль хранится в виде хеша — в доменной модели его обычно нет,
            // поэтому задаётся отдельно в коде (через userEntity.PasswordHash = ...)
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())

            // Конвертация enum Roles → int RoleId
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => (int)src.Role))
            // Игнорируем навигационное свойство, чтобы EF не пытался вставить новую роль
            .ForMember(dest => dest.Role, opt => opt.Ignore())

            // Контактная информация
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? string.Empty))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName ?? string.Empty))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName ?? string.Empty))

            // Дата рождения может быть null
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))

            // Коллекции связей не трогаем — создаются при других операциях
            .ForMember(dest => dest.Cars, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore());


        CreateMap<UserEntity, UserResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => ((Roles)src.RoleId!).ToString()));
    }
}
