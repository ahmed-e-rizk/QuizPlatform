namespace QuizPlatform.DTO.Quiz
{
    public class QuestionDto
    {
        public string QuestionText { get; set; }
        public int AnswerType { get; set; }
        public AnswerDto? Answer { get; set; }
        public List<OptionsDto>? Options { get; set; }
    }


}
