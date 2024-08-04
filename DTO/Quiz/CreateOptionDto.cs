namespace QuizPlatform.DTO.Quiz
{
    public class CreateOptionDto
    {
        public int Id { get; set; }

        public int? QuestionId { get; set; }

        public string OptionText { get; set; } = null!;
    }
}
