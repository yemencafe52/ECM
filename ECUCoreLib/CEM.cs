namespace ECM
{
    using DSLib;
    using System;
    using System.Collections.Generic;
    using System.Data;
    

    public class EM
    {
        private uint emNumber;
        private string emSN;
        private byte emPhese;
        private bool emState;
        private double currentValue;

        /// <summary>
        /// em constructor
        /// </summary>
        /// <param name="emNumber"></param>
        /// <param name="emSN"></param>
        /// <param name="emPhese"></param>
        /// <param name="emState"></param>
        public EM(uint emNumber, string emSN, byte emPhese, bool emState, double currentValue)
        {
            this.emNumber = emNumber;
            this.emSN = emSN;
            this.emPhese = emPhese;
            this.emState = emState;
            this.currentValue = currentValue;
        }

        public uint EmNumber { get => emNumber;  }
        public string EmSN { get => emSN;  }
        public byte EmPhese { get => emPhese; }
        public bool EmState { get => emState; }
        public double CurrentValue { get => currentValue; }
    }

    /// <summary>
    /// em controller class, add,update,delete,query,search
    /// </summary>
    public class EMController
    {
        private readonly AccessDB db;
        public EMController (AccessDB db)
        {
            this.db = db;
        }

        /// <summary>
        /// add new em
        /// </summary>
        /// <param name="em">em object</param>
        /// <returns>return true if success else false </returns>
        public bool Add(EM em)
        {
            bool res = false;

            string sql = string.Format("insert into tblEM " +
                "(" +
                    //"emNumber" +
                    //"," +
                    "emSN" +
                    "," +
                    "emPhese" +
                    "," +
                    "emState" +
                    "," +
                    "currentValue" +
                ")" +
                " values" +
                "(" +
                    "'{0}'" +
                    "," +
                    "{1}" +
                    "," +
                    "{2}" +
                    "," +
                    "{3}" +
                ")"
                ,
                new object[] {
                   // em.EmNumber
                   // ,
                    em.EmSN
                    ,
                    em.EmPhese
                    ,
                    em.EmState.ToString()
                    ,
                    em.CurrentValue
                }
            ) ;

            res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            return res;
        }

        public bool Update(uint number,EM em)
        {
            bool res = false;
            return res;
        }

        public bool Delete(uint number)
        {
            bool res = false;
            return res;
        }

        public bool UpdateCurrenValue(int number, double currentValue)
        {
            bool res = false;
            string sql = $"update tblEM set currentValue={currentValue} where emNumber={number}";
            res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            return res;
        }



        public bool Query(uint number, out EM em)
        {
            bool res = false;
            em = null;

            string sql = $"select emNumber,emSN,emPhese,emState,currentValue from tblEM where emNumber={number}";

            DataTable dt = new DataTable("tblTemp");

            if ((this.db.ExecuteQuery(sql, ref dt)) > 0)
            {
                em = new EM(
                    uint.Parse(dt.Rows[0][0].ToString())
                    ,
                    dt.Rows[0][1].ToString()
                    ,
                    byte.Parse(dt.Rows[0][2].ToString())
                    ,
                    Convert.ToBoolean(dt.Rows[0][3].ToString())
                    ,
                    Convert.ToDouble(dt.Rows[0][4].ToString())
                );
                res = true;
            }
            return res;
        }

        public bool Search(uint number,ref List<EM> em)
        {
            bool res = false;

            string sql = $"select emNumber,emSN,emPhese,emState,currentValue from tblEM where emNumber={number}";
            DataTable dt = new DataTable("tblTemp");

            if ((this.db.ExecuteQuery(sql, ref dt)) > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    em.Add(new EM(
                        uint.Parse(dt.Rows[i][0].ToString())
                        ,
                        dt.Rows[i][1].ToString()
                        ,
                        byte.Parse(dt.Rows[i][2].ToString())
                        ,
                        Convert.ToBoolean(dt.Rows[i][3].ToString())
                        ,
                        Convert.ToDouble(dt.Rows[i][4].ToString())
                    ));
                }

                res = true;
            }
            return res;
        }

        public bool GetALL(ref List<EM> em)
        {
            bool res = false;

            string sql = $"select emNumber,emSN,emPhese,emState,currentValue from tblEM";
            DataTable dt = new DataTable("tblTemp");

            if ((this.db.ExecuteQuery(sql, ref dt)) > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    em.Add(new EM(
                        uint.Parse(dt.Rows[i][0].ToString())
                        ,
                        dt.Rows[i][1].ToString()
                        ,
                        byte.Parse(dt.Rows[i][2].ToString())
                        ,
                        Convert.ToBoolean(dt.Rows[i][3].ToString())
                        ,
                        Convert.ToDouble(dt.Rows[i][4].ToString())
                    ));
                }

                res = true;
            }
            return res;
        }

    }
}
