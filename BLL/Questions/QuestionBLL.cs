using AutoMapper;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Infrastructure;
using QuizPlatform.Models;
using QuizPlatform.Repository;
using QuizPlatform.Response;

namespace QuizPlatform.BLL.Questions
{
    public class QuestionBLL : IQuestionBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Question> _questionRepository;

        public QuestionBLL(IUnitOfWork unitOfWork, IMapper mapper,  IRepository<Question> questionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _questionRepository = questionRepository;
        }
        public async Task<IResponse<bool>> DeleteQuestionAsync(int QuestionId)
        {
            var response = new Response<bool>();
            try
            {
                var quiz = await _questionRepository.GetAsync(e => e.Id == QuestionId);
                _questionRepository.Delete(quiz);
                await _unitOfWork.CommitAsync();

                return response.CreateResponse(true);
            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }
        public async Task<IResponse<QuestionDto>> EditQuestionAsync(QuestionDto question)
        {
            var response = new Response<QuestionDto>();
            try
            {

                _questionRepository.Update(_mapper.Map<Models.Question>(question));
                await _unitOfWork.CommitAsync();

                return response.CreateResponse(question);
            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }

        public async Task<IResponse<CreateQuestionDto>> CreateQuestionAsync(CreateQuestionDto inputDto)
        {
            var response = new Response<CreateQuestionDto>();

            try
            {
                await _questionRepository.AddAsync(_mapper.Map<Models.Question>(inputDto));
                await _unitOfWork.CommitAsync();
                return response.CreateResponse(inputDto);

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }
        public async Task<IResponse<QuestionDto>> GetQuestionById(int id)
        {
            var response = new Response<QuestionDto>();


            try
            {
                var res = await _questionRepository.GetAsync(e => e.Id == id);
                return response.CreateResponse(_mapper.Map<QuestionDto>(res));

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }


        }

        public async Task<IResponse<List<QuestionDto>>> GetAllQuestionByQuizId(int id)
        {
            var response = new Response<List<QuestionDto>> ();

            try
            {
                var res =  _questionRepository.Where(e => e.QuizId == id);
                return response.CreateResponse(_mapper.Map<List<QuestionDto>>(res));

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }


        public async Task<IResponse<List<QuestionDto>>> AddQuestionToQuiz( List<QuestionDto> inputDtos)
        {
            var response = new Response<List<QuestionDto>> ();

            try
            {


                var res =  _questionRepository.Add(_mapper.Map<List<QuestionDto>>(inputDtos));


                return response.CreateResponse(_mapper.Map<List<QuestionDto>>(res));
            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }

        }
    }
}
