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

        // BAGIAN B: Koneksi Database saat Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                lblStatus.Text = "Status: Terhubung";
                lblStatus.ForeColor = Color.Green;
                conn.Close();

                TampilkanData();
                HitungTotal();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Status: Putus";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Koneksi Gagal: " + ex.Message);
            }
        }
    }
}
