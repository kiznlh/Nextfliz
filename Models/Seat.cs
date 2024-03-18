using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Seat
{
    public string SuatChieuId { get; set; } = null!;

    public string ViTriGhe { get; set; } = null!;

    public virtual SuatChieu SuatChieu { get; set; } = null!;
}
