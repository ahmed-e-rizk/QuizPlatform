using System;
using System.Collections.Generic;

namespace QuizPlatform.Models;

public partial class ImageStorage
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? QuizId { get; set; }

    public string FuLlPath { get; set; } = null!;

    public decimal? FileSize { get; set; }

    public virtual Quiz? Quiz { get; set; }
}
