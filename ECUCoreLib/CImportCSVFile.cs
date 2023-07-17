namespace ECUCoreLib
{
    using DSLib;
    using ECM;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ImportCSVFile
    {
        private readonly AccessDB db;
        private readonly string filePath;

        public ImportCSVFile(AccessDB db,string filePath)
        {
            this.db = db;
            this.filePath = filePath;
        }

        public bool Import(EMHistory emh,EMController emc,CustomerController cc,IProgress<float> p)
        {
            bool res = false;

            List<Row> rows = Row.GetRows(this.filePath);

            if (rows.Count > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    EM e = null;
                    if (emc.Query((uint)rows[i].EmNumber, out e))
                    {
                        Customer c = null;
                        if (cc.QueryByEMNumber(e.EmNumber, out c))
                        {
                            double nRead = rows[i].CuurenValue - e.CurrentValue;
                            if (nRead > 0)
                            {
                                if (emc.UpdateCurrenValue(rows[i].EmNumber, rows[i].CuurenValue))
                                {
                                    EMHDetails emhd = new EMHDetails(0, emh.EmhNumber, (int)c.CusNumber, (int)e.EmNumber, e.CurrentValue, rows[i].CuurenValue, nRead);

                                    while (!(new EMHDetailsController(this.db).Add(emhd)))
                                    {
                                        Thread.Sleep(100);
                                    }
                                }
                            }
                        }
                    }

                    p.Report((float)i / (float)rows.Count * (float)100);
                }

                res = true;
            }

            return res;
        }

        internal class Row
        {
            private int emNumber;
            private double cuurenValue;

            internal Row(int emNumber, double cuurenValue)
            {
                this.emNumber = emNumber;
                this.cuurenValue = cuurenValue;
            }

            internal int EmNumber { get => emNumber;  }
            internal double CuurenValue { get => cuurenValue; }

            internal static List<Row> GetRows(string filePath)
            {
                List<Row> res = new List<Row>();

                try
                {
                    string[] ar = System.IO.File.ReadAllLines(filePath);

                    foreach (string r in ar)
                    {
                        string[] ar2 = r.Split(new char[] { ';' }, StringSplitOptions.None);
                        res.Add(new Row(int.Parse(ar2[0]), double.Parse(ar2[1])));
                    }
                }
                catch {
                    res.Clear();
                }

                return res;
            }
        }


    }

   
}
