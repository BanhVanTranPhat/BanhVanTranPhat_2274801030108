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
    public partial class FormQLMon : Form
    {
        QLYSVEntities db = new QLYSVEntities();
        TAIKHOAN acc;
        public FormQLMon(TAIKHOAN acc)
        {
            InitializeComponent();
            this.acc = acc;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            FormPhongDaoTao fdt = new FormPhongDaoTao(acc);
            fdt.Show(); this.Hide();
        }

        private void FormQLMon_Load(object sender, EventArgs e)
        {

        }
    }
}
