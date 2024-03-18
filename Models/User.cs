using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class User
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? HoTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
