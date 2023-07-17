namespace ECM
{
    using DSLib;
    using System.Collections.Generic;
    using System.Data;

    public class Customer
    {
        private uint cusNumber;
        private string cusName;
        private string cusPhone;
        private uint emNumber;

        public Customer(uint cusNumber, string cusName, string cusPhone, uint emNumber)
        {
            this.cusNumber = cusNumber;
            this.cusName = cusName;
            this.cusPhone = cusPhone;
            this.emNumber = emNumber;
        }

        public uint CusNumber { get => cusNumber; }
        public string CusName { get => cusName; }
        public string CusPhone { get => cusPhone; }
        public uint EmNumber { get => emNumber;}
    }

    /// <summary>
    /// customers controller class, add,update,delete,query,search
    /// </summary>
    public class CustomerController
    {
        private readonly AccessDB db;
        public CustomerController(AccessDB db)
        {
            this.db = db;
        }

        public bool Add(Customer customer)
        {
            bool res = false;
            string sql = string.Format("insert into tblCustomers " +
                "(" +
                   // "cusNumber" +
                   // "," +
                    "cusName" +
                    "," +
                    "cusPhone" +
                    "," +
                    "emNumber" +
                ")" +
                " " +
                "values" +
                "(" +
                    "'{0}'" +
                    "," +
                    "'{1}'" +
                    "," +
                    "{2}" +
                   // "," +
                   // "{3}" +
                ")"
                ,
                new object[] { 
                  //  customer.CusNumber
                  //  ,
                    customer.CusName
                    ,
                    customer.CusPhone
                    ,
                    customer.EmNumber
                }
            );
            res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            return res;
        }

        public bool Update(uint number, Customer customer)
        {
            bool res = false;
            return res;
        }

        public bool Delete(uint number)
        {
            bool res = false;
            return res;
        }

        public bool Query(uint number, out Customer customer)
        {
            bool res = false;
            customer = null;

            string sql = $"select cusNumber,cusName,cusPhone,emNumber from tblCustomers where cusNumber={number}";

            DataTable dt = new DataTable("tblTemp");

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                customer = new Customer(
                    uint.Parse(dt.Rows[0][0].ToString())
                    ,
                    (dt.Rows[0][1].ToString())
                    ,
                    (dt.Rows[0][2].ToString())
                    ,
                    uint.Parse(dt.Rows[0][3].ToString())
                );

                res = true;
            }

            return res;
        }

        public bool QueryByEMNumber(uint number, out Customer customer)
        {
            bool res = false;
            customer = null;

            string sql = $"select cusNumber,cusName,cusPhone,emNumber from tblCustomers where emNumber={number}";

            DataTable dt = new DataTable("tblTemp");

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                customer = new Customer(
                    uint.Parse(dt.Rows[0][0].ToString())
                    ,
                    (dt.Rows[0][1].ToString())
                    ,
                    (dt.Rows[0][2].ToString())
                    ,
                    uint.Parse(dt.Rows[0][3].ToString())
                );

                res = true;
            }

            return res;
        }

        public bool Search(string txt, ref List<Customer> customers)
        {
            bool res = false;
            customers.Clear();

            string sql = $"select cusNumber,cusName,cusPhone,emNumber from tblCustomers where cusName like('%{txt}%')";

            DataTable dt = new DataTable("tblTemp");

            if (this.db.ExecuteQuery(sql, ref dt) > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    customers.Add(new Customer(
                        uint.Parse(dt.Rows[i][0].ToString())
                        ,
                        (dt.Rows[i][1].ToString())
                        ,
                        (dt.Rows[i][2].ToString())
                        ,
                        uint.Parse(dt.Rows[i][3].ToString())
                    ));
                }

                res = true;
            }

            return res;
        }
    }
}
