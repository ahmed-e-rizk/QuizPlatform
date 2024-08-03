using QuizPlatform.DTO.Auth;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Response;

namespace QuizPlatform.BLL.Quiz
{
    public interface IQuizBLL
    {
        Task<IResponse<QuizDto>> CreateQuiz(QuizDto QuizDto);

    }
}
