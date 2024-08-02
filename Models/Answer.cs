using System;
using System.Collections.Generic;

namespace QuizPlatform.Models;

public partial class Answer
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public int? UserId { get; set; }

    public string? AnswerText { get; set; }

    public int? OptionId { get; set; }

    public virtual Option? Option { get; set; }

    public virtual Question? Question { get; set; }
}
