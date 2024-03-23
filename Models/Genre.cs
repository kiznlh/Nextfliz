using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Genre
{
    private string GenerateGenreId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Genre()
    {
        do
        {
            GenreId = GenerateGenreId();
        } while (CheckDuplicateId(GenreId));
    }

    private bool CheckDuplicateId(string id)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.Genres.Any(a => a.GenreId == id);
        }
    }

    public string GenreId { get; set; } = null!;

    public string? TenTheLoai { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
