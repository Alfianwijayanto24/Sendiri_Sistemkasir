using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;

namespace SistemKasir
{
    public partial class Form1 : Form
    {
        DAL dbLogic = new DAL();
        BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilkanData();
            HitungTotal();
            bindingNavigator1.BindingSource = bs;
        }

        // =====================
        // TAMPILKAN DATA
        // =====================
        private void TampilkanData()
        {
            try
            {
                // Reset dulu biar tidak konflik dengan preview Excel
                dgvBarang.DataSource = null;

                bs.DataSource = dbLogic.GetBarang();
                dgvBarang.DataSource = bs;

                if (dgvBarang.Columns.Contains("Foto"))
                {
                    DataGridViewImageColumn fotoCol =
                        (DataGridViewImageColumn)dgvBarang.Columns["Foto"];
                    fotoCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
                }

                dgvBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Tampil: " + ex.Message);
            }
        }

        private void HitungTotal()
        {
            try
            {
                lblTotalRecord.Text = "Total Record: " + dbLogic.CountBarang();
            }
            catch { }
        }

        // =====================
        // TOMBOL SIMPAN (INSERT)
        // =====================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtKode.Text == "" || txtNama.Text == "")
            {
                MessageBox.Show("Kode dan Nama tidak boleh kosong!", "Validasi");
                return;
            }
            try
            {
                byte[] imgBytes = ConvertImageToBytes(pictureBoxBarang);
                dbLogic.TambahBarang(
                    txtKode.Text,
                    txtNama.Text,
                    decimal.Parse(txtHargaBeli.Text),
                    decimal.Parse(txtHargaJual.Text),
                    int.Parse(txtStok.Text),
                    txtSatuan.Text,
                    imgBytes
                );
                MessageBox.Show("Data Berhasil Disimpan!", "Sukses");
                BersihkanInput();
                TampilkanData();
                HitungTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Simpan: " + ex.Message);
            }
        }

        // =====================
        // TOMBOL UPDATE
        // =====================
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
                    byte[] imgBytes = ConvertImageToBytes(pictureBoxBarang);
                    dbLogic.UpdateBarang(
                        txtKode.Text,
                        txtNama.Text,
                        decimal.Parse(txtHargaBeli.Text),
                        decimal.Parse(txtHargaJual.Text),
                        int.Parse(txtStok.Text),
                        txtSatuan.Text,
                        imgBytes
                    );
                    MessageBox.Show("Data Berhasil Diperbarui!", "Sukses");
                    BersihkanInput();
                    TampilkanData();
                    HitungTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Update: " + ex.Message);
                }
            }
        }

        // =====================
        // TOMBOL HAPUS
        // =====================
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
                    dbLogic.HapusBarang(txtKode.Text);
                    MessageBox.Show("Data Berhasil Dihapus!");
                    BersihkanInput();
                    TampilkanData();
                    HitungTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // =====================
        // UPLOAD FOTO BARANG
        // =====================
        private void btnUploadFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxBarang.Image = Image.FromFile(ofd.FileName);
                pictureBoxBarang.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        // =====================
        // CONVERT GAMBAR BYTES
        // =====================
        private byte[] ConvertImageToBytes(PictureBox pb)
        {
            if (pb.Image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                pb.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        // =====================
        // KLIK BARIS GRID ISI FORM + TAMPIL FOTO
        // =====================
        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataRow row = ((DataRowView)bs[e.RowIndex]).Row;

                txtKode.Text = row["KodeBarang"].ToString();
                txtNama.Text = row["NamaBarang"].ToString();
                txtHargaBeli.Text = row["HargaBeli"].ToString();
                txtHargaJual.Text = row["HargaJual"].ToString();
                txtStok.Text = row["Stok"].ToString();
                txtSatuan.Text = row["Satuan"].ToString();

                if (row["Foto"] != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])row["Foto"];
                    using (MemoryStream ms = new MemoryStream(imgBytes))
                    {
                        pictureBoxBarang.Image = Image.FromStream(ms);
                        pictureBoxBarang.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    pictureBoxBarang.Image = null;
                }
            }
        }

        // =====================
        // CARI BARANG OTOMATIS
        // =====================
        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bs.DataSource = dbLogic.SearchBarang(txtCari.Text);
                dgvBarang.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================
        // IMPORT DARI EXCEL
        // =====================
        private void btnImportExcel_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog
            { Filter = "Excel Workbook|*.xlsx;*.xls" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    try
                    {
                        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                { UseHeaderRow = true }
                            });

                            DataTable dt = result.Tables[0];

                            // Normalisasi header: hapus spasi + lowercase
                            // Supaya "KODE BARANG" == "KodeBarang" == "kode barang"
                            DataTable dtNormal = NormalizeColumnNames(dt);
                            
                            // Validasi kolom wajib ada
                            string[] kolom = { "kodebarang", "namabarang", "hargabeli", "hargajual", "stok", "satuan" };
                            List<string> missing = new List<string>();
                            foreach (var k in kolom)
                                if (!dtNormal.Columns.Contains(k))
                                    missing.Add(k);

                            if (missing.Count > 0)
                            {
                                MessageBox.Show(
                                    "Kolom tidak ditemukan:\n" + string.Join(", ", missing) +
                                    "\n\nHeader Excel harus ada: KodeBarang, NamaBarang, HargaBeli, HargaJual, Stok, Satuan",
                                    "Error Kolom", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Simpan di Tag supaya btnImportDB bisa akses
                            dgvBarang.DataSource = null;
                            dgvBarang.DataSource = dtNormal;
                            dgvBarang.Tag = dtNormal;
                            dgvBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            MessageBox.Show(
                                dt.Rows.Count + " baris berhasil dibaca dari Excel.\n" +
                                "Klik 'Import ke Database' untuk menyimpan.",
                                "Preview Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal membaca Excel:\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =====================
        // IMPORT EXCEL DATABASE
        // =====================
        private void btnImportDB_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvBarang.Tag as DataTable;

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data. Lakukan Import Excel dulu!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                "Akan mengimport " + dt.Rows.Count + " baris ke database. Lanjutkan?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            int sukses = 0, gagal = 0, skip = 0;
            List<string> pesanGagal = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    // Pakai GetCell (lowercase) karena sudah dinormalisasi
                    string kode = GetCell(row, "kodebarang");
                    string nama = GetCell(row, "namabarang");
                    string satuan = GetCell(row, "satuan");

                    if (string.IsNullOrWhiteSpace(kode) || string.IsNullOrWhiteSpace(nama))
                    { skip++; continue; }

                    if (!decimal.TryParse(GetCell(row, "hargabeli"), out decimal beli))
                    { pesanGagal.Add(kode + ": HargaBeli tidak valid"); gagal++; continue; }

                    if (!decimal.TryParse(GetCell(row, "hargajual"), out decimal jual))
                    { pesanGagal.Add(kode + ": HargaJual tidak valid"); gagal++; continue; }

                    if (!int.TryParse(GetCell(row, "stok"), out int stok))
                    { pesanGagal.Add(kode + ": Stok tidak valid"); gagal++; continue; }

                    dbLogic.TambahBarang(kode, nama, beli, jual, stok, satuan, null);
                    sukses++;
                }
                catch (Exception ex)
                {
                    pesanGagal.Add("Error: " + ex.Message);
                    gagal++;
                }
            }

            string hasil = $"Import selesai!\n✅ Berhasil : {sukses}\n⏭️ Dilewati : {skip}\n❌ Gagal    : {gagal}";
            if (pesanGagal.Count > 0)
                hasil += "\n\nDetail:\n" + string.Join("\n", pesanGagal);

            MessageBox.Show(hasil, "Hasil Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvBarang.Tag = null;
            TampilkanData();
            HitungTotal();
        }

        // =====================
        // BUKA KASIR
        // =====================
        private void btnKasir_Click(object sender, EventArgs e)
        {
            Form2 kasir = new Form2();
            kasir.Show();
            this.Hide();
            kasir.FormClosed += (s, args) =>
            {
                this.Show();
                TampilkanData();
                HitungTotal();
            };
        }

        // =====================
        // BUKA DASHBOARD
        // =====================
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboard dash = new FormDashboard();
            dash.Show();
            this.Hide();
            dash.FormClosed += (s, args) => this.Show();
        }

        // =====================
        // REFRESH & BERSIHKAN
        // =====================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BersihkanInput();
            TampilkanData();
            HitungTotal();
        }

        private void BersihkanInput()
        {
            txtKode.Clear();
            txtNama.Clear();
            txtHargaBeli.Clear();
            txtHargaJual.Clear();
            txtStok.Clear();
            txtSatuan.Clear();
            pictureBoxBarang.Image = null;
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            TampilkanData();
            HitungTotal();
        }
        private DataTable NormalizeColumnNames(DataTable dt)
        {
            DataTable clone = dt.Clone();
            foreach (DataColumn col in clone.Columns)
                col.ColumnName = col.ColumnName.Replace(" ", "").ToLower().Trim();
            foreach (DataRow row in dt.Rows)
                clone.ImportRow(row);
            return clone;
        }

        private string GetCell(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName)) return string.Empty;
            return row[columnName]?.ToString()?.Trim() ?? string.Empty;
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }

        private void button11_Click(object sender, EventArgs e)
        {
            FormRekapData frm = new FormRekapData();
            frm.Show();
            this.Hide();
            frm.FormClosed += (s, args) => this.Show();
        }
    }
}