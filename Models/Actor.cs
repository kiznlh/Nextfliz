using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Actor
{
    private string GenerateActorId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Actor()
    {
        do
        {
            ActorId = GenerateActorId();
        } while (CheckDuplicateActorId(ActorId));
    }

    private bool CheckDuplicateActorId(string actorId)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.Actors.Any(a => a.ActorId == actorId);
        }
    }
    public string ActorId { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? HinhAnh { get; set; }

    public string? TieuSu { get; set; }

    public virtual ICollection<FilmCast> FilmCasts { get; set; } = new List<FilmCast>();
}
