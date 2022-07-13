using AutoMapper;
using Core.Entity;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Core.Utilities.Language;
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
