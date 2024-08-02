using Microsoft.IdentityModel.Tokens;
using QuizPlatform.DTO.Auth;
using QuizPlatform.Enums;
using QuizPlatform.Response;
using QuizPlatform.Validtion.Auth;

namespace QuizPlatform.BLL
{
    public interface IAuthBLL
    {
        Task<IResponse<bool>> RegisterAsync(RegisterDto registerDto);
        Task<IResponse<TokenResultDto>> LoginAsync(LoginDto loginDto);
        Task<IResponse<TokenResultDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto, TokenValidationParameters tokenValidationParameters);
        Task<IResponse<bool>> LogoutAsync(LogoutDto logoutDto);

    }
}