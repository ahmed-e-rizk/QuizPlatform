using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPlatform.BLL.Options;
using QuizPlatform.BLL.Quizs;
using QuizPlatform.DTO.Quiz;

namespace QuizPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionBLL _optionBLL;

        public OptionController(IOptionBLL optionBLL)
        {
            _optionBLL = optionBLL;
        }

        [HttpPost("CreateOptionAsync")]
        public async Task<IActionResult> CreateOptionAsync(CreateOptionDto inputDto)
        {
            var response = await _optionBLL.CreateOptionAsync(inputDto);

            return Ok(response);
        }
        [HttpPost("DeleteOptionAsync")]
        public async Task<IActionResult> DeleteOptionAsync(int Id)
        {
            var response = await _optionBLL.DeleteOptionAsync(Id);

            return Ok(response);
        }
        [HttpPatch("EditOptionAsync")]
        public async Task<IActionResult> EditOptionAsync(CreateOptionDto inputDto)
        {
            var response = await _optionBLL.EditOptionAsync(inputDto);
            return Ok(response);
        }
        [HttpGet("GetOptionById")]
        public async Task<IActionResult> GetOptionById(int id)
        {
            var response = await _optionBLL.GetOptionById(id);
            return Ok(response);
        }
    }
}
