using Dang_ky_mon_hoc;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLSV
{
    public partial class FormThemSinhVien : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        QUANLY ql = new QUANLY();
        SINHVIEN sv = new SINHVIEN();
        public FormThemSinhVien(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            binding();
        }
        void load()
        {
            ql = db.QUANLies.FirstOrDefault(q => q.MaQL == acc.QUANLY.MaQL);
            var dsSV = from c in db.SINHVIENs
                       select new
                       {
                           MaSV = c.MaSV,
                           HoTenSV = c.HoTenSV,
                           GioiTinh = c.GioiTinh,
                           NgaySinh = c.NgaySinh,
                           MaLop = c.MaLop,
                       };
            dataGridView1.DataSource = dsSV.ToList();
        }

        void binding()
        {
            txtMaSV.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "MaSV"));
            txtHoTen.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "HoTenSV"));
            txtGioiTinh.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "GioiTinh"));
            txtNgaySinh.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "NgaySinh"));
        }
        private void FormThemSinhVien_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            FormPhongDaoTao fdt = new FormPhongDaoTao(acc);
            fdt.Show(); this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ma = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ten = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string gt = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            //string malop = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            DateTime nsinh = (DateTime)dataGridView1.Rows[e.RowIndex].Cells[3].Value;

            txtMaSV.Text = ma;
            txtHoTen.Text = ten;
            txtGioiTinh.Text = gt;
            txtNgaySinh.Text = nsinh.ToString("dd/MM/yyyy");

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text;

            // Kiểm tra độ dài mã để phân quyền
            if (maSV.Length == 8)
            {
                // Sinh viên
                TAIKHOAN taiKhoan = new TAIKHOAN
                {
                    TenDangNhap = maSV,
                    MatKhau = "123456",
                    VaiTro = "Sinh Viên"
                };

                SINHVIEN sinhVien = new SINHVIEN
                {
                    MaSV = maSV,
                    HoTenSV = txtHoTen.Text,
                    GioiTinh = txtGioiTinh.Text,
                    NgaySinh = Convert.ToDateTime(txtNgaySinh.Text),
                };

                db.TAIKHOANs.Add(taiKhoan);
                db.SINHVIENs.Add(sinhVien);
            }
            else
            {
                // Độ dài không hợp lệ
                MessageBox.Show("Độ dài mã không hợp lệ!");
                return;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            MessageBox.Show("Đã thêm thành công!");
            load();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaSV.Text;
            TAIKHOAN tk = db.TAIKHOANs.FirstOrDefault(s => s.SINHVIEN.MaSV == ma);
            SINHVIEN sv = db.SINHVIENs.FirstOrDefault(s=>s.MaSV==ma);
            if (tk != null && sv !=null)
            {
                db.TAIKHOANs.Remove(tk);
                db.SINHVIENs.Remove(sv);
                db.SaveChanges ();
            }
            load();
            clear();
        }
        void clear()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtGioiTinh.Clear();
            txtNgaySinh.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = dataGridView1.SelectedCells[0].OwningRow.Cells["MaSV"].Value.ToString();
            SINHVIEN sv = db.SINHVIENs.Find(ma);
            sv.HoTenSV = txtHoTen.Text;
            sv.NgaySinh = Convert.ToDateTime(txtNgaySinh.Text);
            db.SaveChanges();
            load();
        }
    }
}
