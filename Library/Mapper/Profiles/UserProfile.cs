using AutoMapper;
using Library.Data.Entities;
using Library.Models;

namespace Library.Mapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}