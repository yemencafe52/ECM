namespace ECUCoreLib
{
    using DSLib;
    using System.Data;
    public class Report
    {
        private readonly AccessDB db;

        public Report(AccessDB db)
        {
            this.db = db;
        }
        public bool GetReport1(ref DataTable dt)
        {
            bool res = false;
            string sql = "SELECT tblEM.emNumber, tblEM.emSN, tblEM.emPhese, tblEM.emState, tblCustomers.cusNumber, tblCustomers.cusName, tblCustomers.cusPhone, tblCustomers.emNumber AS Expr1, tblBills.billNumber, tblBills.emNumber AS Expr2, tblBills.cusNumber AS Expr3, tblBills.emhNumber, tblBills.emLastValue, tblBills.emCurrentValue, tblBills.billAmount, tblBills.billPricePerKw, tblBills.billServicesAmount, tblBills.billDiff, tblBills.billDate FROM(tblEM INNER JOIN tblCustomers ON tblEM.emNumber = tblCustomers.emNumber) INNER JOIN tblBills ON(tblCustomers.cusNumber = tblBills.cusNumber) AND(tblEM.emNumber = tblBills.emNumber)";
            res = this.db.ExecuteQuery(sql, ref dt) > 0 ? true:false;
            return res;
        }

        public bool GetReport1(int from, int to, ref DataTable dt)
        {
            bool res = false;
            string sql = "SELECT tblEM.emNumber, tblEM.emSN, tblEM.emPhese, tblEM.emState, tblCustomers.cusNumber, tblCustomers.cusName, tblCustomers.cusPhone, tblCustomers.emNumber AS Expr1, tblBills.billNumber, tblBills.emNumber AS Expr2, tblBills.cusNumber AS Expr3, tblBills.emhNumber, tblBills.emLastValue, tblBills.emCurrentValue, tblBills.billAmount, tblBills.billPricePerKw, tblBills.billServicesAmount, tblBills.billDiff, tblBills.billDate FROM(tblEM INNER JOIN tblCustomers ON tblEM.emNumber = tblCustomers.emNumber) INNER JOIN tblBills ON(tblCustomers.cusNumber = tblBills.cusNumber) AND(tblEM.emNumber = tblBills.emNumber)";
            this.db.ExecuteQuery(sql, ref dt);
            return res;
        }
    }
}
