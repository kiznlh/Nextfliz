using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Nextfliz.Models;

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

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<FilmCast> FilmCasts { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<SuatChieu> SuatChieus { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    private string connectionString = ConfigurationManager.ConnectionStrings["NextlizContext"].ConnectionString;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__8B2447B41C0182B4");

            entity.ToTable("Actor");

            entity.Property(e => e.ActorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actor_id");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("hinh_anh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("ten_dien_vien");
            entity.Property(e => e.TieuSu).HasColumnName("tieu_su");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__F5205E498E6BE751");

            entity.ToTable("Director");

            entity.Property(e => e.DirectorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("director_id");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("hinh_anh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("ten_dao_dien");
            entity.Property(e => e.TieuSu).HasColumnName("tieu_su");
        });

        modelBuilder.Entity<FilmCast>(entity =>
        {
            entity.HasKey(e => e.FilmCastId).HasName("PK__FilmCast__06E8FBBB6468B3EF");

            entity.ToTable("FilmCast");

            entity.Property(e => e.FilmCastId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("film_cast_id");
            entity.Property(e => e.ActorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("actor_id");
            entity.Property(e => e.MovieId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movie_id");

            entity.HasOne(d => d.Actor).WithMany(p => p.FilmCasts)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK__FilmCast__actor___4D94879B");

            entity.HasOne(d => d.Movie).WithMany(p => p.FilmCasts)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__FilmCast__movie___4CA06362");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__18428D423228937F");

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
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__83CDF7490A765591");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.Certification)
                .HasMaxLength(20)
                .HasColumnName("certification");
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

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__Movie__director___403A8C7D");

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Movie__genre_id__3F466844");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => new { e.SuatChieuId, e.ViTriGhe }).HasName("PK__Seat__D37C11B1F7A8D503");

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
                .HasConstraintName("FK__Seat__suat_chieu__5070F446");
        });

        modelBuilder.Entity<SuatChieu>(entity =>
        {
            entity.HasKey(e => e.SuatChieuId).HasName("PK__SuatChie__8BDAEB5B4F9379D9");

            entity.ToTable("SuatChieu");

            entity.Property(e => e.SuatChieuId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("suat_chieu_id");
            entity.Property(e => e.GiaVe)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gia_ve");
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
                .HasConstraintName("FK__SuatChieu__movie__4316F928");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__D596F96B29AC535E");

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
                .HasConstraintName("FK__Ticket__movie_id__46E78A0C");

            entity.HasOne(d => d.SuatChieu).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SuatChieuId)
                .HasConstraintName("FK__Ticket__suat_chi__47DBAE45");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Ticket__username__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__User__F3DBC5733D9E00F6");

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
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__80B6FFA865D1D970");

            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("voucher_id");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");
            entity.Property(e => e.TenVoucher)
                .HasMaxLength(100)
                .HasColumnName("ten_voucher");
            entity.Property(e => e.TiLeGiam).HasColumnName("ti_le_giam");
        });

        modelBuilder.Entity<VoucherUsage>(entity =>
        {
            entity.HasKey(e => new { e.VoucherId, e.TicketId }).HasName("PK__VoucherU__2DEF903E58B1CF9C");

            entity.ToTable("VoucherUsage");

            entity.Property(e => e.VoucherId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("voucher_id");
            entity.Property(e => e.TicketId)
                .HasMaxLength(20)
                .HasColumnName("ticket_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}