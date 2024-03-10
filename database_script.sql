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
    gioi_tinh NVARCHAR(10)
);

CREATE TABLE Genre (
    genre_id INT PRIMARY KEY,
    ten_the_loai NVARCHAR(100)
);

CREATE TABLE Certification (
    certification_id INT PRIMARY KEY,
    phan_loai NVARCHAR(50)
);

CREATE TABLE Actor (
    actor_id INT PRIMARY KEY,
    ten_dien_vien NVARCHAR(100),
    hinh_anh NVARCHAR(255),
    tieu_su NVARCHAR(MAX)
);

CREATE TABLE Director (
    director_id INT PRIMARY KEY,
    ten_dao_dien NVARCHAR(100),
    hinh_anh NVARCHAR(255),
    tieu_su NVARCHAR(MAX)
);

CREATE TABLE Movie (
    movie_id INT PRIMARY KEY,
    ten_phim NVARCHAR(255),
    the_loai_id INT FOREIGN KEY REFERENCES Genre(genre_id),
    thoi_luong INT,
    nam_phat_hanh INT,
    diem_danh_gia FLOAT,
    dao_dien_id INT FOREIGN KEY REFERENCES Director(director_id),
    hinh_anh NVARCHAR(255),
    phan_loai_id INT FOREIGN KEY REFERENCES Certification(certification_id)
);

CREATE TABLE SuatChieu (
    suat_chieu_id INT PRIMARY KEY,
    movie_id INT FOREIGN KEY REFERENCES Movie(movie_id),
    ngay_gio_chieu DATETIME
);

CREATE TABLE Ticket (
    ticket_id INT PRIMARY KEY,
    user_id NVARCHAR(50) FOREIGN KEY REFERENCES [User](username),
    movie_id INT FOREIGN KEY REFERENCES Movie(movie_id),
    ngay_dat_ve DATETIME,
    suat_chieu_id INT FOREIGN KEY REFERENCES SuatChieu(suat_chieu_id),
    gia_ve DECIMAL(10, 2),
    ghe_ngoi NVARCHAR(50)
);