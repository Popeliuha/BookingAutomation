using System;
using System.Collections.Generic;

namespace DatabaseHelper.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? HotelId { get; set; }

    public int? UserId { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Hotel? Hotel { get; set; }

    public virtual User? User { get; set; }
}
