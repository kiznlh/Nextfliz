using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Movie
{
    private string GenerateMovieId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Movie()
    {
        do
        {
            MovieId = GenerateMovieId();
        } while (CheckDuplicateId(MovieId));
    }

    private bool CheckDuplicateId(string id)
    {
        using (var dbContext = new NextflizContext())
        {
            return dbContext.Movies.Any(a => a.MovieId == id);
        }
    }
    public string MovieId { get; set; } = null!;

    public string? TenPhim { get; set; }

    public string? GenreId { get; set; }

    public int? ThoiLuong { get; set; }

    public int? NamPhatHanh { get; set; }

    public double? DiemDanhGia { get; set; }

    public string? DirectorId { get; set; }

    public string? HinhAnh { get; set; }

    public string? Certification { get; set; }

    public virtual Director? Director { get; set; }

    public virtual ICollection<FilmCast> FilmCasts { get; set; } = new List<FilmCast>();

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<SuatChieu> SuatChieus { get; set; } = new List<SuatChieu>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
