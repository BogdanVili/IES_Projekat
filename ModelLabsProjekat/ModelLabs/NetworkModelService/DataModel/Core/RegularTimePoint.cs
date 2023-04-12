using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularTimePoint : IdentifiedObject
    {
        private int sequenceNumber;

        private float value1;

        private float value2;

        public float Value2
        {
            get { return value2; }
            set { value2 = value; }
        }


        public RegularTimePoint(long globalId) : base(globalId)
        {
        }

        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        public float Value1
        {
            get { return value1; }
            set { value1 = value; }
        }
    }
}
