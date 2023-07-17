namespace ECM
{
    using DSLib;
    using System;
    using System.Windows.Forms;
    public partial class FrmCustomerEntry : Form
    {
        public FrmCustomerEntry()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;

            numericUpDown1.Enabled = false;
            res = true;
            return res;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((!new ECM.CustomerController(new AccessDB(Constants.ConnectionString)).Add(new Customer(0, textBox1.Text, textBox2.Text, (uint)numericUpDown2.Value))))
            {
                MessageBox.Show("تعذر تنفيذ العملية"); return;
            }

            this.Close();
        }
    }
}
