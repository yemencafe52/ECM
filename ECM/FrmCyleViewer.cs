namespace ECM
{
    using ECUCoreLib;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    public partial class FrmCyleViewer : Form
    {
        private bool isRunning;
        public FrmCyleViewer()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;

            List<ECM.EMHistory> ar = new List<EMHistory>();
            new ECM.EMHistoryController(new DSLib.AccessDB(Constants.ConnectionString)).GetALL(ref ar);
            Display(ar);
            res = true;
            return res;
        }

        private void Open()
        {
            if (!(new ECM.EMHistoryController(new DSLib.AccessDB(Constants.ConnectionString)).Add(new EMHistory(0, DateTime.Now, false))))
            {
                MessageBox.Show("تعذر تنفيذ العملية"); return;
            }

            Preparing();
        }

        private void CloseC()
        {

        }

        private void GenerateBills()
        {

        }

        private void Clear()
        {
            this.listView1.Items.Clear();
            //toolStripStatusLabel2.Text = "0";
        }

        private void Display(List<ECM.EMHistory> ar)
        {
            Clear();
            foreach(EMHistory s in ar)
            {
                ListViewItem lvi = new ListViewItem(s.EmhNumber.ToString());
                lvi.SubItems.Add(s.EmhDate.ToString("yyyy/MM/dd"));
                lvi.SubItems.Add(CUtilities.GetStateInString(s.EmhClosed));
                this.listView1.Items.Add(lvi);
            }

            //toolStripStatusLabel2.Text = ar.Count.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Preparing();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //if (this.listView1.SelectedItems.Count > 0)
            //{
               
            //    int number = int.Parse(this.listView1.SelectedItems[0].Text);
            //    if (!(new ECM.EMHistoryController(new DSLib.AccessDB(Constants.ConnectionString)).Close(number)))
            //    {
            //        MessageBox.Show("تعذر تنفيذ العملية"); return;
            //    }
            //}

            //Preparing();
        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.isRunning = true;
                this.toolStrip1.Enabled = false;

                int num = int.Parse(this.listView1.SelectedItems[0].Text);

                var x = new Progress<float>(i =>
                {
                    toolStripProgressBar1.Minimum = 0;
                    toolStripProgressBar1.Maximum = 100;
                    toolStripProgressBar1.Value = (int)i;
                   
                });

                bool res = false;
                res = await Task.Run(() =>
                    new BillGenerator(new DSLib.AccessDB(Constants.ConnectionString)).GenerateBills(num, new ECUCoreLib.CParam(250, 0, 7), x)
                );

                if (res)
                {
                    while (!(new ECM.EMHistoryController(new DSLib.AccessDB(Constants.ConnectionString)).Close(num)))
                    {
                        Thread.Sleep(100);
                    }

                    MessageBox.Show("تمت العملية بنجاح");
                }
                else
                {
                    MessageBox.Show("تعذر تنفيذ العملية");
                }

              
                this.toolStrip1.Enabled = true; ;
                this.isRunning = false;

                Preparing();
            }
            else
            {
                MessageBox.Show("تعذر تنفيذ العملية");
            }

            toolStripProgressBar1.Minimum = 0;
        }
        private void FrmCyleViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isRunning)
            {
                e.Cancel = true;
                MessageBox.Show("يجب الانتظار حتى إنتهاء العملية");
            }
        }

        private async void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                int num = int.Parse(listView1.SelectedItems[0].Text);

                EMHistory emh;
                if (new EMHistoryController(new DSLib.AccessDB(Constants.ConnectionString)).Read(num, out emh))
                {
                    this.isRunning = true;
                    this.toolStrip1.Enabled = false;

                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "CSV File|*.csv";

                    DialogResult r = ofd.ShowDialog(this);
                    if (r == DialogResult.OK)
                    {
                        var x = new Progress<float>(i => {
                            toolStripProgressBar1.Minimum = 0;
                            toolStripProgressBar1.Maximum = 100;
                            toolStripProgressBar1.Value = (int)i;

                        });

                        EMController emc = new EMController(new DSLib.AccessDB(Constants.ConnectionString));
                        CustomerController cc = new CustomerController(new DSLib.AccessDB(Constants.ConnectionString));

                        ImportCSVFile icf = new ImportCSVFile(new DSLib.AccessDB(Constants.ConnectionString), ofd.FileName);
                        await Task.Run(() => { icf.Import(emh, emc, cc, x); }) ;
                    }

                    this.toolStrip1.Enabled = true;
                    this.isRunning = false;
                }

                MessageBox.Show("تمت العملية بنجاح");
            }
            else
            {
                MessageBox.Show("تعذر تنفيذ العملية");
            }

            toolStripProgressBar1.Value = 0;
        }
    }
}
