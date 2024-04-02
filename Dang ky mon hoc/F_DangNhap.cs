using QLSV;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Dang_ky_mon_hoc
{
    public partial class F_DangNhap : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN user = new TAIKHOAN();
        public F_DangNhap()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }
        void binding()
        {
            textBox1.DataBindings.Add(new Binding("Text", user, "TenDangNhap"));
            textBox2.DataBindings.Add(new Binding("Text", user, "MatKhau"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }
        void login()
        {
            string tk = textBox1.Text;
            string mk = textBox2.Text;
            var user = db.TAIKHOANs.FirstOrDefault(u => u.TenDangNhap == tk && u.MatKhau == mk);
            if (user != null)
            {
                if (user.VaiTro.Contains("Sinh Viên"))
                {
                    F_Thongtinsinhvien F_TTSV = new F_Thongtinsinhvien(user);
                    F_TTSV.Show();
                    this.Hide();

                }
                else if (user.VaiTro.Contains("Giảng Viên"))
                {                   
                    F_Thongtingiangvien F_GV = new F_Thongtingiangvien(user);
                    F_GV.Show();
                    this.Hide();
                }
                else if (user.VaiTro.Contains("QUẢN LÝ"))
                {
                    FormPhongDaoTao F_dt = new FormPhongDaoTao(user);
                    F_dt.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Sai tai khoan hoac MK");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login();
            }
        }
    }
}
