using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class VoucherUsage
{
    private string GenerateId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public VoucherUsage()
    {
        do
        {
            UsageId = GenerateId();
        } while (CheckDuplicateId(UsageId));
    }

    private bool CheckDuplicateId(string id)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.VoucherUsages.Any(a => a.UsageId == id);
        }
    }
    public string VoucherId { get; set; } = null!;

    public string TicketId { get; set; } = null!;

    public string UsageId { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
