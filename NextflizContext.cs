using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using Nextfliz.Models;

namespace Nextfliz;

public partial class NextflizContext : DbContext
{
    public NextflizContext()
    {
    }

    public NextflizContext(DbContextOptions<NextflizContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<SuatChieu> SuatChieus { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NTP7525;Initial Catalog = nextfliz; Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=True;Application Intent = ReadWrite; Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__8B2447B49735FF8F");

            entity.ToTable("Actor");

            entity.Property(e => e.ActorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actor_id");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("hinh_anh");
            entity.Property(e => e.TenDienVien)
                .HasMaxLength(100)
                .HasColumnName("ten_dien_vien");
            entity.Property(e => e.TieuSu).HasColumnName("tieu_su");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.HasKey(e => e.CertificationId).HasName("PK__Certific__185D5AEC0BEC115F");

            entity.ToTable("Certification");

            entity.Property(e => e.CertificationId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("certification_id");
            entity.Property(e => e.PhanLoai)
                .HasMaxLength(50)
                .HasColumnName("phan_loai");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__F5205E497882EDDF");

            entity.ToTable("Director");

            entity.Property(e => e.DirectorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("director_id");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("hinh_anh");
            entity.Property(e => e.TenDaoDien)
                .HasMaxLength(100)
                .HasColumnName("ten_dao_dien");
            entity.Property(e => e.TieuSu).HasColumnName("tieu_su");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__18428D42039B4A04");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genre_id");
            entity.Property(e => e.TenTheLoai)
                .HasMaxLength(100)
                .HasColumnName("ten_the_loai");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__83CDF74961A64D44");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.CertificationId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("certification_id");
            entity.Property(e => e.DiemDanhGia).HasColumnName("diem_danh_gia");
            entity.Property(e => e.DirectorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("director_id");
            entity.Property(e => e.GenreId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genre_id");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("hinh_anh");
            entity.Property(e => e.NamPhatHanh).HasColumnName("nam_phat_hanh");
            entity.Property(e => e.TenPhim)
                .HasMaxLength(255)
                .HasColumnName("ten_phim");
            entity.Property(e => e.ThoiLuong).HasColumnName("thoi_luong");

            entity.HasOne(d => d.Certification).WithMany(p => p.Movies)
                .HasForeignKey(d => d.CertificationId)
                .HasConstraintName("FK__Movie__certifica__4316F928");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__Movie__director___4222D4EF");

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Movie__genre_id__412EB0B6");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmCast",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FilmCast__actor___5165187F"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FilmCast__movie___5070F446"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PK__FilmCast__DB7FB332B29DD546");
                        j.ToTable("FilmCast");
                        j.IndexerProperty<string>("MovieId")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("movie_id");
                        j.IndexerProperty<string>("ActorId")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("actor_id");
                    });
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => new { e.SuatChieuId, e.ViTriGhe }).HasName("PK__Seat__D37C11B125B073EB");

            entity.ToTable("Seat");

            entity.Property(e => e.SuatChieuId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("suat_chieu_id");
            entity.Property(e => e.ViTriGhe)
                .HasMaxLength(5)
                .HasColumnName("vi_tri_ghe");

            entity.HasOne(d => d.SuatChieu).WithMany(p => p.Seats)
                .HasForeignKey(d => d.SuatChieuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seat__suat_chieu__5441852A");
        });

        modelBuilder.Entity<SuatChieu>(entity =>
        {
            entity.HasKey(e => e.SuatChieuId).HasName("PK__SuatChie__8BDAEB5B381077AD");

            entity.ToTable("SuatChieu");

            entity.Property(e => e.SuatChieuId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("suat_chieu_id");
            entity.Property(e => e.MovieId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.NgayGioChieu)
                .HasColumnType("datetime")
                .HasColumnName("ngay_gio_chieu");

            entity.HasOne(d => d.Movie).WithMany(p => p.SuatChieus)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__SuatChieu__movie__45F365D3");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__D596F96BA54CE274");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId)
                .HasMaxLength(20)
                .HasColumnName("ticket_id");
            entity.Property(e => e.GiaVe)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gia_ve");
            entity.Property(e => e.MovieId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.NgayDatVe)
                .HasColumnType("datetime")
                .HasColumnName("ngay_dat_ve");
            entity.Property(e => e.SuatChieuId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("suat_chieu_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.ViTriGhe)
                .HasMaxLength(5)
                .HasColumnName("vi_tri_ghe");

            entity.HasOne(d => d.Movie).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Ticket__movie_id__49C3F6B7");

            entity.HasOne(d => d.SuatChieu).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SuatChieuId)
                .HasConstraintName("FK__Ticket__suat_chi__4AB81AF0");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Ticket__username__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__User__F3DBC57369308E55");

            entity.ToTable("User");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .HasColumnName("gioi_tinh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("ho_ten");
            entity.Property(e => e.NgaySinh).HasColumnName("ngay_sinh");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__80B6FFA89AD4F958");

            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("voucher_id");
            entity.Property(e => e.MovieId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.TiLeGiam).HasColumnName("ti_le_giam");

            entity.HasOne(d => d.Movie).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Voucher__movie_i__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
