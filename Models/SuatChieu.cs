﻿using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class SuatChieu
{
    private string GenerateId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public SuatChieu()
    {
        do
        {
            SuatChieuId = GenerateId();
        } while (CheckDuplicateId(SuatChieuId));
    }

    private bool CheckDuplicateId(string id)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.SuatChieus.Any(a => a.SuatChieuId == id);
        }
    }

    public static void DeleteSuatChieu(string id)
    {
        using (var dbContext = new NextflizContext())
        {

            var tickets = dbContext.Tickets.Where(s => s.SuatChieuId == id);
            foreach (var ticket in tickets)
            {
                using (var context2 = new NextflizContext())
                {
                    var usage = context2.VoucherUsages.Where(s => s.TicketId == ticket.TicketId);
                    context2.VoucherUsages.RemoveRange(usage);
                    context2.SaveChanges();
                }
                
                dbContext.Tickets.Remove(ticket);
            }

            var itemToDelete = dbContext.SuatChieus.FirstOrDefault(s => s.SuatChieuId == id);
            dbContext.SuatChieus.Remove(itemToDelete);
            dbContext.SaveChanges();
        }
    }

    public string SuatChieuId { get; set; } = null!;

    public string? MovieId { get; set; }

    public DateTime? NgayGioChieu { get; set; }

    public decimal? GiaVe { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
