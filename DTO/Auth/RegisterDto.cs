using System.Text.Json.Serialization;

namespace QuizPlatform.DTO.Auth
{
    public class RegisterDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Mobile { get; set; }
        [JsonIgnore]
        public bool? IsAdmin { get; set; }=false;
    }
}
