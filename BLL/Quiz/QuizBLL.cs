using AutoMapper;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Infrastructure;
using QuizPlatform.Response;
using QuizPlatform.Models;

using System;
using QuizPlatform.Repository;

namespace QuizPlatform.BLL.Quiz
{
    public class QuizBLL : IQuizBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Models.Quiz> _quizRepository;

        public QuizBLL(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Models.Quiz> quizRepository) {
            _unitOfWork=unitOfWork;

            _mapper = mapper;
            _quizRepository=quizRepository;


        }


        public async Task<IResponse<QuizDto>> CreateQuiz(QuizDto QuizDto)
        {
            var response = new Response<QuizDto>();
            try {

                QuizDto.Questions = new List<QuestionDto>()
                {

                    new QuestionDto()
                    {QuestionText ="test",AnswerType =0,Answer =new AnswerDto(){ AnswerText=" text"} },

                     new QuestionDto()
                    {QuestionText ="test",AnswerType =1,Options =new List<OptionsDto> () { new OptionsDto() { OptionText ="jsjj"} } }};
            



            var mapped = _mapper.Map< Models.Quiz > (QuizDto);
             await   _quizRepository.AddAsync(mapped);
                await _unitOfWork.CommitAsync();
                return response.CreateResponse(QuizDto);
            
            } catch(Exception e) {
                return response.CreateResponse(e);
            
            }
        }
    }
}
