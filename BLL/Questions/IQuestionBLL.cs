using QuizPlatform.DTO.Quiz;
using QuizPlatform.Response;

namespace QuizPlatform.BLL.Questions
{
    public interface IQuestionBLL
    {
        Task<IResponse<bool>> DeleteQuestionAsync(int QuestionId);
     //   Task<IResponse<CreateQuestionDto>> CreateQuestionAsync(CreateQuestionDto inputDto);
        Task<IResponse<QuestionDto>> EditQuestionAsync(QuestionDto question);
        Task<IResponse<QuestionDto>> GetQuestionById(int id);
        Task<IResponse<List<QuestionDto>>> GetAllQuestionByQuizId(int id);
        Task<IResponse<List<QuestionDto>>>AddQuestionToQuiz( List<QuestionDto> inputDtos);

    }
}
