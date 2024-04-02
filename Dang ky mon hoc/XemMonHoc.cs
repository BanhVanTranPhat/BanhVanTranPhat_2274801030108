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
    public partial class XemMonHoc : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        SINHVIEN sv = new SINHVIEN();
        TAIKHOAN acc;
        public XemMonHoc(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }

        void load()
        {
            sv = db.SINHVIENs.FirstOrDefault(s => s.MaSV == acc.SINHVIEN.MaSV);

            var dsMonHoc = db.DANGKY_MONHOC.Where(d => d.MaSV == sv.MaSV).ToList();
            dgvMonHoc.DataSource = dsMonHoc;
        }
        void binding()
        {
            label2.DataBindings.Add("text", sv, "HoTenSV");
        }
        private void XemMonHoc_Load(object sender, EventArgs e)
        {

        }

        private void lbThongTin_Click(object sender, EventArgs e)
        {
            F_Thongtinsinhvien fttsv = new F_Thongtinsinhvien(acc);
            fttsv.Show();
            this.Hide();
        }

        private void lbThongBao_Click(object sender, EventArgs e)
        {
            FormSinhVien ftb = new FormSinhVien(acc);
            ftb.Show();
            this.Hide();
        }

        private void lbKetQuaHocTap_Click(object sender, EventArgs e)
        {
            F_KetQuaHocTap fkq = new F_KetQuaHocTap(acc);
            fkq.Show();
            this.Hide();
        }

        private void lbDangKiHocPhan_Click(object sender, EventArgs e)
        {
            DangKiHocPhan dk = new DangKiHocPhan(acc);
            dk.Show(); this.Hide();
        }
    }
}
