using QuizPlatform.DTO.Auth;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Response;

namespace QuizPlatform.BLL.Quizs
{
    public interface IQuizBLL
    {
        Task<IResponse<bool>> CreateQuiz(QuizDto QuizDto);
        Task<IResponse<bool>> DeleteQuizAsync(int QuizId);
        Task<IResponse<QuizResultDto>> EditQuizAsync(QuizDto QuizDto);
        Task<IResponse<QuizResultDto>> GetQuizById(int id);
        Task<IResponse<List<QuizResultDto>>> GetAllQuiz();
    }
}
