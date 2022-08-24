using AutoMapper;
using Core.Entity.Concretes;
using Entity.Dtos;

namespace WebApi.AutoMapper.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<UserUpdateDto, User>();
        }
    }
}