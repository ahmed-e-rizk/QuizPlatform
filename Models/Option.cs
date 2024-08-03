using System;
using System.Collections.Generic;

namespace QuizPlatform.Models;

public partial class Option
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public bool IsChecked { get; set; }

    public virtual Answer Answers { get; set; } = new Answer();

    public virtual Question? Question { get; set; }
}
