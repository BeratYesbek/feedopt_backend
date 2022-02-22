using AutoMapper;
using Entity.Concretes;
using Entity.Dtos;

namespace WebApi.AutoMapper.Profiles
{
    public class AdvertProfiles : Profile
    {
        public AdvertProfiles()
        {
            CreateMap<AdvertCreateDto, Advert>();
            CreateMap<AdvertCreateDto, AdvertImage>();
            CreateMap<AdvertCreateDto, Location>();

            CreateMap<AdvertUpdateDto, Advert>();
            CreateMap<AdvertUpdateDto, AdvertImage>();
            CreateMap<AdvertUpdateDto, Location>()
                .ForMember(dest => dest.Id, opt
                    => opt.MapFrom(src => src.LocationId));

        }
    }
}
