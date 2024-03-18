using System;
using System.Collections.Generic;

namespace Nextfliz.Models;

public partial class Actor
{
    public string ActorId { get; set; } = null!;

    public string? TenDienVien { get; set; }

    public string? HinhAnh { get; set; }

    public string? TieuSu { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
