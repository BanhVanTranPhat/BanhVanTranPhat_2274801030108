using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dang_ky_mon_hoc
{
    public partial class F_Thongtingiangvien : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        GIANGVIEN gv = new GIANGVIEN();
        TAIKHOAN acc;
        public F_Thongtingiangvien(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }
        
        void load()
        {
            gv = db.GIANGVIENs.FirstOrDefault(g => g.MaGV == acc.GIANGVIEN.MaGV);
            // 1. Retrieve the teacher's ID
            string teacherId = acc.GIANGVIEN.MaGV;

            // 2. Query for classes with the teacher's ID
            var classes = db.LOPHOCs.Where(l => l.MaGV == teacherId).ToList();

            // 3. Bind the results to a DataGridView
            dgvDanhSachLopDaDangKy.DataSource = classes.ToList();
        }
        void binding()
        {
            lbHoNgocVinh.DataBindings.Add("text", gv, "HoTenGV");
            lbHoNgocVinh2.DataBindings.Add("text", gv, "HoTenGV");
            lb348950.DataBindings.Add("text", gv, "MaGV");
            lbKTPM.DataBindings.Add("text", gv, "MaKhoa");
        }
        private void F_Thongtingiangvien_Load(object sender, EventArgs e)
        {

        }

        private void lbXemDiemSinhVien_Click(object sender, EventArgs e)
        {
            F_Xemdiemsinhvien fdiem = new F_Xemdiemsinhvien(acc);
            fdiem.Show(); this.Hide();
        }

        private void lbDangKiLopGiangDay_Click(object sender, EventArgs e)
        {
            F_Dangkilopgiangday fdkd = new F_Dangkilopgiangday(acc);
            fdkd.Show(); this.Hide();
        }

        private void lbDangXuat_Click(object sender, EventArgs e)
        {
            F_DangNhap fdn = new F_DangNhap();
            fdn.Show(); this.Hide();
            acc = null;
        }
    }
}
