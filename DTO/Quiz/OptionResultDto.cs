namespace QuizPlatform.DTO.Quiz
{
    public class OptionResultDto
    {
        public int Id { get; set; }

        public int? QuestionId { get; set; }

        public string OptionText { get; set; } = null!;

        public bool IsChecked { get; set; }
    }
}
