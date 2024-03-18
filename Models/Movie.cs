using System;
using System.Collections.Generic;

namespace Nextfliz;

public partial class Movie
{
    public string MovieId { get; set; } = null!;

    public string? TenPhim { get; set; }

    public string? GenreId { get; set; }

    public int? ThoiLuong { get; set; }

    public int? NamPhatHanh { get; set; }

    public double? DiemDanhGia { get; set; }

    public string? DirectorId { get; set; }

    public string? HinhAnh { get; set; }

    public string? CertificationId { get; set; }

    public virtual Certification? Certification { get; set; }

    public virtual Director? Director { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<SuatChieu> SuatChieus { get; set; } = new List<SuatChieu>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();
}
