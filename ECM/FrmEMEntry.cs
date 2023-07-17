namespace ECM
{
    using System;
    using System.Windows.Forms;
    using DSLib;
    using ECM;
    public partial class FrmEMEntry : Form
    {
        public FrmEMEntry()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;

            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            res = true;
            return res;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((!new ECM.EMController(new AccessDB(Constants.ConnectionString)).Add(new EM(0, textBox1.Text, 1, false,0))))
            {
                MessageBox.Show("تعذر تنفيذ العملية"); return;
            }

            this.Close();
        }
    }
}
