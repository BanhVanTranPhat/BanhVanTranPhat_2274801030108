using Dang_ky_mon_hoc;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLSV
{
    public partial class FormQLDiem : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        SINHVIEN sv = new SINHVIEN();
        public FormQLDiem(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;
            load();
            binding();
        }
        void load()
        {

            var r = from c in db.SINHVIENs
                    select c;
            dgvQuanLyDiem.DataSource = r.ToList();

            var diem = from d in db.BANGDIEMs
                       select d;

            
        }
        void binding()
        {
            txtHoTen.DataBindings.Add("text", dgvQuanLyDiem.DataSource, "HoTenSV");
            txtMaSV.DataBindings.Add("text", dgvQuanLyDiem.DataSource, "MaSV");
            label9.DataBindings.Add("text", acc.QUANLY, "TenNQL");         
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            FormPhongDaoTao fdt = new FormPhongDaoTao(acc);
            fdt.Show(); this.Hide();
        }

        private void FormQLDiem_Load(object sender, EventArgs e)
        {
            dgvQuanLyDiem.Rows[0].Selected = true;

            // Trigger the event to fetch and select the first course
            dgvQuanLyDiem_CellClick(dgvQuanLyDiem, new DataGridViewCellEventArgs(0, 0));
        }
        void hienDiem(string maMon)
        {
            // Query để lấy điểm cho sinh viên theo Mã môn học
            var diemTheoMon = from diem in db.BANGDIEMs
                              where diem.MaMH == maMon
                              select new
                              {
                                  Diemkt = diem.DiemKiemTra,
                                  Diemgk = diem.DiemGiuaKy,
                                  Diemck = diem.DiemCuoiKy
                              };

            // Hiển thị kết quả trên DataGridView khác (ví dụ: dgvDiem)
            dataGridView1.DataSource = diemTheoMon.ToList();
        }
        private void dgvQuanLyDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dgvQuanLyDiem.Rows.Count)
            {
                dgvQuanLyDiem.Rows[i].Selected = true;
                if (dgvQuanLyDiem.Rows.Count > 0)
                {
                    string x = dgvQuanLyDiem.Rows[i].Cells["MaSV"].Value.ToString();
                    var mon = from m in db.MONHOCs
                              join n in db.BANGDIEMs on m.MaMH equals n.MaMH
                              where n.MaSV == x
                              select m;
                    dataGridView2.DataSource = mon.ToList();
                }
            }
            if (dataGridView2.Rows.Count > 0)
            {
                // Select the first course
                dataGridView2.Rows[0].Selected = true;

                // Trigger the event to display the grades for the selected course
                dataGridView2_CellClick(dataGridView2, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dataGridView2.Rows.Count)
            {
                dataGridView2.Rows[i].Selected = true;
                if (dataGridView2.Rows.Count > 0)
                {
                    string y = dgvQuanLyDiem.SelectedRows[0].Cells["MaSV"].Value.ToString();
                    string x = dataGridView2.Rows[i].Cells["MaMH"].Value.ToString();

                    var studentGrades = from bd in db.BANGDIEMs
                                        join sv in db.SINHVIENs on bd.MaSV equals sv.MaSV
                                        join mh in db.MONHOCs on bd.MaMH equals mh.MaMH
                                        where bd.MaSV == y && bd.MaMH == x
                                        select new
                                        {
                                            DiemKiemTra = bd.DiemKiemTra,
                                            DiemGiuaKy = bd.DiemGiuaKy,
                                            DiemCuoiKy = bd.DiemCuoiKy,
                                        };
                    dataGridView1.DataSource = studentGrades.ToList();
                }

            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
