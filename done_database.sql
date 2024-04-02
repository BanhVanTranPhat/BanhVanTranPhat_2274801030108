-- Tạo cơ sở dữ liệu
CREATE DATABASE QLYSV;
GO

-- Sử dụng cơ sở dữ liệu
USE QLYSV;
GO

-- Tạo bảng TAIKHOAN
CREATE TABLE TAIKHOAN (
    TenDangNhap NVARCHAR(20) PRIMARY KEY,
    MatKhau NVARCHAR(20),
    VaiTro NVARCHAR(20)
);

-- Tạo bảng KHOA
CREATE TABLE KHOA (
    MaKhoa NVARCHAR(20) PRIMARY KEY,
    TenKhoa NVARCHAR(100),
    DiaChiVPHoc NVARCHAR(255),
    SoDienThoai NVARCHAR(15)
);

-- Tạo bảng NGANH
CREATE TABLE NGANH (
    MaNganh NVARCHAR(20) PRIMARY KEY,
    TenNganh NVARCHAR(100),
    MaKhoa NVARCHAR(20),
    CONSTRAINT NganhThuocKhoa FOREIGN KEY (MaKhoa) REFERENCES KHOA(MaKhoa) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Tạo bảng GIANGVIEN
CREATE TABLE GIANGVIEN (
    MaGV NVARCHAR(20) PRIMARY KEY,
    HoTenGV NVARCHAR(100),
    MaKhoa NVARCHAR(20),
    CONSTRAINT TaiKhoanGV FOREIGN KEY (MaGV) REFERENCES TAIKHOAN(TenDangNhap) ON DELETE CASCADE,
    CONSTRAINT GVThuocKhoa FOREIGN KEY (MaKhoa) REFERENCES KHOA(MaKhoa)
);

-- Tạo bảng QUANLY
CREATE TABLE QUANLY (
    MaQL NVARCHAR(20) PRIMARY KEY,
    TenNQL NVARCHAR(100),
    CONSTRAINT TaiKhoanQL FOREIGN KEY (MaQL) REFERENCES TAIKHOAN(TenDangNhap) ON DELETE CASCADE
);

-- Tạo bảng CTDAOTAO
CREATE TABLE CTDAOTAO (
    MaCTDT NVARCHAR(20) PRIMARY KEY,
    TenCTDT NVARCHAR(50),
    HinhThucDT NVARCHAR(50),
    NgonNguDT NVARCHAR(50),
    TrinhDoDaoTao NVARCHAR(50)
);

-- Tạo bảng LOP
CREATE TABLE LOP (
    MaLop NVARCHAR(20) PRIMARY KEY,
    TenLop NVARCHAR(50),
    MaNganh NVARCHAR(20),
    MaCTDT NVARCHAR(20),
    CONSTRAINT LopThuocNganh FOREIGN KEY (MaNganh) REFERENCES NGANH(MaNganh) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT LopThuocCTDT FOREIGN KEY (MaCTDT) REFERENCES CTDAOTAO(MaCTDT) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Tạo bảng SINHVIEN
CREATE TABLE SINHVIEN (
    MaSV NVARCHAR(20) PRIMARY KEY,
    HoTenSV NVARCHAR(100),
    GioiTinh NVARCHAR(3),
    NgaySinh DATE,
    MaLop NVARCHAR(20),
    QueQuan NVARCHAR(255),
    DiaChi NVARCHAR(255),
    CONSTRAINT SVThuocLop FOREIGN KEY (MaLop) REFERENCES LOP(MaLop) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT TaiKhoanSV FOREIGN KEY (MaSV) REFERENCES TAIKHOAN(TenDangNhap) ON DELETE CASCADE
);

-- Tạo bảng MONHOC
CREATE TABLE MONHOC (
    MaMH NVARCHAR(20) PRIMARY KEY,
    TenMH NVARCHAR(100),
    SoTinChi INT,
    MoTa NVARCHAR(255),
    GiaoVien NVARCHAR(100)
);

-- Tạo bảng MONHOC_DAOTAO
CREATE TABLE MONHOC_DAOTAO (
    MaMHDT NVARCHAR(20) PRIMARY KEY,
    MaMH NVARCHAR(20),
    MaCTDT NVARCHAR(20),
    MaNganh NVARCHAR(20),
    CONSTRAINT CuaMonHoc FOREIGN KEY (MaMH) REFERENCES MONHOC(MaMH) ON DELETE CASCADE,
    CONSTRAINT CuaCTDT FOREIGN KEY (MaCTDT) REFERENCES CTDAOTAO(MaCTDT) ON DELETE CASCADE,
    CONSTRAINT CuaNganh FOREIGN KEY (MaNganh) REFERENCES NGANH(MaNganh) ON DELETE CASCADE
);

-- Tạo bảng LOPHOC
CREATE TABLE LOPHOC (
    MaLopHoc NVARCHAR(20) PRIMARY KEY,
    MaMHDT NVARCHAR(20),
    MaGV NVARCHAR(20),
    GioiHan INT,
    TenPhong NVARCHAR(20),
    Thu NVARCHAR(20),
    TietBatDau INT,
    TietKetThuc INT,
    ThoiGianBatDau DATE,
    ThoiGianKetThuc DATE,
    HocKy NVARCHAR(5),
    Nam INT,
    CONSTRAINT CoMonHoc FOREIGN KEY (MaMHDT) REFERENCES MONHOC_DAOTAO(MaMHDT) ON DELETE CASCADE,
    CONSTRAINT CuaGiangVien FOREIGN KEY (MaGV) REFERENCES GIANGVIEN(MaGV) ON DELETE SET NULL
);

-- Tạo bảng BANGDIEM
CREATE TABLE BANGDIEM (
    MaSV NVARCHAR(20),
    MaMH NVARCHAR(20),
    DiemKiemTra FLOAT,
    DiemGiuaKy FLOAT,
    DiemCuoiKy FLOAT,
    PRIMARY KEY (MaSV, MaMH),
    CONSTRAINT FK_SV_MonHoc FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV) ON DELETE CASCADE,
    CONSTRAINT FK_MonHoc_BangDiem FOREIGN KEY (MaMH) REFERENCES MONHOC(MaMH) ON DELETE CASCADE
);

-- Tạo bảng MONHOC_TIENQUYET
CREATE TABLE MONHOC_TIENQUYET (
    MaMH NVARCHAR(20),
    MaMH_TienQuyet NVARCHAR(20),
    PRIMARY KEY (MaMH, MaMH_TienQuyet),
    CONSTRAINT FK_MonHoc FOREIGN KEY (MaMH) REFERENCES MONHOC(MaMH) ON DELETE CASCADE,
    CONSTRAINT FK_MonHoc_TienQuyet FOREIGN KEY (MaMH_TienQuyet) REFERENCES MONHOC(MaMH) ON DELETE NO ACTION -- Thay đổi từ CASCADE thành NO ACTION
);

-- Tạo bảng DANGKY_MONHOC
CREATE TABLE DANGKY_MONHOC (
MaSV NVARCHAR(20),
MaLopHoc NVARCHAR(20),
KetQua NVARCHAR(20),
PRIMARY KEY (MaSV, MaLopHoc),
CONSTRAINT FK_SV_DangKy FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV) ON DELETE CASCADE,
CONSTRAINT FK_LopHoc_DangKy FOREIGN KEY (MaLopHoc) REFERENCES LOPHOC(MaLopHoc) ON DELETE CASCADE
);


-- Tạo đăng nhập SinhVien với mật khẩu
CREATE LOGIN SinhVien WITH PASSWORD = 'sinhvien';
-- Tạo người dùng SinhVien trong cơ sở dữ liệu và liên kết với đăng nhập
CREATE USER SinhVien FOR LOGIN SinhVien;

-- Phân quyền cho vai trò Sinh viên
GRANT SELECT ON OBJECT::SINHVIEN TO SinhVien;
GRANT SELECT, INSERT ON OBJECT::DANGKY_MONHOC TO SinhVien;
GRANT SELECT ON OBJECT::BANGDIEM TO SinhVien;


-- Tạo đăng nhập GiangVien với mật khẩu
CREATE LOGIN GiangVien WITH PASSWORD = 'giangvien';
-- Tạo người dùng GiangVien trong cơ sở dữ liệu và liên kết với đăng nhập
CREATE USER GiangVien FOR LOGIN GiangVien;
-- Phân quyền cho vai trò Giảng viên
GRANT SELECT ON OBJECT::GIANGVIEN TO GiangVien;
GRANT SELECT ON OBJECT::DANGKY_MONHOC TO GiangVien;
GRANT SELECT, INSERT, UPDATE ON OBJECT::BANGDIEM TO GiangVien;


-- Tạo đăng nhập QuanLy với mật khẩu
CREATE LOGIN QuanLy WITH PASSWORD = 'quanly';
-- Tạo người dùng QuanLy trong cơ sở dữ liệu và liên kết với đăng nhập
CREATE USER QuanLy FOR LOGIN QuanLy;
-- Phân quyền cho vai trò Quản lý
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::SINHVIEN TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::KHOA TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::NGANH TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::GIANGVIEN TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::LOP TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::CTDAOTAO TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::MONHOC TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::MONHOC_DAOTAO TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::LOPHOC TO QuanLy;
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::DANGKY_MONHOC TO QuanLy;
GRANT EXECUTE ON SCHEMA::dbo TO QuanLy;

--Nhập dữ liệu
insert into KHOA(MaKhoa, TenKhoa, DiaChiVPHoc, SoDienThoai) values
('29', N'Khoa Công nghệ may & Thời trang', 'CS3 Van Lang' , '0779980781'),
('35', N'Khoa Xây dựng & Cơ học ứng dụng', 'CS2 Van Lang' , '0779989781'),
('32', N'Khoa kinh tế', 'CS1 Van Lang' , '0779980782'),
('15', N'Khoa Cơ khí động lực', 'CS3 Van Lang' , '0279980781'),
('26', N'Khoa Thời trang và Du lịch', 'CS3 Van Lang' , '0729980781'),
('19', N'Khoa công nghệ thông tin', 'CS2 Van Lang' , '0772980781'),
('25', N'Khoa cơ khí chế tạo máy', 'CS1 Van Lang' , '0777980781'),
('34', N'Khoa in và truyền thông' , 'CS3 Van Lang' , '0799980781'),
('14', N'Khoa Công nghệ hóa học & Thực phẩm', 'CS3 Van Lang' , '0179980781'),
('21', N'Khoa điện điện tử', 'CS3 Van Lang' , '0239980781'),
('49', N'Khoa Lý luận Chính trị', 'CS3 Van Lang' , '0999980781'),
('37', N'Khoa Khoa học ứng dụng', 'CS2 Van Lang' , '0789980781'),
('20', N'Khoa Ngoại ngữ', 'CS1 Van Lang' , '0759980781'),
('30', N'Khoa Truyền Thông & Báo Chí', 'CS3 Van Lang' , '0249980781'),
('33', N'Khoa Môi Trường', 'CS2 Van Lang' , '0279980781'),
('45', N'Khoa đào tạo cử nhân', 'CS3 Van Lang' , '0779980781'),
('40', N'Khoa Ngữ Văn', 'CS2 Van Lang' , '0779980781'),
('28', N'Khoa Văn hóa & Nghệ thuật ', 'CS3 Van Lang' , '0669980781'),
('36', N'Khoa điện ảnh', 'CS1 Van Lang' , '0889980781'),
('72', N'Khoa giáo dục thể chất và quốc phòng', 'CS1 Van Lang' , '0599980781')

go
insert into NGANH(MaNganh, TenNganh, MaKhoa) values
('321', N'Logistics & Quản lý Chuỗi cung ứng', '32'),
('251', N'Kế toán ', '32'),
('260', N'Thương mại điện tử', '32'),
('360', N'Kinh doanh quốc tế', '32'),
('240', N'Quản lý công nghiệp', '32'),
('560', N'Thiết kế đồ họa', '34'),
('443', N'Cơ khí', '15'),
('290', N'Kỹ thuật y sinh', '21'),
('390', N'Hệ thống nhúng & IOT', '21'),
('103', N'Công Nghệ thông tin', '19'),
('104', N'Kỹ thuật dữ liệu', '19'),
('456', N'Công nghệ Kỹ thuật môi trường', '14'),
('123', N'Công nghệ thực phẩm', '14'),
('789', N'Công nghệ Kỹ thuật Hóa học', '14'),
('457', N'Công nghệ kỹ thuật ô tô', '15'),
('009', N'Nhóm môn học chính trị', '49'),
('008', N'Nhóm môn học thể chất', '72'),
('072', N'Nhóm môn học khoa học ứng dụng', '37')

go

insert into CTDAOTAO(MaCTDT, TenCTDT, HinhThucDT, NgonNguDT, TrinhDoDaoTao) values
('CTDT1', N'Đại trà', N'Chính Quy', N'Tiếng Việt', N'Đại học'),
('CTDT2', N'CLC', N'Chính Quy', N'Tiếng Việt', N'Đại học'),
('CTDT3', N'CLC', N'Chính Quy', N'Tiếng Anh', N'Đại học'),
('CTDT4', N'CLC', N'Chính Quy', N'Tiếng Nhật', N'Đại học')
go


insert into LOP(MaLop, TenLop, MaNganh, MaCTDT) values
('201321C', N'Logistics và Quản lý Chuỗi cung ứng C', '321', 'CTDT1'),
('181251B', N'Kế toán B', '251', 'CTDT1'),
('211263A', N'Thương mại điện tử A', '260', 'CTDT2'),
('201457B', N'Công nghệ kỹ thuật ô tô B', '457', 'CTDT1'),
('201443C', N'Cơ khí C', '443', 'CTDT1'),
('211107A', N'Công nghệ thông tin A', '103', 'CTDT1'),
('211462C', N'Kinh doanh quốc tế', '360', 'CTDT1'),
('181560B', N'Thiết kế đồ họa B', '560', 'CTDT1'),
('201321B', N'Logistics và quản lí chuỗi cung ứng B', '321', 'CTDT1'),
('201241B', N'Quản lí công nghiệp B', '240', 'CTDT1'),
('201290B', N'Kỹ thuật y sinh B', '290', 'CTDT3'),
('201390B', N'Hệ thống nhúng & IOT B', '390', 'CTDT1'),
('201106C', N'Công Nghệ thông tin C', '103', 'CTDT1'),
('201106A', N'Công Nghệ thông tin A', '103', 'CTDT2'),
('201106B', N'Công Nghệ thông tin B', '103', 'CTDT1'),
('201321A', N'Logistics và quản lí chuỗi cung ứng A', '321', 'CTDT4')


go
insert into TAIKHOAN(TenDangNhap, MatKhau, VaiTro) values
('20132202', '123456', N'Sinh Viên'),
('18125106', '123456', N'Sinh Viên'),
('21126327', '123456', N'Sinh Viên'),
('17124068', '123456', N'Sinh Viên'),
('20145707', '123456', N'Sinh Viên'),
('20144333', '123456', N'Sinh Viên'),
('19121001', '123456', N'Sinh Viên'),
('21110767', '123456', N'Sinh Viên'),
('21146201', '123456', N'Sinh Viên'),
('18156069', '123456', N'Sinh Viên'),
('20132084', '123456', N'Sinh Viên'),
('20146418', '123456', N'Sinh Viên'),
('20124361', '123456', N'Sinh Viên'),
('20151426', '123456', N'Sinh Viên'),
('20129049', '123456', N'Sinh Viên'),
('20151109', '123456', N'Sinh Viên'),
('20139062', '123456', N'Sinh Viên'),
('20142383', '123456', N'Sinh Viên'),
('20110625', '123456', N'Sinh Viên'),
('20132082', '123456', N'Sinh Viên'),

('1951', '123456', N'Giảng Viên'),
('5615', '123456', N'Giảng Viên'),
('1916', '123456', N'Giảng Viên'),
('8954', '123456', N'Giảng Viên'),
('4455', '123456', N'Giảng Viên'),
('1611', '123456', N'Giảng Viên'),
('5151', '123456', N'Giảng Viên'),
('4812', '123456', N'Giảng Viên'),
('2551', '123456', N'Giảng Viên'),
('1591', '123456', N'Giảng Viên'),
('9495', '123456', N'Giảng Viên'),
('6516', '123456', N'Giảng Viên'),
('1917', '123456', N'Giảng Viên'),
('1651', '123456', N'Giảng Viên'),
('1585', '123456', N'Giảng Viên'),
('8447', '123456', N'Giảng Viên'),
('4181', '123456', N'Giảng Viên'),
('9852', '123456', N'Giảng Viên'),
('5452', '123456', N'Giảng Viên'),

('120000', '123456', N'QUẢN LÝ'),
('120001', '123456', N'QUẢN LÝ'),
('120002', '123456', N'QUẢN LÝ'),
('120003', '123456', N'QUẢN LÝ'),
('120004', '123456', N'QUẢN LÝ')
go


go
insert into SINHVIEN(MaSV, HoTenSV, GioiTinh, NgaySinh, MaLop, QueQuan, DiaChi) values
('20132202', N'Phạm Cao Thien Loc', N'Nam', '2004-06-01', '201321C', 'HCM', '157 Pham Van Chieu' ),
('18125106', N'Huỳnh Thoi Duy', N'Nam', '2000-10-28', '181251B', 'HCM', '58 Pham Van Chieu'  ),
('21126327', N'Nguyễn Tan Dat', N'Nữ', '2003-09-07', '211263A', 'HCM', '259 Pham Van Chieu'  ),
('17124068', N'Trương Thị Khánh Linh', N'Nữ', '1999-07-10', '201106A', 'HCM', '300 Pham Van Chieu'  ),
('20145707', N'Nguyễn Vũ Phương Nam', N'Nam', '2002-06-05', '201457B' , 'HCM', '411 Pham Van Chieu'  ),
('20144333', N'Nguyễn Hoàng Linh', N'Nữ', '2002-12-12', '201443C', 'HCM', '569 Pham Van Chieu'  ),
('19121001', N'Nguyễn Thùy Trúc Quyên', N'Nữ', '2001-03-04', '201106A', 'HCM', '789 Pham Van Chieu'  ),
('21110767', N'Lý Huy Hoàng', N'Nam', '2003-09-27', '211107A', 'HCM', '345 Pham Van Chieu'  ),
('21146201', N'Banh Van Tran Phát', N'Nam', '2003-09-07', '211107A' , 'HCM', '90 Pham Van Chieu'  ),
('18156069', N'Võ Thị Hồng Nhung', N'Nữ', '2000-02-06', '181560B', 'HCM', '13 Pham Van Chieu'  ),
('20132084', N'Trần Thanh Sơn', N'Nam', '2002-06-01', '201321B', 'HCM', '15 Pham Van Chieu'  ),
('20146418', N'Bùi Hữu Thạch', N'Nam', '2002-06-01', '201106A', 'HCM', '17 Pham Van Chieu'  ),
('20124361', N'Nguyễn Thị Thúy Hằng', N'Nữ', '2002-10-16', '201241B', 'HCM', '87 Pham Van Chieu'  ),
('20151426', N'Nguyễn Xuân Trưởng', N'Nam', '2002-05-19', '201106B', 'HCM', '97 Pham Van Chieu'  ),
('20129049', N'Nguyễn Thị Thu Hiền', N'Nữ', '2002-04-03', '201290B', 'HCM', '117 Pham Van Chieu'  ),
('20151109', N'Nguyễn Quốc Trình', N'Nam', '2002-08-05', '201106A', 'HCM', '127 Pham Van Chieu'  ),
('20139062', N'Nguyễn Trí Ban', N'Nam', '2002-01-01', '201390B', 'HCM', '68 Pham Van Chieu'  ),
('20142383', N'Trịnh Minh Nhựt', N'Nam', '2002-04-01', '201106B', 'HCM', '9 Pham Van Chieu'  ),
('20110625', N'Nguyễn Ngọc Duy', N'Nam', '2002-09-01', '201106C', 'HCM', '7 Pham Van Chieu'  ),
('20132082', N'Trần Cẩm Nhung', N'Nữ', '2002-02-24', '201321C' , 'HCM', '8 Pham Van Chieu' )

go
insert into GIANGVIEN(MaGV, HoTenGV, MaKhoa) values
('1951', N'Nguyễn Thị Thanh Hà', '29'),
('5615', N'Vũ Thị Tuyết Mai', '35'),
('1916', N'Nguyễn Nguyên Đương', '32'),
('8954', N'Đào Quốc Hưng', '15'),
('4455', N'Vũ Bảo Duy', '26'),
('1611', N'Đinh Công Hiếu', '19'),
('5151', N'Phan Dương Hoàng Kha', '25'),
('4812', N'Lê Phương Linh', '34'),
('2551', N'Nguyễn Phan Quỳnh Như', '14'),
('1591', N'Nguyễn Thu Sang', '21'),
('9495', N'Phạm Thị Minh Tâm', '49'),
('6516', N'Võ Minh Nghĩa', '37'),
('1917', N'Trần Trung Thảo', '20'),
('1651', N'Nguyễn Thị Thanh Thảo', '30'),
('1585', N'Đặng Nhật Hạ', '33'),
('8447', N'Bùi Văn Quy', '45'),
('4181', N'Đỗ Thị Hà', '40'),
('9852', N'Nguyễn Thúc Thùy Tiên', '28'),
('5452', N'Hồ Văn Qúy', '36')

go
insert into QUANLY(MaQL, TenNQL) values
('120000', N'FatBank'),
('120001', N'Huỳnh Phương Anh'),
('120002', N'Lê Thị Loc'),
('120003', N'Võ Ngọc Quỳnh Châu'),
('120004', N'Đỗ Tan Duy')


go

insert into MONHOC(MaMH, TenMH, SoTinChi) values
('LLCT120314', N'Tư tưởng Hồ Chí Minh', 2),
('LLCT120205', N'Kinh tế chính trị Mác - Lênin', 2),
('DBSY230184', N'Cơ sở dữ liệu', 3),
('LLCT220514', N'Lịch sử Đảng CSVN', 2),
('PHED130715', N'Giáo dục thể chất 3', 3),
('LLCT120405', N'Chủ nghĩa xã hội khoa học', 2),
('PHED110513', N'Giáo dục thể chất 1', 1),
('PHED110613', N'Giáo dục thể chất 2', 1),
('MAOP230706', N'Nguyên lí kế toán', 3),
('ECON240206', N'Tin học ứng dụng', 3),
('PRAC230407', N'Sức bền vật liệu', 3),
('TLAW332209', N'Thương mại điện tử',3),
('FUMA230806', N'Phân tích dữ liệu', 2),
('ETHE221506', N'Tín hiệu thống kê', 3),
('PSBU220408', N'Toán cao cấp 2', 3),
('PRSK320705', N'Thí nghiệm vật lí 1', 1),
('BCUL320506', N'Tâm lí học', 2),
('PRAN321106', N'Xác xuất thống kê ứng dụng', 3),
('APCM230307', N'Máy điện đo lường', 2),
('TAPO330407', N'Điện tử cơ bản', 3)
go

insert into MONHOC_DAOTAO(MaMHDT, MaMH, MaCTDT, MaNganh) values
('MHDT001', 'LLCT120314', 'CTDT1', '009'),
('MHDT002', 'LLCT120205', 'CTDT1', '009'),
('MHDT003', 'LLCT120205', 'CTDT2', '009'),
('MHDT004', 'LLCT120205', 'CTDT3', '009'),
('MHDT005', 'DBSY230184', 'CTDT1', '103'),
('MHDT006', 'DBSY230184', 'CTDT2', '103'),
('MHDT007', 'LLCT220514', 'CTDT1', '009'),
('MHDT008', 'LLCT220514', 'CTDT2', '009'),
('MHDT009', 'LLCT220514', 'CTDT3', '009'),
('MHDT010', 'LLCT220514', 'CTDT4', '009'),
('MHDT011', 'LLCT120314', 'CTDT2', '009'),
('MHDT012', 'LLCT120314', 'CTDT3', '009'),
('MHDT013', 'LLCT120314', 'CTDT4', '009'),
('MHDT014', 'LLCT120205', 'CTDT4', '009'),
('MHDT015', 'DBSY230184', 'CTDT3', '103'),
('MHDT021', 'PHED110613', 'CTDT1', '008'),
('MHDT022', 'PHED110613', 'CTDT2', '008'),
('MHDT023', 'PHED110613', 'CTDT3', '008'),
('MHDT024', 'PHED110613', 'CTDT4', '008'),
('MHDT025', 'PHED130715', 'CTDT1', '008'),
('MHDT026', 'PHED130715', 'CTDT2', '008'),
('MHDT027', 'PHED130715', 'CTDT3', '008'),
('MHDT028', 'PHED130715', 'CTDT4', '008'),
('MHDT029', 'LLCT120405', 'CTDT1', '009'),
('MHDT030', 'LLCT120405', 'CTDT2', '009'),
('MHDT031', 'LLCT120405', 'CTDT3', '009'),
('MHDT032', 'LLCT120405', 'CTDT4', '009'),
('MHDT033', 'DBSY230184', 'CTDT1', '260'),
('MHDT034', 'DBSY230184', 'CTDT2', '260'),
('MHDT036', 'DBSY230184', 'CTDT4', '260'),
('MHDT045', 'MAOP230706', 'CTDT1', '260'),
('MHDT046', 'MAOP230706', 'CTDT2', '260'),
('MHDT047', 'MAOP230706', 'CTDT3', '260'),
('MHDT048', 'MAOP230706', 'CTDT4', '260'),
('MHDT056', 'MAOP230706', 'CTDT4', '240'),
('MHDT057', 'ECON240206', 'CTDT1', '251'),
('MHDT058', 'ECON240206', 'CTDT2', '251'),
('MHDT059', 'ECON240206', 'CTDT3', '251'),
('MHDT060', 'ECON240206', 'CTDT4', '251'),
('MHDT065', 'PRAC230407', 'CTDT1', '390'),
('MHDT066', 'PRAC230407', 'CTDT2', '390'),
('MHDT067', 'PRAC230407', 'CTDT3', '390'),
('MHDT068', 'PRAC230407', 'CTDT4', '390'),
('MHDT069', 'TLAW332209', 'CTDT1', '103'),
('MHDT070', 'TLAW332209', 'CTDT2', '103'),
('MHDT071', 'TLAW332209', 'CTDT3', '103'),
('MHDT072', 'TLAW332209', 'CTDT4', '103'),
('MHDT073', 'FUMA230806', 'CTDT1', '104'),
('MHDT074', 'FUMA230806', 'CTDT2', '104'),
('MHDT075', 'FUMA230806', 'CTDT3', '104'),
('MHDT076', 'FUMA230806', 'CTDT4', '104'),
('MHDT077', 'ETHE221506', 'CTDT1', '457'),
('MHDT078', 'ETHE221506', 'CTDT2', '457'),
('MHDT079', 'ETHE221506', 'CTDT3', '457'),
('MHDT080', 'ETHE221506', 'CTDT4', '457'),
('MHDT081', 'PSBU220408', 'CTDT1', '072'),
('MHDT082', 'PSBU220408', 'CTDT2', '072'),
('MHDT083', 'PSBU220408', 'CTDT3', '072'),
('MHDT084', 'PSBU220408', 'CTDT4', '072'),
('MHDT085', 'PRSK320705', 'CTDT1', '072'),
('MHDT086', 'PRSK320705', 'CTDT2', '072'),
('MHDT087', 'PRSK320705', 'CTDT3', '072'),
('MHDT088', 'PRSK320705', 'CTDT4', '072'),
('MHDT089', 'BCUL320506', 'CTDT1', '072'),
('MHDT090', 'BCUL320506', 'CTDT2', '072'),
('MHDT091', 'BCUL320506', 'CTDT3', '072'),
('MHDT092', 'BCUL320506', 'CTDT4', '072'),
('MHDT093', 'PRAN321106', 'CTDT1', '072'),
('MHDT094', 'PRAN321106', 'CTDT2', '072'),
('MHDT095', 'PRAN321106', 'CTDT3', '072'),
('MHDT096', 'PRAN321106', 'CTDT4', '072'),
('MHDT097', 'APCM230307', 'CTDT1', '457'),
('MHDT098', 'TAPO330407', 'CTDT1', '457'),
('MHDT099', 'TAPO330407', 'CTDT2', '457'),
('MHDT100', 'TAPO330407', 'CTDT3', '457'),
('MHDT101', 'TAPO330407', 'CTDT4', '457')

go

insert into LOPHOC(MaLopHoc, MaMHDT, MaGV, GioiHan, TenPhong, Thu, TietBatDau, TietKetThuc, ThoiGianBatDau, ThoiGianKetThuc, HocKy, Nam) values
('LLCT120314_01', 'MHDT001', '1951', 60, N'A111', N'Thứ 3', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('LLCT120314_02', 'MHDT001', '5615', 75, N'A111', N'Thứ 4', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('LLCT120205_01', 'MHDT002', '1916', 80, N'A113', N'Thứ 2', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('LLCT120205_02', 'MHDT003', '8954', 65, N'A114', N'Thứ 5', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('DBSY230184_02', 'MHDT015', '4455', 60, N'A116', N'Thứ 7', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('LLCT220514_01', 'MHDT009', '1611', 75, N'A117', N'Thứ 2', 4, 6, '2022-06-01', '2022-12-01', '1', '2022'),
('LLCT220514_02', 'MHDT008', '5151', 80, N'B111', N'Thứ 3', 4, 6, '2022-06-01', '2022-12-01', '1', '2022'),
('LLCT120405_01', 'MHDT031', '9495', 60, N'B113', N'Thứ 6', 4, 8, '2022-06-01', '2022-12-01', '1', '2022'),
('LLCT120405_02', 'MHDT032', '6516', 60, N'B114', N'Thứ 7', 4, 5, '2022-06-01', '2022-12-01', '1', '2022'),
('PHED110513_01', 'MHDT025', '1917', 60, N'B115', N'Thứ 3', 4, 7, '2022-06-01', '2022-12-01', '1', '2022'),
('PHED110513_02', 'MHDT025', '1651', 60, N'C111', N'Thứ 4', 7, 10, '2022-06-01', '2022-12-01', '1', '2022'),
('PHED110613_03', 'MHDT026', '1585', 60, N'C112', N'Thứ 2', 7, 10, '2022-06-01', '2022-12-01', '1', '2022'),
('PHED110613_04', 'MHDT027', '8447', 60, N'C113', N'Thứ 3', 7, 11, '2022-06-01', '2022-12-01', '1', '2022'),
('MAOP230706_01', 'MHDT045', '4181', 60, N'C114', N'Thứ 6', 9, 13, '2022-06-01', '2022-12-01', '1', '2022'),
('ECON240206_01', 'MHDT057', '5452', 60, N'D111', N'Thứ 2', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('PRAC230407_01', 'MHDT065', '2551', 60, N'D112', N'Thứ 3', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('TLAW332209_01', 'MHDT069', '9495', 60, N'D113', N'Thứ 6', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('FUMA230806_01', 'MHDT076', '8447', 60, N'D114', N'Thứ 2', 7, 10, '2022-06-01', '2022-12-01', '1', '2022'),
('ETHE221506_01', 'MHDT079', '5615', 60, N'A111', N'Thứ 6', 1, 3,'2022-06-01', '2022-12-01', '1', '2022'),
('PSBU220408_01', 'MHDT082', '9852', 60, N'B111', N'Thứ 7', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('PRSK320705_01', 'MHDT085', '5151', 60, N'C111', N'Thứ 3', 1, 3, '2022-06-01', '2022-12-01', '1', '2022'),
('BCUL320506_01', 'MHDT089', '6516', 60, N'D111', N'Thứ 4', 4, 8, '2022-06-01', '2022-12-01', '1', '2022'),
('PRAN321106_01', 'MHDT094', '1951', 60, N'A111', N'Thứ 3', 4, 7, '2022-06-01', '2022-12-01', '1', '2022'),
('APCM230307_01', 'MHDT097', '4181', 60, N'B111', N'Thứ 7', 7, 10, '2022-06-01', '2022-12-01', '1', '2022'),
('TAPO330407_01', 'MHDT098', '1951', 60, N'C111', N'Thứ 4', 11, 14, '2022-06-01', '2022-12-01', '1', '2022')

go
insert into DANGKY_MONHOC(MaSV, MaLopHoc,KetQua) values
('20132202', 'LLCT120314_01', 'đậu'),
('18125106', 'TAPO330407_01', 'rớt'),
('21126327', 'APCM230307_01', 'rớt'),
('20145707', 'ETHE221506_01', 'đậu'),
('20144333', 'ECON240206_01', 'rớt'),
('19121001', 'MAOP230706_01', 'đậu'),
('21110767', 'DBSY230184_02', 'rớt'),
('21146201', 'BCUL320506_01', 'đậu'),
('18156069', 'PSBU220408_01', 'rớt'),
('20132084', 'LLCT120314_01', 'đậu'),
('21146201', 'DBSY230184_02', 'đậu'),
('21110767', 'PHED110513_01', 'rớt')

go
-- Chèn dữ liệu vào bảng MONHOC_TIENQUYET
INSERT INTO MONHOC_TIENQUYET (MaMH, MaMH_TienQuyet)
VALUES
('LLCT120314', 'LLCT120205'),
('LLCT120205', 'LLCT120405'),
('DBSY230184', 'FUMA230806'),
('LLCT220514', 'PHED110513'),
('PHED130715', 'PHED110613')

-- Chèn dữ liệu vào bảng BANGDIEM cho tất cả sinh viên và môn học
INSERT INTO BANGDIEM (MaSV, MaMH, DiemKiemTra, DiemGiuaKy, DiemCuoiKy)
VALUES
('21126327', 'APCM230307', 5.0, 3.5, 4.0),
('17124068', 'PHED130715', 8.0, 7.5, 9.0),
('20145707', 'ETHE221506', 7.0, 9.0, 8.5),
('20144333', 'ECON240206', 8.5, 7.0, 9.0),
('19121001', 'MAOP230706', 7.0, 8.0, 8.5),
('21110767', 'DBSY230184', 8.0, 8.5, 9.0),
('21146201', 'BCUL320506', 9.0, 7.5, 8.0),
('18156069', 'PSBU220408', 7.5, 8.0, 8.5),
('20132084', 'LLCT120314', 8.0, 8.5, 9.0),
('20146418', 'DBSY230184', 9.0, 7.5, 8.0),
('20124361', 'PHED110513', 7.5, 8.0, 8.5),
('20151426', 'ETHE221506', 8.0, 8.5, 9.0),
('20129049', 'MAOP230706', 9.0, 7.5, 8.0),
('20151109', 'DBSY230184', 7.5, 8.0, 8.5),
('20139062', 'PSBU220408', 8.0, 8.5, 9.0),
('20142383', 'LLCT120314', 7.0, 8.0, 8.5),
('20110625', 'PHED110513', 8.5, 9.0, 7.0),
('20132082', 'ETHE221506', 7.5, 8.0, 8.5)

-- Truy vấn tất cả dữ liệu từ các bảng
SELECT * FROM TAIKHOAN;
SELECT * FROM KHOA;
SELECT * FROM NGANH;
SELECT * FROM GIANGVIEN;
SELECT * FROM QUANLY;
SELECT * FROM CTDAOTAO;
SELECT * FROM LOP;
SELECT * FROM SINHVIEN;
SELECT * FROM MONHOC;
SELECT * FROM MONHOC_DAOTAO;
SELECT * FROM LOPHOC;
SELECT * FROM BANGDIEM;
SELECT * FROM MONHOC_TIENQUYET;
SELECT * FROM DANGKY_MONHOC;
