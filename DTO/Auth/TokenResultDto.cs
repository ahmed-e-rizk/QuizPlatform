namespace QuizPlatform.DTO.Auth
{
    public class TokenResultDto
    {

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
    public class RefreshTokenDto
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
