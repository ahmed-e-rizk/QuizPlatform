using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPlatform.BLL.Questions;
using QuizPlatform.BLL.Quizs;
using QuizPlatform.DTO.Quiz;

namespace QuizPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionBLL _questionBLL;

        public QuestionController(IQuestionBLL questionBLL)
        {
            _questionBLL = questionBLL;
        }

        //[HttpPost("CreateQuestionAsync")]
        //public async Task<IActionResult> CreateQuestionAsync(CreateQuestionDto inputDto)
        //{
        //    var response = await _questionBLL.CreateQuestionAsync(inputDto);

        //    return Ok(response);
        //}
        [HttpPost("DeleteQuestionAsync")]
        public async Task<IActionResult> DeleteQuestionAsync(int QuestionId)
        {
            var response = await _questionBLL.DeleteQuestionAsync(QuestionId);

            return Ok(response);
        }
        [HttpPatch("EditQuestionAsync")]
        public async Task<IActionResult> EditQuestionAsync(QuestionDto question)
        {
            var response = await _questionBLL.EditQuestionAsync(question);
            return Ok(response);
        }
        [HttpGet("GetQuestionById")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var response = await _questionBLL.GetQuestionById(id);
            return Ok(response);
        }
        [HttpPost("GetAllQuestionByQuizId")]
        public async Task<IActionResult> GetAllQuestionByQuizId(int id )
        {
            var response = await _questionBLL.GetAllQuestionByQuizId(id);
            return Ok(response);
        }
        [HttpPost("AddQuestionToQuiz")]
        public async Task<IActionResult> AddQuestionToQuiz([FromBody]List<QuestionDto> inputDtos)
        {
            var response = await _questionBLL.AddQuestionToQuiz(inputDtos);
            return Ok(response);
        }
    }
}
