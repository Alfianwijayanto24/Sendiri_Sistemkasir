using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemKasir
{
    public partial class Form1 : Form
    {
        // 1. Pastikan Server Name sesuai dengan punya kamu
        static string connectionString = "Server=ALFIANN\\ALFIANWIJAYANTO;Database=SistemKasirWarung;Integrated Security=True;";
        SqlConnection conn = new SqlConnection(connectionString);

        public Form1()
        {
            InitializeComponent();
        }

    }
}