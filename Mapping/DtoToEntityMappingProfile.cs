using AutoMapper;
using QuizPlatform.DTO.Auth;
using QuizPlatform.Models;

namespace QuizPlatform.Mapping
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<RegisterDto, User>();

        }
    }
}
