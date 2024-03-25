using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Ticket
{
    public string TicketId { get; set; } = null!;

    public string? Username { get; set; }

    public string? MovieId { get; set; }

    public DateTime? NgayDatVe { get; set; }

    public string? SuatChieuId { get; set; }

    public decimal? GiaVe { get; set; }

    public string? ViTriGhe { get; set; }

    public bool? VoucherSinhNhat { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual SuatChieu? SuatChieu { get; set; }

    public virtual User? UsernameNavigation { get; set; }

    public virtual ICollection<VoucherUsage> VoucherUsages { get; set; } = new List<VoucherUsage>();
}
