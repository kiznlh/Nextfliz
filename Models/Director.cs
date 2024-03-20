using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Director
{
    private string GenerateDirectorId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Director()
    {
        do
        {
            DirectorId = GenerateDirectorId();
        } while (CheckDuplicateDirectorId(DirectorId));
    }

    private bool CheckDuplicateDirectorId(string directorId)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.Directors.Any(d => d.DirectorId == directorId);
        }
    }
    public string DirectorId { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? HinhAnh { get; set; }

    public string? TieuSu { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
