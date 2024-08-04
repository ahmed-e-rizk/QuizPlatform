using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPlatform.BLL;
using QuizPlatform.BLL.Quizs;
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
        public async Task<IActionResult> CreateQuizAsync([FromForm] QuizDto quizDto)
        {
            var response = await _quizBLL.CreateQuiz(quizDto);

            return Ok(response);
        }
        [HttpPost("DeleteQuiz")]
        public async Task<IActionResult> DeleteQuizAsync(int QuizId)
        {
            var response = await _quizBLL.DeleteQuizAsync(QuizId);

            return Ok(response);
        }

        [HttpPatch("EditQuizAsync")]
        public async Task<IActionResult> EditQuizAsync(QuizDto QuizDto)
        {
            var response = await _quizBLL.EditQuizAsync(QuizDto);

            return Ok(response);
        }

        [HttpGet("GetQuizById")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var response = await _quizBLL.GetQuizById(id);
            return Ok(response);
        }
        [HttpGet("GetAllQuiz")]
        public async Task<IActionResult> GetAllQuiz(int id)
        {
            var response = await _quizBLL.GetAllQuiz();
            return Ok(response);
        }
    }
}
