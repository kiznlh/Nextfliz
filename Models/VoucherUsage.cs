using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class VoucherUsage
{
    public string VoucherId { get; set; } = null!;

    public string TicketId { get; set; } = null!;

    public string UsageId { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
