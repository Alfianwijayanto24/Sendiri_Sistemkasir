using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemKasir
{
    public partial class FormCetakTransaksi: Form
    {
        List<DataTransaksi> listTransaksi;
        string tanggal;

        public FormCetakTransaksi(List<DataTransaksi> data, string tgl)
        {
            InitializeComponent();
            listTransaksi = data;
            tanggal = tgl;
        }

        private void FormCetakTransaksi_Load(object sender, EventArgs e)
        {
            RptTransaksi rpt = new RptTransaksi();
            rpt.SetDataSource(listTransaksi);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }

        private void FormCetakTransaksi_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is FormRekapData)
                {
                    f.Show();
                    break;
                }
            }
        }
    }
    
 }
