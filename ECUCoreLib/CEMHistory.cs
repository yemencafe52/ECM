namespace ECM
{
    using DSLib;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class EMHistory
    {
        private int emhNumber;
        private DateTime emhDate;
        private bool emhClosed;

        public EMHistory(int emhNumber, DateTime emhDate, bool emhClosed)
        {
            this.emhNumber = emhNumber;
            this.emhDate = emhDate;
            this.emhClosed = emhClosed;
        }

        public int EmhNumber { get => emhNumber; }
        public DateTime EmhDate { get => emhDate; }
        public bool EmhClosed { get => emhClosed; }
    }

    public class EMHistoryController
    {
        private readonly AccessDB db;
        public EMHistoryController(AccessDB db)
        {
            this.db = db;
        }

        public bool Add(EMHistory h)
        {
            bool res = false;

            if (CanOpen())
            {
                string sql = string.Format("insert into tblEMHistory " +
                    "(" +
                        //   "emhNumber" +
                        //   "," +
                        "emhDate" +
                        "," +
                        "emhClosed" +
                    ")" +
                    " " +
                    "values" +
                    "(" +
                        //   "{0}" +
                        //   "," +
                        "'{0}'" +
                        "," +
                        "{1}" +
                    ")"
                    ,
                    new object[] {
                   // h.EmhNumber
                  //  ,
                    h.EmhDate
                    ,
                    "False"
                    }
                );

                res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            }

            return res;
        }

        public bool Close(int number)
        {
            bool res = false;

            if (CanClose(number))
            {
                string sql = $"update tblEMHistory set emhClosed=True where emhNumber={number}";
                res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            }
            return res;
        }

        public bool GetALL(ref List<EMHistory> ar)
        {
            bool res = false;

            string sql = "select emhNumber,emhDate,emhClosed from tblEMHistory";
            DataTable dt = new DataTable("tblTemp");
            
            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ar.Add(new EMHistory(
                        int.Parse(dt.Rows[i][0].ToString())
                        ,
                        DateTime.Parse(dt.Rows[i][1].ToString())
                        ,
                        Convert.ToBoolean(dt.Rows[i][2].ToString())));
                }

                res = true;
            }

            return res;
        }

        public bool Read(int number, out EMHistory e)
        {
            bool res = false;

            string sql = $"select emhNumber,emhDate,emhClosed from tblEMHistory where emhNumber={number}";
            DataTable dt = new DataTable("tblTemp");
            e = null;

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                    e = new EMHistory(
                        int.Parse(dt.Rows[0][0].ToString())
                        ,
                        DateTime.Parse(dt.Rows[0][1].ToString())
                        ,
                        Convert.ToBoolean(dt.Rows[0][2].ToString()));
                //}

                res = true;
            }

            return res;
        }

        private bool CanOpen()
        {
            bool res = false;

            string sql = "select count(*) as res from tblEMHistory where emhClosed=False";
            DataTable dt = new DataTable("tblTemp");

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                if (int.Parse(dt.Rows[0][0].ToString()) == 0)
                {
                    res = true;
                }
            }

            return res;
        }

        private bool CanClose(int number)
        {
            bool res = false;

            string sql = $"select count(*) as res from tblEMHDetailes where emhNumber={number}";
            DataTable dt = new DataTable("tblTemp");

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                List<Customer> ar = new List<Customer>();
                if (new CustomerController(this.db).Search("", ref ar))
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) == ar.Count)
                    {
                        res = true;
                    }
                }
            }

            return res;
        }
    }
}
