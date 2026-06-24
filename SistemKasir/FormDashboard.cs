using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SistemKasir
{
    public partial class FormDashboard : Form
    {
        DAL dbLogic = new DAL();
        bool isInitializing = true;
        DataTable dt;
        int button = 0;

        public FormDashboard()
        {
            InitializeComponent();

            // Setup DateTimePicker hanya tampil tahun
            dateTimePicker1.MinDate = new DateTime(2000, 1, 1);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.MinDate = new DateTime(2000, 1, 1);
            dateTimePicker1.MaxDate = DateTime.Now;

            // Setup ComboBox tipe chart
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            List<KeyValuePair<string, SeriesChartType>> items =
                new List<KeyValuePair<string, SeriesChartType>>();
            items.Add(new KeyValuePair<string, SeriesChartType>("Kolom", SeriesChartType.Column));
            items.Add(new KeyValuePair<string, SeriesChartType>("Pie", SeriesChartType.Pie));

            isInitializing = true;
            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            comboBox1.SelectedIndex = 0;
            isInitializing = false;

            LoadDataChart();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            // handled di constructor
        }

        public void LoadDataChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();
            chart1.ChartAreas.Clear();

            ChartArea ca = new ChartArea("MainArea");
            ca.AxisX.Title = "Nama Barang";
            ca.AxisY.Title = "Total Terjual";
            ca.AxisX.LabelStyle.Angle = -45;
            ca.BackColor = System.Drawing.Color.Transparent;
            chart1.ChartAreas.Add(ca);

            try
            {
                dt = (button == 1)
                    ? dbLogic.GetDataChartByTahun(dateTimePicker1.Value)
                    : dbLogic.GetAllDataChart();

                SeriesChartType tipe = (SeriesChartType)comboBox1.SelectedValue;

                Series s = new Series("Penjualan");
                s.ChartType = tipe;

                if (tipe == SeriesChartType.Pie)
                {
                    s.IsValueShownAsLabel = true;
                    s.Label = "#VAL";
                    s.LegendText = "#VALX";
                }

                foreach (DataRow row in dt.Rows)
                {
                    string nama = row["NamaBarang"].ToString();
                    int total = Convert.ToInt32(row["TotalTerjual"]);
                    s.Points.AddXY(nama, total);
                }

                chart1.Series.Add(s);

                Title title = new Title(
                    "Total Penjualan per Barang",
                    Docking.Top,
                    new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                    System.Drawing.Color.DarkBlue);
                chart1.Titles.Add(title);

                Legend legend = new Legend("MainLegend");
                legend.Docking = Docking.Right;
                chart1.Legends.Add(legend);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load data: " + ex.Message);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;
            LoadDataChart();
        }

        private void button3_Click(object sender, EventArgs e) // Load
        {
            button = 1;
            LoadDataChart();
        }

        private void button2_Click(object sender, EventArgs e) // Reset
        {
            button = 0;
            LoadDataChart();
        }

        private void button1_Click(object sender, EventArgs e) // Kembali
        {
            this.Close();
        }

        private void FormDashboard_Load_1(object sender, EventArgs e)
        {

        }
    }
}