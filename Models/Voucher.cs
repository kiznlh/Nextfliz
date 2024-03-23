using System;
using System.Collections.Generic;

namespace Nextfliz.Models;

public partial class Voucher
{
    private string GenerateId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Voucher()
    {
        do
        {
            VoucherId = GenerateId();
        } while (CheckDuplicateId(VoucherId));
    }

    private bool CheckDuplicateId(string id)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.Vouchers.Any(a => a.VoucherId == id);
        }
    }
    public string VoucherId { get; set; } = null!;

    public string? TenVoucher { get; set; }

    public int? SoLuong { get; set; }

    public double? TiLeGiam { get; set; }
}
