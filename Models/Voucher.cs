using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Voucher
{
    public string VoucherId { get; set; } = null!;

    public int? SoLuong { get; set; }

    public double? TiLeGiam { get; set; }
}
