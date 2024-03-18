using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Certification
{
    public string CertificationId { get; set; } = null!;

    public string? PhanLoai { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
