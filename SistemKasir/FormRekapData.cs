using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace SistemKasir
{
    public partial class FormRekapData : Form
    {
        DAL dbLogic = new DAL();

        public FormRekapData()
        {
            InitializeComponent();
        }

        private void FormRekapData_Load(object sender, EventArgs e)
        {
            dtpTanggal.Format = DateTimePickerFormat.Short;
            dtpTanggal.Value = DateTime.Now;
            cmbJenis.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJenis.Items.Add("Laporan Barang");
            cmbJenis.Items.Add("Laporan Transaksi");
            cmbJenis.SelectedIndex = 0;
            btnCetak.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbJenis.SelectedIndex == 0)
                    dataGridView1.DataSource = dbLogic.GetReportBarang();
                else
                {
                    string tgl = dtpTanggal.Value.ToString("yyyy-MM-dd");
                    dataGridView1.DataSource = dbLogic.GetReportTransaksi(tgl);
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                btnCetak.Enabled = dataGridView1.Rows.Count > 0;
                if (dataGridView1.Rows.Count == 0)
                    MessageBox.Show("Data tidak ditemukan.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load: " + ex.Message);
            }
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            if (cmbJenis.SelectedIndex == 0)
            {
                FormCetakBarang frm = new FormCetakBarang();
                var list = dbLogic.GetReportBarang();
                DataTable dt = ToDataTable(list);
                frm.LoadData(dt);
                frm.Show();
                this.Hide();
            }
            else
            {
                string tgl = dtpTanggal.Value.ToString("yyyy-MM-dd");
                FormCetakTransaksi frm = new FormCetakTransaksi(
                    dbLogic.GetReportTransaksi(tgl),
                    dtpTanggal.Value.ToString("dd/MM/yyyy"));
                frm.Show();
                this.Hide();
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (var prop in props)
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in items)
            {
                var row = dt.NewRow();
                foreach (var prop in props)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}