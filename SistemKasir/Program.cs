using System;
using System.Windows.Forms;

namespace SistemKasir
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // APLIKASI MULAI DARI FORM1
            Application.Run(new Form1());
        }
    }
}