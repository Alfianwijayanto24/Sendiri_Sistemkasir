using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SistemKasir
{
    public partial class Form1 : Form
    {
        static string connectionString =
            "Server=ALFIANN\\ALFIANWIJAYANTO;Database=SistemKasirWarung;Integrated Security=True;";

        SqlConnection conn = new SqlConnection(connectionString);

        BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        // =========================
        // FORM DIBUKA → LANGSUNG TAMPIL DATA OTOMATIS
        // =========================
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                lblStatus.Text = "Status: Terhubung ✔";
                lblStatus.ForeColor = Color.Green;
                conn.Close();

                // OTOMATIS TAMPILKAN DATA SAAT FORM DIBUKA
                TampilkanData();
                HitungTotal();

                bindingNavigator1.BindingSource = bs;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Status: Putus ✘";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Koneksi Gagal: " + ex.Message);
            }
        }

        // =========================
        // TAMPILKAN DATA BARANG
        // =========================
        private void TampilkanData()
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM vw_Barang", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                bs.DataSource = dt;
                dgvBarang.DataSource = bs;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Tampil: " + ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // =========================
        // HITUNG TOTAL RECORD
        // =========================
        private void HitungTotal()
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Barang", conn);
                int jumlah = (int)cmd.ExecuteScalar();
                lblTotalRecord.Text = "Total Record: " + jumlah;

                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // =========================
        // TOMBOL TAMPILKAN / REFRESH
        // =========================
        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            TampilkanData();
            HitungTotal();
        }

        // =========================
        // SIMPAN BARANG BARU
        // =========================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtKode.Text == "" || txtNama.Text == "")
            {
                MessageBox.Show("Kode dan Nama tidak boleh kosong!", "Validasi");
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();

                SqlCommand cmd =
                    new SqlCommand(
                        "sp_tambah_barang",
                        conn
                    );


                cmd.CommandType =
                    CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kode", txtKode.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@beli", decimal.Parse(txtHargaBeli.Text));
                cmd.Parameters.AddWithValue("@jual", decimal.Parse(txtHargaJual.Text));
                cmd.Parameters.AddWithValue("@stok", int.Parse(txtStok.Text));
                cmd.Parameters.AddWithValue("@satuan", txtSatuan.Text);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Data Berhasil Disimpan!", "Sukses");
                BersihkanInput();
                TampilkanData();
                HitungTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Simpan: " + ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // =========================
        // UPDATE BARANG
        // =========================
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtKode.Text == "") { MessageBox.Show("Pilih data terlebih dahulu!"); return; }

            DialogResult dialog = MessageBox.Show(
                "Yakin ingin mengubah data " + txtNama.Text + "?",
                "Konfirmasi Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                try
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                    conn.Open();

                    SqlCommand cmd =
                        new SqlCommand(
                            "sp_update_barang",
                            conn
                        );

                    cmd.CommandType =
                        CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@kode", txtKode.Text);
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@beli", decimal.Parse(txtHargaBeli.Text));
                    cmd.Parameters.AddWithValue("@jual", decimal.Parse(txtHargaJual.Text));
                    cmd.Parameters.AddWithValue("@stok", int.Parse(txtStok.Text));
                    cmd.Parameters.AddWithValue("@satuan", txtSatuan.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Data Berhasil Diperbarui!", "Sukses");
                    BersihkanInput();
                    TampilkanData();
                    HitungTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Update: " + ex.Message);
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }

        // =========================
        // HAPUS BARANG
        // =========================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtKode.Text == "") { MessageBox.Show("Pilih data terlebih dahulu!"); return; }

            DialogResult dialog = MessageBox.Show(
                "Yakin ingin menghapus barang " + txtNama.Text + "?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                try
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                    conn.Open();

                    SqlCommand cmd =
                        new SqlCommand(
                            "sp_hapus_barang",
                            conn
                        );

                    cmd.CommandType =
                        CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@kode", txtKode.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Data Berhasil Dihapus!");
                    BersihkanInput();
                    TampilkanData();
                    HitungTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }

        // =========================
        // REFRESH
        // =========================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BersihkanInput();
            TampilkanData();
            HitungTotal();
        }

        // =========================
        // KLIK BARIS DI GRID → ISI TEXTBOX
        // =========================
        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBarang.Rows[e.RowIndex];
                txtKode.Text = row.Cells[1].Value.ToString();
                txtNama.Text = row.Cells[2].Value.ToString();
                txtHargaBeli.Text = row.Cells[3].Value.ToString();
                txtHargaJual.Text = row.Cells[4].Value.ToString();
                txtStok.Text = row.Cells[5].Value.ToString();
                txtSatuan.Text = row.Cells[6].Value.ToString();
            }
        }

        // =========================
        // CARI BARANG OTOMATIS
        // =========================
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();

                SqlCommand cmd =
                    new SqlCommand(
                        "sp_search_barang",
                        conn
                    );

                cmd.CommandType =
                    CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@keyword", txtCari.Text);

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                bs.DataSource = dt;
                dgvBarang.DataSource = bs;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // =========================
        // TOMBOL BUKA KASIR → PINDAH KE FORM2
        // =========================
        private void btnKasir_Click(object sender, EventArgs e)
        {
            Form2 kasir = new Form2();
            kasir.Show();       // Form2 terbuka
            this.Hide();        // Form1 disembunyikan (bukan ditutup)

            // Saat Form2 ditutup → Form1 muncul lagi
            kasir.FormClosed += (s, args) =>
            {
                this.Show();
                TampilkanData();
                HitungTotal();
            };
        }

        // =========================
        // BERSIHKAN INPUT
        // =========================
        private void BersihkanInput()
        {
            txtKode.Clear();
            txtNama.Clear();
            txtHargaBeli.Clear();
            txtHargaJual.Clear();
            txtStok.Clear();
            txtSatuan.Clear();
        }

        // Event handler kosong (tidak dihapus agar Designer tidak error)
        private void label1_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
    }
}