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
            CreateMap<AdvertUpdateDto, AdvertImage>();

            CreateMap<AdvertUpdateDto, Location>()
                .ForMember(dest => dest.Id, opt
                    => opt.MapFrom(src => src.LocationId));

            CreateMap<AdvertUpdateDto, Advert>()
                .ForMember(dest => dest.Description, 
                    src =>
                        src.Condition((src,dest) => dest.Description != null))
                .ForMember(dest => dest.AnimalName, 
                    src => src.Condition(t => t.AnimalName != null));


        }

    }
}
