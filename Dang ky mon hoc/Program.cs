using System;
using System.Windows.Forms;

namespace Dang_ky_mon_hoc
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new F_DangNhap());
        }
    }
}
