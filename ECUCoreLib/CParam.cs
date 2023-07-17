using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECUCoreLib
{
    public class CParam
    {
        private readonly double pricePerKW;
        private readonly double otherAmount;
        private readonly ushort dayPerCycle;

        public CParam(double pricePerKW, double otherAmount, ushort dayPerCycle)
        {
            //this.pricePerKW = pricePerKW;
            //this.otherAmount = otherAmount;
            //this.dayPerCycle = dayPerCycle;

            this.pricePerKW = 250;
            this.otherAmount = 0;
            this.dayPerCycle = 7;
        }

        public double PricePerKW => pricePerKW;

        public double OtherAmount => otherAmount;

        public ushort DayPerCycle => dayPerCycle;
    }
}
