using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class FilmCast
{
    private string GenerateId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public FilmCast()
    {
        do
        {
            FilmCastId = GenerateId();
        } while (CheckDuplicateId(FilmCastId));
    }

    private bool CheckDuplicateId(string id)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.FilmCasts.Any(a => a.FilmCastId == id);
        }
    }
    public string FilmCastId { get; set; } = null!;

    public string? MovieId { get; set; }

    public string? ActorId { get; set; }

    public virtual Actor? Actor { get; set; }

    public virtual Movie? Movie { get; set; }
}
