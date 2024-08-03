using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPlatform.BLL;
using QuizPlatform.BLL.Quiz;
using QuizPlatform.DTO.Auth;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Models;

namespace QuizPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : BaseController
    {
        private readonly IQuizBLL _quizBLL;

        public QuizController(IQuizBLL quizBLL)
        {
            _quizBLL = quizBLL;
        }

        [HttpPost("CreateQuiz")]
        public async Task<IActionResult> CreateQuizAsync([FromQuery] QuizDto quizDto)
        {
            var response = await _quizBLL.CreateQuiz(quizDto);

            return Ok(response);
        }
    }
}
