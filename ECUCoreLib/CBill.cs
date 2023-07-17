namespace ECM
{
    using ECUCoreLib;
    using System;
    using System.Collections.Generic;
    public class Bill
    {
        private int billNumber;
        private int emNumber;
        private int cusNumber;
        private int emhNumber;
        private double emLastValue;
        private double emCurrentValue;
        private double billAmount;
        private double billPricePerKw;
        private double billServicesAmount;
        private double billDiff;
        private DateTime billDate;

        public Bill(int billNumber, int emNumber, int cusNumber, int emhNumber, double emLastValue, double emCurrentValue, double billAmount, double billPricePerKw, double billServicesAmount, double billDiff, DateTime billDate)
        {
            this.billNumber = billNumber;
            this.emNumber = emNumber;
            this.cusNumber = cusNumber;
            this.emhNumber = emhNumber;
            this.emLastValue = emLastValue;
            this.emCurrentValue = emCurrentValue;
            this.billAmount = billAmount;
            this.billPricePerKw = billPricePerKw;
            this.billServicesAmount = billServicesAmount;
            this.billDiff = billDiff;
            this.billDate = billDate;
        }

        public int BillNumber { get => billNumber; }
        public int EmNumber { get => emNumber; }
        public int CusNumber { get => cusNumber;}
        public int EmhNumber { get => emhNumber;  }
        public double EmLastValue { get => emLastValue; }
        public double EmCurrentValue { get => emCurrentValue; }
        public double BillAmount { get => billAmount; }
        public double BillPricePerKw { get => billPricePerKw;  }
        public double BillServicesAmount { get => billServicesAmount; }
        public double BillDiff { get => billDiff;}
        public DateTime BillDate { get => billDate;}
    }

    public class BillGenerator
    {
        private readonly DSLib.AccessDB db;

        public BillGenerator(DSLib.AccessDB db)
        {
            this.db = db;
        }

        public bool GenerateBills(int emh, CParam param,IProgress<float> p)
        {
            bool res = false;

            try
            {
                EMHistory e;

                if (new EMHistoryController(this.db).Read(emh, out e))
                {
                    if (!(e.EmhClosed))
                    {
                        List<EMHDetails> ar = new List<EMHDetails>();

                        if (!(new ECM.EMHDetailsController(this.db).Query(emh, ref ar)))
                        {
                            throw new Exception();
                        }

                        for (int i = 0; i < ar.Count; i++)
                        {
                            Bill bill;

                            if (!(CalcBill(ar[i], param, out bill)))
                            {
                                throw new Exception();
                            }

                            if (!(Generate(bill)))
                            {
                                throw new Exception();
                            }

                            p.Report((float)i / (float)ar.Count * (float)100);
                            
                        }

                        if (!(new EMHistoryController(this.db).Close(emh)))
                        {
                            throw new Exception();
                        }

                        res = true;
                    }
                }
            }

            catch (Exception ee){
                Console.Write(ee.Message);
                DeleteByEMHNumber(emh);
                p.Report(0);
            }
            return res;
        }

        public bool Generate(Bill bill)
        {
            bool res = false;

            string sql = string.Format("insert into tblBills " +
                "(" +
                    //  "billNumber" +
                    //  "," +
                    "emNumber" +
                    "," +
                    "cusNumber" +
                    "," +
                    "emhNumber" +
                    "," +
                    "emLastValue" +
                    "," +
                    "emCurrentValue" +
                    "," +
                    "billAmount" +
                    "," +
                    "billPricePerKw" +
                    "," +
                    "billServicesAmount" +
                    "," +
                    "billDiff" +
                    "," +
                    "billDate" +
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
                    "," +
                    "{6}" +
                    "," +
                    "{7}" +
                    "," +
                    "{8}" +
                    "," +
                    "'{9}'" +
                   // "," +
                   // "'{10}'" +
                ")"
                , 
                new object[]
                {
                    bill.EmNumber
                    ,
                    bill.CusNumber
                    ,
                    bill.EmhNumber
                    ,
                    bill.EmLastValue
                    ,
                    bill.EmCurrentValue
                    ,
                    bill.BillAmount
                    ,
                    bill.BillPricePerKw
                    ,
                    bill.BillServicesAmount
                    ,
                    bill.BillDiff
                    ,
                    bill.BillDate
                }
            );
            res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            return res;
        }

        public bool Read(int number,out Bill bill)
        {
            bool res = false;
            bill = null;
            return res;
        }

        public bool Read(DateTime from,DateTime to,ref List<Bill> bills)
        {
            bool res = false;
            return res;
        }

        private bool CalcBill(EMHDetails d, CParam param,out Bill bill)
        {
            bool res = false;
            bill = null;

            try
            {
                bill = new Bill(0, d.EmNumber, d.CusNumber, d.EmhNumber, d.EmhdLastValue, d.EmhdCurrentValue, d.EmhdDiff * param.PricePerKW, param.PricePerKW, param.OtherAmount, d.EmhdDiff, DateTime.Now);
                res = true;
            }
            catch { }

            return res;
        }

        private bool DeleteByEMHNumber(int number)
        {
            bool res = false;
            string sql = $"delete from tblBills where emhNumber={number}";
            res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            return res;
        }

        private bool DeleteByBillNumber(int number)
        {
            bool res = false;
            string sql = $"delete from tblBills where billNumber={number}";
            res = this.db.ExecuteNoneQuery(sql) > 0 ? true : false;
            return res;
        }

    }
}
