using System;
using System.Collections.Generic;

namespace QuizPlatform.Models;

public partial class Question
{
    public int Id { get; set; }

    public int? QuizId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int AnswerType { get; set; }

    public virtual Answer Answers { get; set; } = new Answer();

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual Quiz? Quiz { get; set; }
}
