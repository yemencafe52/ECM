namespace ECM
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    public partial class FrmEMViewer : Form
    {
        public FrmEMViewer()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;


            res = true;
            return res;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmEMEntry fEME = new FrmEMEntry();
            fEME.ShowDialog();
        }

        private void Search()
        {
            List<ECM.EM> ar = new List<EM>();
            new ECM.EMController(new DSLib.AccessDB(Constants.ConnectionString)).GetALL(ref ar);
            Display(ar);
        }

        private void Display(List<ECM.EM> ar)
        {
            Clear();

            List<ListViewItem> l = new List<ListViewItem>();

            for(int i = 0; i < ar.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(ar[i].EmNumber.ToString());
                lvi.SubItems.Add(ar[i].EmSN);
                lvi.SubItems.Add(ar[i].EmPhese.ToString());
                lvi.SubItems.Add("");

                l.Add(lvi);
            }

            this.listView1.Items.AddRange(l.ToArray());
            this.toolStripStatusLabel2.Text = l.Count.ToString();
        }

        private void Clear()
        {
            this.listView1.Items.Clear();
            this.toolStripStatusLabel2.Text = "0";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void FrmEMViewer_Shown(object sender, EventArgs e)
        {
            toolStripButton2.PerformClick();
        }
    }
}
