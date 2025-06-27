using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage
{
    public struct PaidFormat
    {
        public string unitId;
        public string CurrentCode;
        public double Value;

        public PaidFormat(string unitId, string currentCode, double value)
        {
            this.unitId = unitId;
            this.CurrentCode = currentCode;
            this.Value = value;
        }

        public PaidFormat(string unitId, double value)
        {
            this.unitId = unitId;
            this.CurrentCode = "USD";
            this.Value = value;
        }
    }
}
