using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizPlatform.BLL;
using QuizPlatform.DTO.Auth;

namespace QuizPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthBLL _authBLL;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthController(IAuthBLL authBLL, TokenValidationParameters tokenValidationParameters)
        {
            _authBLL = authBLL;
            _tokenValidationParameters = tokenValidationParameters;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            var response = await _authBLL.RegisterAsync(registerDto);

            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var response = await _authBLL.LoginAsync(loginDto);

            return Ok(response);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var response = await _authBLL.RefreshTokenAsync(refreshTokenDto, _tokenValidationParameters);

            if (response.IsSuccess)
                return Ok(response);
            else
                return Unauthorized();
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync(LogoutDto logoutDto)
        {
            var response = await _authBLL.LogoutAsync(logoutDto);

            return Ok(response);
        }

    }
}
