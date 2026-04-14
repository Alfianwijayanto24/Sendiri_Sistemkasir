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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // BAGIAN E: Tampilkan Data menggunakan SqlDataReader
        private void TampilkanData()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Barang", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvBarang.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Tampil: " + ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
        // BAGIAN D: ExecuteScalar untuk Hitung Total Record
        private void HitungTotal()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Barang", conn);
                int jumlah = (int)cmd.ExecuteScalar();
                lblTotalRecord.Text = "Total Record: " + jumlah.ToString();
                conn.Close();
            }
            catch { if (conn.State == ConnectionState.Open) conn.Close(); }
        }


        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtKode.Text == "" || txtNama.Text == "")
            {
                MessageBox.Show("Kode dan Nama tidak boleh kosong!", "Validasi");
                return;
            }

            try
            {
                conn.Open();
                // PERBAIKAN: Menyebutkan kolom secara spesifik karena ID otomatis
                string query = "INSERT INTO Barang (KodeBarang, NamaBarang, HargaBeli, HargaJual, Stok, Satuan) " +
                               "VALUES (@kode, @nama, @beli, @jual, @stok, @satuan)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode", txtKode.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@beli", decimal.Parse(txtHargaBeli.Text));
                cmd.Parameters.AddWithValue("@jual", decimal.Parse(txtHargaJual.Text));
                cmd.Parameters.AddWithValue("@stok", int.Parse(txtStok.Text));
                cmd.Parameters.AddWithValue("@satuan", txtSatuan.Text);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Data Berhasil Disimpan", "Sukses");
                TampilkanData();
                HitungTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Simpan: " + ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

    }
}
