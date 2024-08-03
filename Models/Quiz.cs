using System;
using System.Collections.Generic;

namespace QuizPlatform.Models;

public partial class Quiz
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? ImageId { get; set; }

    public DateTime? Date { get; set; }

    public virtual ImageStorage ImageStorages { get; set; } = new ImageStorage();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
