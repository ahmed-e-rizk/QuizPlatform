namespace QuizPlatform.DTO.Quiz
{
    public class QuizResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Image { get; set; }

        public DateTime? Date { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
