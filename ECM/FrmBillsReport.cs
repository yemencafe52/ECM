using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ECM
{
    public partial class FrmBillsReport : Form
    {
        public FrmBillsReport()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            numericUpDown2.Enabled = false;
            res = true;
            return res;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            new ECUCoreLib.Report(new DSLib.AccessDB(Constants.ConnectionString)).GetReport1(ref dt);
            ReportDataSource rds = new ReportDataSource("tblTempECM_2", dt);
            FrmReportViewer fRV = new FrmReportViewer(rds, "ECM.RptBill.rdlc");
            fRV.ShowDialog();
        }
    }
}
