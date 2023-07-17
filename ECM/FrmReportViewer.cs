namespace ECM
{
    using System;
    using System.Windows.Forms;
    using Microsoft.Reporting.WinForms;
    public partial class FrmReportViewer : Form
    {
        public FrmReportViewer(ReportDataSource rds,string tempelateName)
        {
            InitializeComponent();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = tempelateName;
        }

        private void FrmReportViewer_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
