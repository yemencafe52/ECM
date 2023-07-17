namespace ECM
{
    using DSLib;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class EMHDetails
    {
        private int emhdNmber;
        private int emhNumber;
        private int cusNumber;
        private int emNumber;
        private double emhdLastValue;
        private double emhdCurrentValue;
        private double emhdDiff;

        public EMHDetails(int emhdNmber, int emhNumber, int cusNumber, int emNumber, double emhdLastValue, double emhdCurrentValue, double emhdDiff)
        {
            this.emhdNmber = emhdNmber;
            this.emhNumber = emhNumber;
            this.cusNumber = cusNumber;
            this.emNumber = emNumber;
            this.emhdLastValue = emhdLastValue;
            this.emhdCurrentValue = emhdCurrentValue;
            this.emhdDiff = emhdDiff;
        }

        public int EmhdNmber { get => emhdNmber;  }
        public int EmhNumber { get => emhNumber; }
        public int CusNumber { get => cusNumber; }
        public int EmNumber { get => emNumber; }
        public double EmhdLastValue { get => emhdLastValue;  }
        public double EmhdCurrentValue { get => emhdCurrentValue; }
        public double EmhdDiff { get => emhdDiff;}
    }

    public class EMHDetailsController
    {
        private readonly AccessDB db;
        public EMHDetailsController(AccessDB db)
        {
            this.db = db;
        }
        internal bool Add(EMHDetails emhd)
        {
            bool res = false;
            string sql = string.Format("insert into tblEMHDetailes " +
                "(" +
                   // "emhdNmber" +
                   // "," +
                    "emhNumber" +
                    "," +
                    "cusNumber" +
                    "," +
                    "emNumber" +
                    "," +
                    "emhdLastValue" +
                    "" +
                    "," +
                    "emhdCurrentValue" +
                    "," +
                    "emhdDiff" +
                ")" +
                " values" +
                "(" +
                    "{0}" +
                    "," +
                    "{1}" +
                    "," +
                    "{2}" +
                    "," +
                    "{3}" +
                    "," +
                    "{4}" +
                    "," +
                    "{5}" +
                  //  "," +
                  //  "{6}" +
                ") ", new object[] 
                { 
                   // emhd.EmhdNmber
                   // ,
                    emhd.EmhNumber
                    ,
                    emhd.CusNumber
                    ,
                    emhd.EmNumber
                    ,
                    emhd.EmhdLastValue
                    ,
                    emhd.EmhdCurrentValue
                    ,
                    emhd.EmhdDiff
                }
            );
            res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            return res;
        }
        public bool Query(int number,out EMHDetails emh)
        {
            bool res = false;
            emh = null;

            string sql = "select emhdNmber,emhNumber,cusNumber,emNumber,emhdLastValue,emhdCurrentValue,emhdDiff from tblEMHDetailes where emhdNmber=" + number;
            DataTable dt = new DataTable("tblTemp");

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                emh = new EMHDetails(
                    int.Parse(dt.Rows[0][0].ToString())
                    ,
                    int.Parse(dt.Rows[0][1].ToString())
                    ,
                    int.Parse(dt.Rows[0][2].ToString())
                    ,
                    int.Parse(dt.Rows[0][3].ToString())
                    ,
                    double.Parse(dt.Rows[0][4].ToString())
                    ,
                    double.Parse(dt.Rows[0][5].ToString())
                    ,
                    double.Parse(dt.Rows[0][6].ToString())
                    );
                res = true;
            }

            return res;
        }

        public bool Query(int number,ref List<EMHDetails> emh)
        {
            bool res = false;
            emh.Clear();

            string sql = "select " +
                "emhdNmber" +
                "," +
                "emhNumber" +
                "," +
                "cusNumber" +
                "," +
                "emNumber" +
                "," +
                "emhdLastValue" +
                "," +
                "emhdCurrentValue" +
                "," +
                "emhdDiff" +
                "" +
                " from tblEMHDetailes where emhNumber=" + number;
            DataTable dt = new DataTable("tblTemp");

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    emh.Add(new EMHDetails(
                        int.Parse(dt.Rows[i][0].ToString())
                        ,
                        int.Parse(dt.Rows[i][1].ToString())
                        ,
                        int.Parse(dt.Rows[i][2].ToString())
                        ,
                        int.Parse(dt.Rows[i][3].ToString())
                        ,
                        double.Parse(dt.Rows[i][4].ToString())
                        ,
                        double.Parse(dt.Rows[i][5].ToString())
                        ,
                        double.Parse(dt.Rows[i][6].ToString())
                        ));
                }
                res = true;
            }

            return res;
        }
    }
}
