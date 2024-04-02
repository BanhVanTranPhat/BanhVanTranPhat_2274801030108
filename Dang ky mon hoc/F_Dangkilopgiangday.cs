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
    public partial class F_Dangkilopgiangday : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        GIANGVIEN gv = new GIANGVIEN();
        TAIKHOAN acc;
        public F_Dangkilopgiangday(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }
        void load()
        {
            gv = db.GIANGVIENs.FirstOrDefault(g=>g.MaGV == acc.GIANGVIEN.MaGV);
            var dsMon = from c in db.MONHOC_DAOTAO
                        select c;
            dgcChonLop.DataSource = dsMon.ToList();
        }
        void binding()
        {
            lbHoNgocVinh2.DataBindings.Add("text", gv,"HoTenGV");
        }
        private void F_Dangkilopgiangday_Load(object sender, EventArgs e)
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

        private void lbDangXuat2_Click(object sender, EventArgs e)
        {
            F_DangNhap fdn = new F_DangNhap();
            fdn.Show(); this.Hide();
            acc = null;
        }

        private void lbKetQuaHocTap_Click(object sender, EventArgs e)
        {
            F_Xemdiemsinhvien fxd = new F_Xemdiemsinhvien(acc);
            fxd.Show(); this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedCourseId = dgcChonLop.SelectedRows[0].Cells[0].Value.ToString();
                string malop = textBox1.Text;
                string phong = textBox2.Text;
                int gh = int.Parse(textBox3.Text);
                int start = int.Parse(textBox4.Text);
                int end = int.Parse(textBox5.Text);

                LOPHOC newClass = new LOPHOC()
                {
                    MaMHDT = selectedCourseId,
                    MaLopHoc = malop,
                    TenPhong = phong,
                    GioiHan = gh,   
                    TietBatDau =start,
                    TietKetThuc = end,
                    MaGV = acc.GIANGVIEN.MaGV 
                };

                db.LOPHOCs.Add(newClass);
                db.SaveChanges();

                MessageBox.Show("Class registered successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering class: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgcChonLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the clicked cell
            int clickedRowIndex = e.RowIndex;

            // Check if the clicked cell is within a valid row index
            if (clickedRowIndex >= 0 && clickedRowIndex < dgcChonLop.Rows.Count)
            {
                // Select the entire row of the clicked cell
                dgcChonLop.Rows[clickedRowIndex].Selected = true;
            }
        }
    }
}
