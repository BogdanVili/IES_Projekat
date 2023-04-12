using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Protection
{
    public class RecloseSequence : IdentifiedObject
    {
        private float recloseDelay;

        private int recloseStep;

        public RecloseSequence(long globalId) : base(globalId)
        {
        }

        public float RecloseDelay
        {
            get { return recloseDelay; }
            set { recloseDelay = value; }
        }

        public int RecloseStep
        {
            get { return recloseStep; }
            set { recloseStep = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
