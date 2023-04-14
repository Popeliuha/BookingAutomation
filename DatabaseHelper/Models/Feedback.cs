using System;
using System.Collections.Generic;

namespace DatabaseHelper.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? OrderId { get; set; }

    public int StarsCount { get; set; }

    public string? Comment { get; set; }

    public virtual Order? Order { get; set; }
}
