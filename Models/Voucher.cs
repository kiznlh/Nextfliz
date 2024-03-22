using System;
using System.Collections.Generic;

namespace Nextfliz.Models;

public partial class Voucher
{
    public string VoucherId { get; set; } = null!;

    public string? TenVoucher { get; set; }

    public int? SoLuong { get; set; }

    public double? TiLeGiam { get; set; }
}
