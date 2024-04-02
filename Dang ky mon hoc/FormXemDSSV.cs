using Dang_ky_mon_hoc;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLSV
{
    public partial class FormXemDSSV : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        QUANLY ql = new QUANLY();
        public FormXemDSSV(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }
        void load()
        {
            ql = db.QUANLies.FirstOrDefault(s => s.MaQL == acc.QUANLY.MaQL);

            var u = from c in db.SINHVIENs
                    select new
                    {
                        MaSinhVien = c.MaSV,
                        HoTenSinhVien = c.HoTenSV,
                        GioiTinh = c.GioiTinh,
                        NgaySinh = c.NgaySinh,
                        MaLop = c.MaLop,
                        QueQuan = c.QueQuan,
                        DiaChi = c.DiaChi
                    };
            dgvDSSV.DataSource = u.ToList();

            int w = dgvDSSV.Width;
            dgvDSSV.Columns[1].Width = w * 30 / 100;
        }
        void binding()
        {
            label1.DataBindings.Add("text", ql, "TenNQL");
        }
        private void lbTrangChu_Click(object sender, EventArgs e)
        {
            FormPhongDaoTao fdt = new FormPhongDaoTao(acc);
            fdt.Show(); this.Hide();
        }

        private void btnQLSV_Click(object sender, EventArgs e)
        {
            FormThemSinhVien ftsv = new FormThemSinhVien(acc);
            ftsv.Show(); this.Hide();
        }

        private void btnQLGV_Click(object sender, EventArgs e)
        {
            FormThemGV ftgv = new FormThemGV(acc);
            ftgv.Show(); this.Hide();
        }

        private void btnQLMH_Click(object sender, EventArgs e)
        {
            FormQLMon fqlm = new FormQLMon(acc);
            fqlm.Show(); this.Hide();
        }

        private void btnXemGV_Click(object sender, EventArgs e)
        {
            FormXemDSGV fdsgv = new FormXemDSGV(acc);
            fdsgv.Show(); this.Hide();
        }

        private void btnQLDiem_Click(object sender, EventArgs e)
        {
            FormQLDiem fqld = new FormQLDiem(acc);
            fqld.Show(); this.Hide();
        }

        private void FormXemDSSV_Load(object sender, EventArgs e)
        {

        }
        void timSV(string key)
        {
            var kq = from c in db.SINHVIENs
                     where c.HoTenSV.Contains(key) || c.MaSV.Contains(key)
                     select new
                     {
                         MaSinhVien = c.MaSV,
                         HoTenSinhVien = c.HoTenSV,
                         GioiTinh = c.GioiTinh,
                         NgaySinh = c.NgaySinh,
                         MaLop = c.MaLop,
                         QueQuan = c.QueQuan,
                         DiaChi = c.DiaChi
                     };
            dgvDSSV.DataSource = kq.ToList();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tu = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(tu))
            {
                load();
            }
            else timSV(tu);
        }
    }
}
