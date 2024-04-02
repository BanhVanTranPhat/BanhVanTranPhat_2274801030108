using Dang_ky_mon_hoc;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLSV
{
    public partial class FormXemDSGV : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        QUANLY ql = new QUANLY();
        public FormXemDSGV(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }
        void load()
        {
            ql = db.QUANLies.FirstOrDefault(g => g.MaQL == acc.QUANLY.MaQL);

            var qli = from c in db.GIANGVIENs
                      select new
                      {
                          MaGiangVien = c.MaGV,
                          HoTenGiangVien = c.HoTenGV,
                          MaKhoa = c.MaKhoa
                      };

            dgvDSGV.DataSource = qli.ToList();

            int w = panel2.Width;
            dgvDSGV.Columns[0].Width = w * 20 / 100;
            dgvDSGV.Columns[1].Width = w * 30 / 100;
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

        private void btnQLDiem_Click(object sender, EventArgs e)
        {
            FormQLDiem fqld = new FormQLDiem(acc);
            fqld.Show(); this.Hide();
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

        private void btnXemDSSV_Click(object sender, EventArgs e)
        {
            FormXemDSSV fdssv = new FormXemDSSV(acc);
            fdssv.Show(); this.Hide();
        }

        private void FormXemDSGV_Load(object sender, EventArgs e)
        {

        }
        void timGV(string key)
        {
            var kq = from c in db.GIANGVIENs
                     where c.HoTenGV.Contains(key) || c.MaGV.Contains(key)
                     select new
                     {
                         MaGiangVien = c.MaGV,
                         HoTenGiangVien = c.HoTenGV,
                         MaKhoa = c.MaKhoa
                     };
            dgvDSGV.DataSource = kq.ToList();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tu = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(tu))
            {
                load();
            }
            else timGV(tu);
        }
    }
}
