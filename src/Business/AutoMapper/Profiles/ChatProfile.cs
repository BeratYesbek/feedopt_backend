using AutoMapper;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.AutoMapper.Profiles;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<Chat, ChatDto>().ForPath(dest => dest.Chat, opt => opt.MapFrom(src => src));

        //ForMember(dest => dest, opt => opt.MapFrom(src => src.Chat));
    }
}