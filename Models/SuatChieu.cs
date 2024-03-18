using System;
using System.Collections.Generic;
//using Nextfliz.Models;

namespace Nextfliz;

public partial class SuatChieu
{
    public string SuatChieuId { get; set; } = null!;

    public string? MovieId { get; set; }

    public DateTime? NgayGioChieu { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
