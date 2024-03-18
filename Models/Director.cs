using System;
using System.Collections.Generic;

namespace Nextfliz.Models;

public partial class Director
{
    public string DirectorId { get; set; } = null!;

    public string? TenDaoDien { get; set; }

    public string? HinhAnh { get; set; }

    public string? TieuSu { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
