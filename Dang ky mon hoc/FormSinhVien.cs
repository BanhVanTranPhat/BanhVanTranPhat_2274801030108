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
    public partial class FormSinhVien : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        SINHVIEN sv = new SINHVIEN();
        public FormSinhVien(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
        }

        void load()
        {
            sv = db.SINHVIENs.FirstOrDefault(s => s.MaSV == acc.SINHVIEN.MaSV);
            label1.Text = sv.HoTenSV.ToString();
        }

        private void FormSinhVien_Load(object sender, EventArgs e)
        {

        }

        private void lbThongTin_Click(object sender, EventArgs e)
        {
            F_Thongtinsinhvien fttsv = new F_Thongtinsinhvien(acc);
            fttsv.Show();
            this.Hide();
        }
    }
}
