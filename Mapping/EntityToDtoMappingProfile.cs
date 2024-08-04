using AutoMapper;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizPlatform.Mapping
{
    public class EntityToDtoMappingProfile : Profile
    {

        public EntityToDtoMappingProfile()
        {

            CreateMap<Quiz, QuizResultDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(e => e.Date))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(e => e.ImageStorages.FuLlPath));


            
            CreateMap<Question,QuestionDto>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(e => e.QuestionText))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(e => e.AnswerType == 0 ? e.Answers : null))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(e => e.AnswerType == 1 ? e.Options : null));

            CreateMap<Answer , AnswerDto>()
                .ForMember(dest => dest.AnswerText, opt => opt.MapFrom(e => e.AnswerText));
            CreateMap<Option, OptionsDto > ()
                .ForMember(dest => dest.OptionText, opt => opt.MapFrom(e => e.OptionText));
            CreateMap<Option, OptionResultDto>();
        }
}
}
