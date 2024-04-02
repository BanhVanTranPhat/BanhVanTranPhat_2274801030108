using QLSV;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Dang_ky_mon_hoc
{
    public partial class F_Thongtinsinhvien : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        SINHVIEN sv = new SINHVIEN();
        TAIKHOAN acc;

        public F_Thongtinsinhvien(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

        }
        void binding()
        {
            label2.DataBindings.Add("Text", sv, "MaSV");
            label3.DataBindings.Add("Text", sv, "HoTenSV");
            label4.DataBindings.Add("Text", sv, "NgaySinh");
            label5.DataBindings.Add("Text", sv, "QueQuan");
            label6.DataBindings.Add("Text", sv, "DiaChi");
            label7.DataBindings.Add("Text", sv, "GioiTinh");
            lbDangXuat.DataBindings.Add("Text", sv, "HoTenSV");
        }
        void load()
        {
            sv = db.SINHVIENs.FirstOrDefault(s => s.MaSV == acc.SINHVIEN.MaSV);
        }
        void logout()
        {
            F_DangNhap f_DangNhap = new F_DangNhap();
            f_DangNhap.Show();

            acc = null;

            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();
            binding();
        }

        private void lb_logout_Click(object sender, EventArgs e)
        {
            logout();
        }

        private void lbKetQuaHocTap_Click(object sender, EventArgs e)
        {
            F_KetQuaHocTap fkq = new F_KetQuaHocTap(acc);
            fkq.Show();
            this.Hide();
        }

        private void lbThongBao_Click(object sender, EventArgs e)
        {
            FormSinhVien ftb = new FormSinhVien(acc);
            ftb.Show();
            this.Hide();
        }

        private void lbMonHoc_Click(object sender, EventArgs e)
        {
            XemMonHoc fxem = new XemMonHoc(acc);
            fxem.Show();
            this.Hide();
        }

        private void lbDangKiHocPhan_Click(object sender, EventArgs e)
        {
            DangKiHocPhan dk = new DangKiHocPhan(acc);
            dk.Show(); this.Hide();
        }
    }
}
