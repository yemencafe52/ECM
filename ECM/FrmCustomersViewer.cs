namespace ECM
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    public partial class FrmCustomersViewer : Form
    {
        public FrmCustomersViewer()
        {
            InitializeComponent();
            Preparing();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmCustomerEntry fCE = new FrmCustomerEntry();
            fCE.ShowDialog();
        }

     
        private bool Preparing()
        {
            bool res = false;


            res = true;
            return res;
        }

      
        private void Search()
        {
            List<ECM.Customer> ar = new List<Customer>();
            new ECM.CustomerController(new DSLib.AccessDB(Constants.ConnectionString)).Search(toolStripTextBox1.Text,ref ar);
            Display(ar);
        }

        private void Display(List<ECM.Customer> ar)
        {
            Clear();

            List<ListViewItem> l = new List<ListViewItem>();

            for (int i = 0; i < ar.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(ar[i].EmNumber.ToString());
                lvi.SubItems.Add(ar[i].CusName);
                lvi.SubItems.Add(ar[i].CusPhone.ToString());
                lvi.SubItems.Add(ar[i].EmNumber.ToString());

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

        private void FrmCustomersViewer_Shown(object sender, EventArgs e)
        {
            toolStripButton2.PerformClick();
        }

      
    }
}
