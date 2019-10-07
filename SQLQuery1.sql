use master
Drop Database QL_Web_1D2C
----------
create database QL_Web_1D2C
GO
use QL_Web_1D2C
GO
CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1),
	HoTen nVarchar(50) NOT NULL,
	Taikhoan Varchar(50) UNIQUE,
	Matkhau Varchar(50) NOT NULL,
	Email Varchar(100) UNIQUE,
	DiachiKH nVarchar(200),
	DienthoaiKH Varchar(50),	
	Ngaysinh DATETIME
	CONSTRAINT PK_Khachhang PRIMARY KEY(MaKH)
)
GO
Create Table NHASAXUAT
(
	MaNSX int identity(1,1),
	TenNSX nvarchar(50) NOT NULL,
	Diachi NVARCHAR(200),
	DienThoai VARCHAR(50),
	CONSTRAINT PK_NhaSanXuat PRIMARY KEY(MaNSX)
)
GO
Create Table LOAI
(
	MaLoai int Identity(1,1),
	TenLoai nvarchar(50) NOT NULL,
	MaNSX INT,
	CONSTRAINT PK_Loai PRIMARY KEY(MaLoai),
	CONSTRAINT FK_NSX FOREIGN KEY(MaNSX) REFERENCES NHASAXUAT(MaNSX)
)
GO
create TABLE SANPHAM
(
	Masp INT IDENTITY(1,1),
	Tensp NVARCHAR(100) NOT NULL,
	Giaban Decimal(18,0) CHECK (Giaban>=0),
	Mota NVarchar(Max),
	Anhbia VARCHAR(50),
	Ngaycapnhat DATETIME,
	Soluongton INT,
	MaLoai INT,
	MaNSX INT,
	CONSTRAINT PK_Sanpham PRIMARY KEY(Masp),
	CONSTRAINT FK_Loai FOREIGN KEY(MaLoai) REFERENCES LOAI(MaLoai),
	CONSTRAINT FK_Nhasanxuat FOREIGN KEY(MaNSX) REFERENCES NHASAXUAT(MaNSX)
)

GO
CREATE TABLE DONDATHANG
(
	MaDonHang INT IDENTITY(1,1),
	Dathanhtoan bit,
	Tinhtranggiaohang  bit,
	Ngaydat Datetime,
	Ngaygiao Datetime,	
	MaKH INT,
	CONSTRAINT FK_Khachhang FOREIGN KEY(MaKH) REFERENCES Khachhang(MaKH),
	CONSTRAINT PK_DonDatHang PRIMARY KEY(MaDonHang)
)
GO
CREATE TABLE CHITIETDONTHANG
(
	MaDonHang INT,
	Masp INT,
	Soluong Int Check(Soluong>0),
	Dongia Decimal(18,0) Check(Dongia>=0),	
	CONSTRAINT PK_CTDatHang PRIMARY KEY(MaDonHang,Masp),
	CONSTRAINT FK_Donhang FOREIGN KEY (Madonhang) REFERENCES Dondathang(Madonhang),
	CONSTRAINT FK_SP FOREIGN KEY (Masp) REFERENCES SANPHAM(Masp)

)
GO
create table Admin
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten nvarchar(50)
)
Insert into Admin values ('phung','1','Nguyen Trung Phung')
/****** CHUDE******/
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS ALPHABOUNCE',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS YEEZY',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS NMD HUMAN RACE',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS NMD R1',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS NMD R2',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS NMD XR1',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS PROPHERE',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'ADIDAS EQT',1)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'AIRMAX',2)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'UPTEMPO',2)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'JORDAN',2)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'HUARACHE',2)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'TRIPLE S',3)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'SPEED TRAINER',3)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'CLASSIC 1970',4)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'CHUCK II TAYLOR',4)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'VAN CỔ CAO',5)
INSERT LOAI(TenLoai,MaNSX) VALUES (N'VAN CỔ THẤP',5)


/****** KHACHHANG ******/
INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email)
VALUES (N'Nguyễn Trung Phụng', N'12 Trần Huy Liệu', N'0918062755', N'phung', N'123456', '08/20/1976', 'phetcm@hgmail.com')
INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email) 
VALUES (N'Trần Tuấn Nghĩa', N'21 Quận 6', N'0917654310', N'nghia', N'123456', '10/15/1990', N'ntluan@hcmuns.edu.vn')
INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email) 
VALUES (N'Phạm Thanh Thiên', N'32 Sư Vạn Hạnh', N'098713245', N'thien', N'123456', '05/21/1991', N'dqhoa@hcmuns.edu.vn')
INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email) 
VALUES (N'Kiều Nhất Thống', N'12 Khu chung cư', N'0918544699', N'thong', N'123456', '10/12/1986', N'nnngan@hcmuns.edu.vn')


/****** NHASAXUAT    ******/

INSERT NHASAXUAT(TenNSX, Diachi, Dienthoai) VALUES (N'ADIDAS', N'124 Nguyễn Văn Cừ Q.1 Tp.HCM', N'19001560')
INSERT NHASAXUAT(TenNSX, Diachi, Dienthoai) VALUES ( N'NIKE', N'Đồng Nai', N'19001511')
INSERT NHASAXUAT(TenNSX, Diachi, Dienthoai) VALUES ( N'BALENCIAGA', N'Tp.HCM', N'19001570')
INSERT NHASAXUAT(TenNSX, Diachi, Dienthoai) VALUES ( N'CONVERSE & VANS', N'Tp.HCM', N'0908419981')
INSERT NHASAXUAT(TenNSX, Diachi, Dienthoai) VALUES ( N'VANS', N'Tp.HCM', N'0908419981')



/******SANPHAM ******/

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'ADIDAS NMD R1 TAN', 600000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'shoes2.jpg','01/04/2019', 120, 4, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'ADIDAS NMD R2 ĐEN ĐỎ', 600000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'ADIDAS NMD R2 ÐEN Ð?.jpg','01/04/2019', 20, 5, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'ADIDAS NMD XR1 TRẮNG CAMO', 600000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'shoes2.jpg','01/04/2019', 20, 6, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N' ADIDAS NMD HUMAN RACE ĐỎ 1.0', 600000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'human-race-do1.jpg','01/04/2019', 20, 3, 1)
INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N' ADIDAS ANPHABOUNE-NAM XANH DA TRỜI', 600000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'Alpha_xanh.jpg','01/04/2019', 20, 1, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'ADIDAS YEEZY BOOST 350 V2 CREAM WHITE', 600000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'shoes2.jpg','01/04/2019', 20, 2, 1)


--

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'Adidas EQT trainers sneaker unisex Xanh ôliu', 800000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'EQT_xanh_oliu.jpg','03/07/2019', 20, 8, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'Adidas EQT trainers sneaker unisex black/white', 900000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'EQT_dentrang.jpg','03/07/2019', 20, 8, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'Adidas EQT trainers sneaker unisex white', 700000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'EQT_trang.jpg','03/07/2019', 20, 8, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'Adidas Prophere Sneakers Unisex Xám', 700000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'Prophere_xam.jpg','03/07/2019', 20, 7, 1)


INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'Adidas Prophere Olive', 700000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'Prophere_xanhreu.jpg','03/07/2019', 20, 7, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'Adidas Prophere Unisex Trainers Trắng', 6500000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'Prophere_trang.jpg','03/07/2019', 20, 7, 1)

INSERT SANPHAM(Tensp, Giaban, Mota,Anhbia,Ngaycapnhat,Soluongton, MaLoai, MaNSX) 
VALUES (N'Adidas Stan Smith Unisex Sneakers Da Trơn Màu Trắng Gót Xanh', 6500000, N'ĐÔI GIÀY CHẠY NMD VỚI THÂN GIÀY SOCK-LIKE ADIDAS PRIMEKNIT
Tiến bộ, cao cấp, tiên phong. NMD pha trộn di sản adidas tinh khiết với các vật liệu hiện đại, tiên tiến để tạo ra một cái nhìn hiện đại nổi bật trên đường phố. Thân giày Adidas Primeknit thích ứng, hỗ trợ giống như một chiếc vớ. Các tính năng nổi bật trên xuống nổi ren. Đệm Boost mang lại sự thoải mái và đáp ứng liên tục.', 
'Stansmith_gotxanh.jpg','03/07/2019', 20, 4, 1)

/****** DONDATHANG ******/

--INSERT DONDATHANG (MaKH, Dathanhtoan,Ngaydat,Ngaygiao,Tinhtranggiaohang) 
--VALUES ( 1,0, '10/15/2115', '10/20/2015',0)

--INSERT DONDATHANG (MaKH, Dathanhtoan,Ngaydat,Ngaygiao,Tinhtranggiaohang) 
--VALUES ( 3,0, '10/05/2114', '10/20/2014',0)

/******CHITIETDONHANG ******/
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (1, 19, 1, 25000)
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (1, 15, 3, 50000)
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (1, 14, 1, 25000)
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (2, 5, 3, 10000)
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (2, 9, 1, 15000)
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (2, 15, 3, 150000)
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (3, 9, 1, 25000)
--INSERT CHITIETDONTHANG (MaDonHang,Masp,SOLUONG, Dongia) VALUES (3, 10, 3,70000)

select * from LOAI
select * from NHASAXUAT
select * from SANPHAM
select * from LOAI where MaNSX=3
select * from KHACHHANG
select * from Admin

select * from DONDATHANG
select * from CHITIETDONTHANG
select * from KHACHHANG

