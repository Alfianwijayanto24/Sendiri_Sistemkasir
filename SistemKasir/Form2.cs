using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemKasir
{
    public partial class Form2 : Form
    {
        static string connectionString =
            "Server=ALFIANN\\ALFIANWIJAYANTO;Database=SistemKasirWarung;Integrated Security=True;";

        SqlConnection conn = new SqlConnection(connectionString);

        BindingSource bs = new BindingSource();

        public Form2()
        {
            InitializeComponent();
            InitDGV();
            lblTotal.Text = "Total : Rp 0";
            bindingNavigator1.BindingSource = bs;
        }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void InitDGV()
        {
            dgvTransaksi.Columns.Clear();
            dgvTransaksi.Columns.Add("NamaBarang", "Nama Barang");
            dgvTransaksi.Columns.Add("Harga", "Harga");
            dgvTransaksi.Columns.Add("Qty", "Qty");
            dgvTransaksi.Columns.Add("Subtotal", "Subtotal");
            dgvTransaksi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadBarang(string keyword = "")
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = @"
            SELECT 
                ID,
                KodeBarang,
                NamaBarang,
                HargaJual,
                Stok
            FROM vw_Barang
            WHERE NamaBarang LIKE @keyword
               OR KodeBarang LIKE @keyword";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                da.SelectCommand.Parameters.AddWithValue(
                    "@keyword",
                    "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                bs.DataSource = dt;
                dgvBarang.DataSource = bs;

                dgvBarang.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            lblTotal.Text = "Total : Rp 0";
            LoadBarang();
        }

        // =========================
        // CARI BARANG
        // =========================
        private void btnCari_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtKode.Text))
                {
                    MessageBox.Show("Masukkan kode barang terlebih dahulu!");
                    return;
                }

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd =
                    new SqlCommand(
                        "sp_search_barang_kode",
                        conn
                    );

                cmd.CommandType =
                    CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kode", txtKode.Text.Trim());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtNama.Text = dr["NamaBarang"].ToString();
                    txtHarga.Text = dr["HargaJual"].ToString();
                    txtQty.Focus();
                }
                else
                {
                    MessageBox.Show("Barang tidak ditemukan!");
                    txtNama.Clear();
                    txtHarga.Clear();
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        // =========================
        // TAMBAH KE KERANJANG
        // =========================
        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNama.Text))
                {
                    MessageBox.Show("Cari barang terlebih dahulu!");
                    return;
                }

                if (!decimal.TryParse(txtHarga.Text, out decimal hargaDecimal))
                {
                    MessageBox.Show("Format harga tidak valid!");
                    return;
                }

                if (!int.TryParse(txtQty.Text, out int qty) || qty <= 0)
                {
                    MessageBox.Show("Qty harus berupa angka bulat lebih dari 0!");
                    return;
                }

                int harga = (int)hargaDecimal;
                int subtotal = harga * qty;

                dgvTransaksi.Rows.Add(txtNama.Text, harga, qty, subtotal);
                HitungTotal();

                txtKode.Clear();
                txtNama.Clear();
                txtHarga.Clear();
                txtQty.Clear();
                txtKode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // =========================
        // HITUNG TOTAL
        // =========================
        private int HitungTotal()
        {
            int total = 0;
            foreach (DataGridViewRow row in dgvTransaksi.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                    total += Convert.ToInt32(row.Cells["Subtotal"].Value);
            }
            lblTotal.Text = "Total : Rp " + total.ToString("N0");
            return total;
        }

        // =========================
        // HITUNG KEMBALIAN OTOMATIS
        // =========================
        private void txtBayar_TextChanged(object sender, EventArgs e)
        {
            string input = txtBayar.Text.Replace(".", "").Replace(",", "").Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                txtKembali.Text = "";
                txtKembali.ForeColor = System.Drawing.Color.Black;
                return;
            }

            if (!decimal.TryParse(input, out decimal bayar))
            {
                txtKembali.Text = "Input tidak valid";
                txtKembali.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int total = HitungTotal();
            decimal kembali = bayar - total;

            txtKembali.Text = kembali.ToString("N0");
            txtKembali.ForeColor = kembali < 0
                ? System.Drawing.Color.Red
                : System.Drawing.Color.Black;
        }

        // =========================
        // HAPUS BARIS DI DATAGRIDVIEW
        // =========================
        private void dgvTransaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvTransaksi.Columns.Count - 1)
            {
                dgvTransaksi.Rows.RemoveAt(e.RowIndex);
                HitungTotal();
            }
        }

        // =========================
        // SIMPAN TRANSAKSI
        // =========================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTransaksi.Rows.Count == 0)
                {
                    MessageBox.Show("Keranjang masih kosong!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBayar.Text))
                {
                    MessageBox.Show("Masukkan jumlah bayar!");
                    return;
                }

                int total = HitungTotal();

                if (!decimal.TryParse(
                        txtBayar.Text.Replace(".", "").Replace(",", ""),
                        out decimal bayar))
                {
                    MessageBox.Show("Format bayar tidak valid!");
                    return;
                }

                if (bayar < total)
                {
                    MessageBox.Show("Uang bayar kurang!");
                    return;
                }

                decimal kembali = bayar - total;

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlTransaction trx = conn.BeginTransaction();

                try
                {
                    string queryTransaksi = @"
                        INSERT INTO Transaksi (TanggalTransaksi, TotalPenjualan, Bayar, Kembali)
                        VALUES (@Tanggal, @Total, @Bayar, @Kembali);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdTrx = new SqlCommand(queryTransaksi, conn, trx);
                    cmdTrx.Parameters.AddWithValue("@Tanggal", DateTime.Now);
                    cmdTrx.Parameters.AddWithValue("@Total", total);
                    cmdTrx.Parameters.AddWithValue("@Bayar", bayar);
                    cmdTrx.Parameters.AddWithValue("@Kembali", kembali);

                    int transaksiID = Convert.ToInt32(cmdTrx.ExecuteScalar());

                    foreach (DataGridViewRow row in dgvTransaksi.Rows)
                    {
                        if (row.Cells["NamaBarang"].Value == null) continue;

                        string namaBarang = row.Cells["NamaBarang"].Value.ToString();
                        int harga = Convert.ToInt32(row.Cells["Harga"].Value);
                        int qty = Convert.ToInt32(row.Cells["Qty"].Value);
                        int subtotal = Convert.ToInt32(row.Cells["Subtotal"].Value);

                        string queryBarangID = "SELECT ID FROM Barang WHERE NamaBarang = @nama";
                        SqlCommand cmdBarang = new SqlCommand(queryBarangID, conn, trx);
                        cmdBarang.Parameters.AddWithValue("@nama", namaBarang);
                        int barangID = Convert.ToInt32(cmdBarang.ExecuteScalar());

                        string queryDetail = @"
                            INSERT INTO DetailTransaksi
                                (TransaksiID, BarangID, Qty, HargaJual, Subtotal)
                            VALUES
                                (@TransaksiID, @BarangID, @Qty, @Harga, @Subtotal)";

                        SqlCommand cmdDetail = new SqlCommand(queryDetail, conn, trx);
                        cmdDetail.Parameters.AddWithValue("@TransaksiID", transaksiID);
                        cmdDetail.Parameters.AddWithValue("@BarangID", barangID);
                        cmdDetail.Parameters.AddWithValue("@Qty", qty);
                        cmdDetail.Parameters.AddWithValue("@Harga", harga);
                        cmdDetail.Parameters.AddWithValue("@Subtotal", subtotal);
                        cmdDetail.ExecuteNonQuery();
                    }

                    trx.Commit();

                    // ✅ Tampilkan struk sukses
                    MessageBox.Show(
                        "Transaksi berhasil disimpan!\n" +
                        "Tanggal : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n" +
                        "Total   : Rp " + total.ToString("N0") + "\n" +
                        "Bayar   : Rp " + bayar.ToString("N0") + "\n" +
                        "Kembali : Rp " + kembali.ToString("N0"),
                        "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // ✅ Konfirmasi sebelum reset — DGV tidak langsung kosong
                    DialogResult tanya = MessageBox.Show(
                        "Buat transaksi baru?",
                        "Konfirmasi",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (tanya == DialogResult.Yes)
                        ResetForm();
                }
                catch
                {
                    trx.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        
        // =========================
        // RESET SEMUA INPUT
        // =========================
        private void ResetForm()
        {
            dgvTransaksi.Rows.Clear();
            lblTotal.Text = "Total : Rp 0";
            txtKode.Clear();
            txtNama.Clear();
            txtHarga.Clear();
            txtQty.Clear();
            txtBayar.Clear();
            txtKembali.Clear();
            txtKode.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }
        private void txtCariBarang_TextChanged(object sender, EventArgs e)
        {
            LoadBarang(txtCariBarang.Text);
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBarang.Rows[e.RowIndex];

                txtKode.Text = row.Cells["KodeBarang"].Value.ToString();
                txtNama.Text = row.Cells["NamaBarang"].Value.ToString();
                txtHarga.Text = row.Cells["HargaJual"].Value.ToString();

                txtQty.Focus();
            }
        }
       
    }
}