using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    public static void DeleteMovie(string id)
    {
        ObservableCollection<string> ids = new ObservableCollection<string>();
        using (var dbContext = new NextflizContext())
        {
            var suatChieus = dbContext.SuatChieus.Where(s => s.MovieId == id);
            foreach (SuatChieu suatChieu in suatChieus)
            {
                SuatChieu.DeleteSuatChieu(suatChieu.SuatChieuId);
            }
        }
        using (var dbContext = new NextflizContext())
        {
            var filmCasts = dbContext.FilmCasts.Where(f => f.MovieId == id);
            foreach (FilmCast filmCast in filmCasts)
            {
                dbContext.FilmCasts.Remove(filmCast);
            }

            var itemToDelete = dbContext.Movies.FirstOrDefault(a => a.MovieId == id);
            dbContext.Movies.Remove(itemToDelete);

            dbContext.SaveChanges();
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
}
