using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Genre
{
    public string GenreId { get; set; } = null!;

    public string? TenTheLoai { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
