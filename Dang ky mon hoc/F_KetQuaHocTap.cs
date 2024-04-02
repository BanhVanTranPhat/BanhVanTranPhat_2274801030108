using QLSV;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Dang_ky_mon_hoc
{
    public partial class F_KetQuaHocTap : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc = new TAIKHOAN();
        SINHVIEN sv = new SINHVIEN();
        NGANH ct = new NGANH();
        LOPHOC loc = new LOPHOC();

        
        
        public F_KetQuaHocTap(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }
        void load()
        {
            sv = db.SINHVIENs.FirstOrDefault(s => s.MaSV == acc.SINHVIEN.MaSV);
            ct = db.NGANHs.FirstOrDefault(t => t.TenNganh == sv.LOP.NGANH.TenNganh);

            loc = db.SINHVIENs
                    .Where(s => s.MaSV == acc.SINHVIEN.MaSV)
                    .SelectMany(s => s.DANGKY_MONHOC)
                    .Select(dk => dk.LOPHOC)
                    .FirstOrDefault();



            var result = from diem in db.BANGDIEMs
                         join monhoc in db.MONHOCs on diem.MaMH equals monhoc.MaMH
                         where diem.MaSV == sv.MaSV
                         select new
                         {
                             MaMon = monhoc.MaMH,
                             TenMon = monhoc.TenMH,
                             SoTinChi = monhoc.SoTinChi
                         };
            dgvKetQuaHocTap.DataSource = result.ToList();

            int w1 = panel1.Width;
            dgvKetQuaHocTap.Columns[0].Width = w1 * 30 / 100;
            dgvKetQuaHocTap.Columns[1].Width = w1 * 30 / 100;
            dgvKetQuaHocTap.Columns[2].Width = w1 * 30 / 100;

            // Kiểm tra xem có môn học nào không
            if (dgvKetQuaHocTap.Rows.Count > 0)
            {
                // Lấy giá trị của ô trong cột MaMon của dòng đầu tiên
                string selectedMaMon = dgvKetQuaHocTap.Rows[0].Cells["MaMon"].Value.ToString();

                // Gọi phương thức hiển thị điểm dựa trên Mã môn học đầu tiên
                hienDiem(selectedMaMon);
            }
            
        }
        
        void hienDiem(string maMon)
        {
            // Query để lấy điểm cho sinh viên theo Mã môn học
            var diemTheoMon = from diem in db.BANGDIEMs
                              where diem.MaSV == sv.MaSV && diem.MaMH == maMon
                              select new
                              {
                                  Diemkt = diem.DiemKiemTra,
                                  Diemgk = diem.DiemGiuaKy,
                                  Diemck = diem.DiemCuoiKy
                              };

            // Hiển thị kết quả trên DataGridView khác (ví dụ: dgvDiem)
            dataGridView1.DataSource = diemTheoMon.ToList();
        }
        void binding()
        {
            lbDangXuat.DataBindings.Add("text", sv, "HoTenSV");
            lbKyThuatPhanMem.DataBindings.Add(new Binding("text", ct, "TenNganh"));
            if (loc != null)
            {
                label1.Text = loc.Nam.ToString(); // Chuyển đổi sang chuỗi nếu cần
                label2.Text = loc.HocKy.ToString();  // Chuyển đổi sang chuỗi nếu cần
            }
            else
            {
                // Xử lý trường hợp nếu loc là null (không có dữ liệu)
                label1.Text = "N/A";
                label2.Text = "N/A";
            }
        }
        private void F_KetQuaHocTap_Load(object sender, EventArgs e)
        {
            
        }

        private void dgvKetQuaHocTap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem CellClick có diễn ra trên dòng có dữ liệu không
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị của ô trong cột MaMon
                string selectedMaMon = dgvKetQuaHocTap.Rows[e.RowIndex].Cells["MaMon"].Value.ToString();

                // Gọi phương thức hiển thị điểm dựa trên Mã môn học
                hienDiem(selectedMaMon);
            }
        }

        private void lbThongTin_Click(object sender, EventArgs e)
        {
            F_Thongtinsinhvien ftsv = new F_Thongtinsinhvien(acc);
            ftsv.Show();
            this.Hide();
        }

        private void lbThongBao_Click(object sender, EventArgs e)
        {
            FormSinhVien ftb = new FormSinhVien(acc);
            ftb.Show();
            this.Hide();
        }

        private void lbChuonTrinh_Click(object sender, EventArgs e)
        {
            XemMonHoc xem = new XemMonHoc(acc);
            xem.Show();
            this.Hide();
        }

        private void lbDangKiHocPhan_Click(object sender, EventArgs e)
        {
            DangKiHocPhan dk = new DangKiHocPhan(acc);
            dk.Show(); this.Hide();
        }

        private void lbDangXuat3_Click(object sender, EventArgs e)
        {
            F_DangNhap log = new F_DangNhap();
            log.Show(); this.Hide();
            acc = null;
        }
    }
}
