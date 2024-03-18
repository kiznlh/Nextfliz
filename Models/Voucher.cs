using System;
using System.Collections.Generic;

namespace Nextfliz.Models;

public partial class Voucher
{
    public string VoucherId { get; set; } = null!;

    public string? MovieId { get; set; }

    public double? TiLeGiam { get; set; }

    public virtual Movie? Movie { get; set; }
}
