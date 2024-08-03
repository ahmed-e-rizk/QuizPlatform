using System.Text.Json.Serialization;

namespace QuizPlatform.DTO.Quiz
{
    public class QuizDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        [JsonIgnore]
        public IFormFile Image { get; set; }

        public DateTime? Date { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
