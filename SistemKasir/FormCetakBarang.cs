using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SistemKasir
{
    public partial class FormCetakBarang : Form
    {
        public FormCetakBarang()
        {
            InitializeComponent();
        }

        public void LoadData(DataTable dt)
        {
            RptBarang rpt = new RptBarang();
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}