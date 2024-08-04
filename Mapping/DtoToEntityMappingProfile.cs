using AutoMapper;
using QuizPlatform.DTO.Auth;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Helder;
using QuizPlatform.Models;

namespace QuizPlatform.Mapping
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<QuizDto, Quiz>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(e => e.Date))
           .ForMember(dest => dest.ImageStorages, opt => opt.MapFrom<ImageStorageNameResolver,IFormFile>(e=> e.Image))
            ;
            CreateMap<QuestionDto, Question>()
                                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(e => e.QuestionText))
                                 .ForMember(dest => dest.Answers, opt => opt.MapFrom(e => e.AnswerType == 0 ? e.Answer : new())).ForMember(dest => dest.Options, opt => opt.MapFrom(e => e.AnswerType == 1 ? e.Options : new()));

            CreateMap<AnswerDto, Answer>()
                                           .ForMember(dest => dest.AnswerText, opt => opt.MapFrom(e => e.AnswerText));
            CreateMap<OptionsDto, Option>()
                                      .ForMember(dest => dest.OptionText, opt => opt.MapFrom(e => e.OptionText));

            CreateMap<CreateQuestionDto, Question>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(e => e.QuestionText))
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(e => e.AnswerType == 0 ? e.Answer : new()))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(e => e.AnswerType == 1 ? e.Options : new()))
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(e => e.QuizId));
;
            CreateMap<CreateOptionDto, Option>();
        }
    }
}
