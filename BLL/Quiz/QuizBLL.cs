using AutoMapper;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Infrastructure;
using QuizPlatform.Response;
using QuizPlatform.Models;

using System;
using QuizPlatform.Repository;
using Microsoft.EntityFrameworkCore;

namespace QuizPlatform.BLL.Quiz
{
    public class QuizBLL : IQuizBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Models.Quiz> _quizRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Option> _optionRepository;

        public QuizBLL(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Models.Quiz> quizRepository, IRepository<Question> questionRepository, IRepository<Answer> answerRepository , IRepository<Option> optionRepository) {
            _unitOfWork=unitOfWork;

            _mapper = mapper;
            _quizRepository=quizRepository;
            _questionRepository=questionRepository;
            _answerRepository=answerRepository;
            _optionRepository = optionRepository;
        }

        public async Task<IResponse<bool>> DeleteQuizAsync(int QuizId)
        {
            var response = new Response<bool>();
            try
            {


              

                var quize = await _quizRepository.GetAsync(e => e.Id == QuizId);

                _quizRepository.Delete(quize);
                await _unitOfWork.CommitAsync();

                if (quize != null)
                {
                    var questions = _questionRepository.Where(e => e.QuizId == QuizId);
                    if(questions.Any())
                    {
                        var answers = _answerRepository.Where(e => questions.Select(e => e.Id).Contains(e.QuestionId??0));
                        var options = _optionRepository.Where(e => questions.Select(e => e.Id).Contains(e.QuestionId ?? 0));
                    }
                }





                //var mapped = _mapper.Map<Models.Quiz>(QuizDto);
                //await _quizRepository.AddAsync(mapped);
                //await _unitOfWork.CommitAsync();
                return response.CreateResponse();

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
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
