using Dang_ky_mon_hoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QLSV
{
    public partial class DangKiHocPhan : Form
    {
        TAIKHOAN acc;
        QLYSVEntities db = new QLYSVEntities();
        SINHVIEN sv = new SINHVIEN();

        List<MONHOC> dsMonchon = new List<MONHOC>();
        public DangKiHocPhan(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;

            load();
            bingding();
        }
        void load()
        {
            sv = db.SINHVIENs.FirstOrDefault(s => s.MaSV == acc.SINHVIEN.MaSV);

            var mon = from m in db.LOPHOCs
                      select m;
            dgvDangKiHocPhan.DataSource = mon.ToList();
            // Retrieve information from LOPHOC based on the student's registration
            List<LOPHOC> dsDangKi = db.LOPHOCs
                .Where(c => db.DANGKY_MONHOC.Any(dk => dk.MaLopHoc == c.MaLopHoc && dk.MaSV == sv.MaSV))
                .ToList();

            dataGridView1.DataSource = dsDangKi;
        }
        void bingding()
        {
            label2.DataBindings.Add("text", sv, "HoTenSV");
        }
        private void DangKiHocPhan_Load(object sender, EventArgs e)
        {

        }

        private void lbThongTin_Click(object sender, EventArgs e)
        {
            F_Thongtinsinhvien trchu = new F_Thongtinsinhvien(acc);
            trchu.Show(); this.Hide();
        }

        private void lbThongBao_Click(object sender, EventArgs e)
        {
            FormSinhVien tb = new FormSinhVien(acc);
            tb.Show(); this.Hide();
        }

        private void lbChươngTrinhDaoTao_Click(object sender, EventArgs e)
        {
            XemMonHoc dt = new XemMonHoc(acc);
            dt.Show(); this.Hide();
        }

        private void lbKetQuaHocTap_Click(object sender, EventArgs e)
        {
            F_KetQuaHocTap kq = new F_KetQuaHocTap(acc);
            kq.Show(); this.Hide();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            F_DangNhap log = new F_DangNhap();
            acc = null;
            log.Show(); this.Hide();
        }

        private void dgvDangKiHocPhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the clicked cell
            int clickedRowIndex = e.RowIndex;

            // Check if the clicked cell is within a valid row index
            if (clickedRowIndex >= 0 && clickedRowIndex < dgvDangKiHocPhan.Rows.Count)
            {
                // Select the entire row of the clicked cell
                dgvDangKiHocPhan.Rows[clickedRowIndex].Selected = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the index of the clicked cell
            int clickedRowIndex = e.RowIndex;

            // Check if the clicked cell is within a valid row index
            if (clickedRowIndex >= 0 && clickedRowIndex < dgvDangKiHocPhan.Rows.Count)
            {
                // Select the entire row of the clicked cell
                dataGridView1.Rows[clickedRowIndex].Selected = true;
            }
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            if (dgvDangKiHocPhan.SelectedRows.Count > 0)
            {
                int rowIndex = dgvDangKiHocPhan.SelectedRows[0].Index;

                if (rowIndex >= 0 && rowIndex < dgvDangKiHocPhan.Rows.Count)
                {
                    string maLop = (string)dgvDangKiHocPhan.Rows[rowIndex].Cells["MaLopHoc"].Value;

                    // Check if the student has already registered for this class
                    if (db.DANGKY_MONHOC.Any(dk => dk.MaLopHoc == maLop && dk.MaSV == sv.MaSV))
                    {
                        MessageBox.Show("Bạn đã đăng ký lớp này!");
                        return;
                    }

                    // Retrieve information from LOPHOC based on the selected class
                    LOPHOC selectedClass = db.LOPHOCs.FirstOrDefault(c => c.MaLopHoc == maLop);

                    if (selectedClass != null)
                    {
                        // Create a DANGKY_MONHOC entry
                        DANGKY_MONHOC dangKiLop = new DANGKY_MONHOC()
                        {
                            MaLopHoc = maLop,
                            MaSV = sv.MaSV,
                            // Set appropriate status based on your logic
                            KetQua = "Đang chờ duyệt"
                        };

                        // Add the registration to the database
                        db.DANGKY_MONHOC.Add(dangKiLop);
                        db.SaveChanges();

                        MessageBox.Show("Đăng ký lớp thành công!");

                        // Update dgv2 with information from LOPHOC
                        List<LOPHOC> dsDangKi = db.LOPHOCs
                            .Where(c => db.DANGKY_MONHOC.Any(dk => dk.MaLopHoc == c.MaLopHoc && dk.MaSV == sv.MaSV))
                            .ToList();

                        dataGridView1.DataSource = dsDangKi;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin lớp học.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid row.");
                }
            }
            else
            {
                MessageBox.Show("Please select a row before clicking the button.");
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
                {
                    string maLop = (string)dataGridView1.Rows[rowIndex].Cells["MaLopHoc"].Value;

                    // Find the DANGKY_MONHOC entry to remove
                    DANGKY_MONHOC dangKyToRemove = db.DANGKY_MONHOC
                        .FirstOrDefault(dk => dk.MaLopHoc == maLop && dk.MaSV == sv.MaSV);

                    if (dangKyToRemove != null)
                    {
                        // Remove the entry from the database
                        db.DANGKY_MONHOC.Remove(dangKyToRemove);
                        db.SaveChanges();

                        MessageBox.Show("Đã xóa lớp học thành công!");

                        // Reload dataGridView1 after removal
                        load();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin đăng ký lớp học.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid row.");
                }
            }
            else
            {
                MessageBox.Show("Please select a row before clicking the button.");
            }
        }
    }
}
