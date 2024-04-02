using Dang_ky_mon_hoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class FormPhongDaoTao : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        QUANLY ql = new QUANLY();
        public FormPhongDaoTao(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }
        void load()
        {
            ql = db.QUANLies.FirstOrDefault(q=>q.MaQL == acc.QUANLY.MaQL);
        }
        void binding()
        {
            label1.DataBindings.Add("text", ql, "TenNQL");
        }
        private void FormPhongDaoTao_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            F_DangNhap fdn = new F_DangNhap();
            fdn.Show(); this.Hide(); acc = null;
        }

        private void btnThemSinhVien_Click(object sender, EventArgs e)
        {
            FormThemSinhVien ftsv = new FormThemSinhVien(acc);
            ftsv.Show(); this.Hide();
        }

        private void btnQuanLyGV_Click(object sender, EventArgs e)
        {
            FormThemGV ftgv = new FormThemGV(acc);
            ftgv.Show(); this.Hide();
        }

        private void btnQuanLyDiem_Click(object sender, EventArgs e)
        {
            FormQLDiem fqld = new FormQLDiem(acc);
            fqld.Show(); this.Hide();
        }

        private void btnQuanLyMonHoc_Click(object sender, EventArgs e)
        {
            FormQLMon fqlm = new FormQLMon(acc);
            fqlm.Show(); this.Hide();
        }

        private void btnXemSV_Click(object sender, EventArgs e)
        {
            FormXemDSSV fxdssv = new FormXemDSSV(acc);
            fxdssv.Show(); this.Hide();
        }

        private void btnXemGV_Click(object sender, EventArgs e)
        {
            FormXemDSGV fxdsgv = new FormXemDSGV(acc);
            fxdsgv.Show(); this.Hide();
        }
    }
}
