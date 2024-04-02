using Dang_ky_mon_hoc;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLSV
{
    public partial class FormThemGV : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        QUANLY ql = new QUANLY();
        GIANGVIEN gv = new GIANGVIEN();
        public FormThemGV(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;
            load();
            binding();
        }
        void load()
        {
            ql = db.QUANLies.FirstOrDefault(q => q.MaQL == acc.QUANLY.MaQL);
            var dsGV = from g in db.GIANGVIENs
                       select new
                       {
                           MaGV = g.MaGV,
                           HoTenGV = g.HoTenGV,
                           MaKhoa = g.MaKhoa,
                       };
            dgvThemGiangVien.DataSource = dsGV.ToList();
        }
        void binding()
        {
            txtMaGV.DataBindings.Add(new Binding("text", dgvThemGiangVien.DataSource, "MaGV"));
            txtHoTenGV.DataBindings.Add(new Binding("text", dgvThemGiangVien.DataSource, "HoTenGV"));
            txtMaKhoa.DataBindings.Add(new Binding("text", dgvThemGiangVien.DataSource, "MaKhoa"));

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            FormPhongDaoTao fdt = new FormPhongDaoTao(acc);
            fdt.Show(); this.Hide();
        }

        private void FormThemGV_Load(object sender, EventArgs e)
        {

        }
        void tim(string key)
        {
            var kq = from c in db.GIANGVIENs
                     where c.MaGV.Contains(key) || c.HoTenGV.Contains(key)
                     select new
                     {
                         MaGV = c.MaGV,
                         HoTenGV = c.HoTenGV,
                         MaKhoa = c.MaKhoa,
                     };
            dgvThemGiangVien.DataSource = kq.ToList();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tukhoa = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(tukhoa))
            {
                load();
            }
            else tim(tukhoa);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string magv = txtMaGV.Text;

            // Kiểm tra độ dài mã để phân quyền
            if (magv.Length == 4)
            {

                TAIKHOAN taiKhoan = new TAIKHOAN
                {
                    TenDangNhap = magv,
                    MatKhau = "123456",
                    VaiTro = "Giảng Viên"
                };

                GIANGVIEN gv = new GIANGVIEN()
                {
                    MaGV = magv,
                    HoTenGV = txtHoTenGV.Text,
                    MaKhoa = txtMaKhoa.Text,
                };

                db.TAIKHOANs.Add(taiKhoan);
                db.GIANGVIENs.Add(gv);
            }
            db.SaveChanges();
            load();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaGV.Text;
            GIANGVIEN x = db.GIANGVIENs.FirstOrDefault(g => g.MaGV == ma);
            TAIKHOAN y = db.TAIKHOANs.FirstOrDefault(i => i.GIANGVIEN.MaGV == ma);
            if (x != null && y != null)
            {
                db.GIANGVIENs.Remove(x);
                db.TAIKHOANs.Remove(y);
                db.SaveChanges();
            }
            load();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = dgvThemGiangVien.SelectedCells[0].OwningRow.Cells["MaGV"].Value.ToString();
            GIANGVIEN gv = db.GIANGVIENs.Find(ma);
            gv.HoTenGV = txtHoTenGV.Text;
            gv.MaKhoa=txtMaKhoa.Text;
        }

        private void dgvThemGiangVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ma = dgvThemGiangVien.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ten = dgvThemGiangVien.Rows[e.RowIndex].Cells[1].Value.ToString();
            string mak = dgvThemGiangVien.Rows[e.RowIndex].Cells[2].Value.ToString();

            txtMaGV.Text = ma;
            txtHoTenGV.Text = ten;
            txtMaKhoa.Text = mak;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
