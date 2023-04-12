using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;

        private UnitSymbol value1Unit;

        public BasicIntervalSchedule(long globalId) : base(globalId)
        {
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public UnitSymbol Value1Unit
        {
            get { return value1Unit; }
            set { value1Unit = value; }
        }

        public override bool Equals(object obj)
        {
            
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
