namespace DSLib
{
    using System.Data;
    using System.Data.OleDb;
    public class AccessDB
    {
        private readonly string connectionString;

        public AccessDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int ExecuteNoneQuery(string sql)
        {
            int res = 0;

            try
            {
                using(OleDbConnection con = new OleDbConnection(this.connectionString))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch { }
            return res;
        }

        public int ExecuteQuery(string sql,ref DataTable dt)
        {
            int res = 0;

            try
            {
                using (OleDbDataAdapter adp = new OleDbDataAdapter(sql, new OleDbConnection(this.connectionString)))
                {
                    adp.Fill(dt);
                    res = dt.Rows.Count;
                }
            }
            catch { }
            return res;
        }
    }

}
