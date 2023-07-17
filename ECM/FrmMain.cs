using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECM
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if(!(tn is null))
            {
                if(!(tn.Tag is null))
                {
                    switch (byte.Parse(tn.Tag.ToString()))
                    {
                        case 1:
                            {
                                FrmParam fP = new FrmParam();
                                fP.ShowDialog();
                                break;
                            }

                        case 2:
                            {
                                FrmEMViewer fEE = new FrmEMViewer();
                                fEE.ShowDialog();
                                break;
                            }

                        case 3:
                            {
                                FrmCustomersViewer fCE = new FrmCustomersViewer();
                                fCE.ShowDialog();
                                break;
                            }

                        case 4:
                            {
                                FrmRecordEntry fRE = new FrmRecordEntry();
                                fRE.ShowDialog();
                               
                                break;
                            }

                        case 5:
                            {
                                FrmCustomersReport fCR = new FrmCustomersReport();
                                fCR.ShowDialog();
                                break;
                            }

                        case 6:
                            {
                                FrmHistoryRecordReport FHRR = new FrmHistoryRecordReport();
                                FHRR.ShowDialog();
                                break;
                            }

                        case 7:
                            {
                                FrmBillsReport fBR = new FrmBillsReport();
                                fBR.ShowDialog();
                                break;
                            }

                        case 8:
                            {
                                break;
                            }

                        case 9:
                            {
                                FrmCyleViewer fCV = new FrmCyleViewer();
                                fCV.ShowDialog();
                                break;
                            }
                    }
                }
            }
        }
    }
}
