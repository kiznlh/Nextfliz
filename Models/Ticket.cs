using System;
using System.Collections.Generic;
using System.IO;

namespace Nextfliz;

public partial class Ticket
{
    private string GenerateDirectorId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Ticket()
    {
        do
        {
            TicketId = GenerateDirectorId();
        } while (CheckDuplicateId(TicketId));
    }

    private bool CheckDuplicateId(string TicketId)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.Tickets.Any(d => d.TicketId == TicketId);
        }
    }
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
