using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class VoucherUsage
{
    private string GenerateMovieId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public VoucherUsage()
    {
        do
        {
            VoucherId = GenerateMovieId();
        } while (CheckDuplicateId(VoucherId));
    }

    private bool CheckDuplicateId(string id)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.Movies.Any(a => a.MovieId == id);
        }
    }
    public string VoucherId { get; set; } = null!;

    public string TicketId { get; set; } = null!;

    public string UsageId { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
