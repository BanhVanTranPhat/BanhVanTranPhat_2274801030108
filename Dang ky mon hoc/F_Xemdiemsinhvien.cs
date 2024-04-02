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
    public partial class F_Xemdiemsinhvien : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        GIANGVIEN gv = new GIANGVIEN();
        TAIKHOAN acc;
        public F_Xemdiemsinhvien(TAIKHOAN acc)
        {           
            InitializeComponent();
            this.acc = acc;

            load();
            binding();  
        }
        void load()
        {
            gv = db.GIANGVIENs.FirstOrDefault(g => g.MaGV == acc.GIANGVIEN.MaGV);
        }
        void binding()
        {
            label1.DataBindings.Add("text", gv, "HoTenGV");
        }
        private void F_Xemdiemsinhvien_Load(object sender, EventArgs e)
        {

        }

        private void lbThongTin_Click(object sender, EventArgs e)
        {
            F_Thongtingiangvien fgv = new F_Thongtingiangvien(acc);
            fgv.Show(); this.Hide();
        }

        

        private void lbDangKiLopGiangDay_Click(object sender, EventArgs e)
        {
            F_Dangkilopgiangday fdkd = new F_Dangkilopgiangday(acc);
            fdkd.Show(); this.Hide();
        }
    }
}
