USE master
GO

IF DB_ID('nextfliz') IS NOT NULL
	DROP DATABASE nextfliz
GO 

CREATE DATABASE nextfliz
GO

USE nextfliz
GO

CREATE TABLE [User] (
    username NVARCHAR(50) PRIMARY KEY,
    password NVARCHAR(255),
    ho_ten NVARCHAR(100),
    ngay_sinh DATE,
    gioi_tinh NVARCHAR(10),
    role NVARCHAR(10)
);

CREATE TABLE Genre (
    genre_id CHAR(5) PRIMARY KEY,
    ten_the_loai NVARCHAR(100)
);

CREATE TABLE Actor (
    actor_id CHAR(5) PRIMARY KEY,
    ten_dien_vien NVARCHAR(100),
    hinh_anh NVARCHAR(255),
    tieu_su NVARCHAR(MAX)
);

CREATE TABLE Director (
    director_id CHAR(5) PRIMARY KEY,
    ten_dao_dien NVARCHAR(100),
    hinh_anh NVARCHAR(255),
    tieu_su NVARCHAR(MAX)
);

CREATE TABLE Movie (
    movie_id CHAR(5) PRIMARY KEY,
    ten_phim NVARCHAR(255),
    genre_id CHAR(5) FOREIGN KEY REFERENCES Genre(genre_id),
    thoi_luong INT,
    nam_phat_hanh INT,
    diem_danh_gia FLOAT,
    director_id CHAR(5) FOREIGN KEY REFERENCES Director(director_id),
    hinh_anh NVARCHAR(255),
    certification NVARCHAR(20)
);

CREATE TABLE SuatChieu (
    suat_chieu_id CHAR(5) PRIMARY KEY,
    movie_id CHAR(5) FOREIGN KEY REFERENCES Movie(movie_id),
    ngay_gio_chieu DATETIME,
    gia_ve DECIMAL(10, 2),/*gia ve goc cua suat chieu*/
);

CREATE TABLE Ticket (
    ticket_id NVARCHAR(20) PRIMARY KEY,
    username NVARCHAR(50) FOREIGN KEY REFERENCES [User](username),
    movie_id CHAR(5) FOREIGN KEY REFERENCES Movie(movie_id),
    ngay_dat_ve DATETIME,
    suat_chieu_id CHAR(5) FOREIGN KEY REFERENCES SuatChieu(suat_chieu_id),
	gia_ve DECIMAL(10, 2),/*gia ve sau cung(da ap dung voucher)*/
    vi_tri_ghe NVARCHAR(5),
	voucher_sinh_nhat BIT
);

CREATE TABLE Voucher (
    voucher_id CHAR(5) PRIMARY KEY,
    ten_voucher NVARCHAR(100),
	so_luong INT,
    ti_le_giam FLOAT
);

CREATE TABLE FilmCast (
    film_cast_id CHAR(10) PRiMARY KEY,
    movie_id CHAR(5),
    actor_id CHAR(5),
    FOREIGN KEY (movie_id) REFERENCES Movie(movie_id),
    FOREIGN KEY (actor_id) REFERENCES Actor(actor_id)
);

CREATE TABLE Seat (
    suat_chieu_id CHAR(5),
    vi_tri_ghe NVARCHAR(5),
    PRIMARY KEY (suat_chieu_id, vi_tri_ghe),
    FOREIGN KEY (suat_chieu_id) REFERENCES SuatChieu(suat_chieu_id)
);

CREATE TABLE VoucherUsage (
    voucher_id CHAR(5),
	ticket_id NVARCHAR(20),
	PRIMARY KEY (voucher_id, ticket_id),
	FOREIGN KEY (voucher_id) REFERENCES Voucher(voucher_id),
	FOREIGN KEY (ticket_id) REFERENCES Ticket(ticket_id),
);