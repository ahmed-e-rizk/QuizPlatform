using System;
using System.Collections.Generic;

namespace QuizPlatform.Models;

public partial class Option
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public bool IsChecked { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Question? Question { get; set; }
}
