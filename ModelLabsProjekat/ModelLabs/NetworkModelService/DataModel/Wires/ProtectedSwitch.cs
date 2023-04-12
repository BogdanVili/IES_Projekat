using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ProtectedSwitch : Switch
    {
        private float breakingCapacity;

        private List<long> recloseSequences = new List<long>();

        public ProtectedSwitch(long globalId) : base(globalId)
        {
        }

        public float BreakingCapacity
        {
            get { return breakingCapacity; }
            set { breakingCapacity = value; }
        }

        public List<long> RecloseSequences
        {
            get { return recloseSequences; }
            set { recloseSequences = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ProtectedSwitch x = (ProtectedSwitch)obj;
                return (x.breakingCapacity == this.breakingCapacity &&
                        CompareHelper.CompareLists(x.recloseSequences, this.recloseSequences));
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

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.PROTECTEDSWITCH_BREAKINGCAPACITY:
                case ModelCode.PROTECTEDSWITCH_RECLOSESEQUENCES:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PROTECTEDSWITCH_BREAKINGCAPACITY:
                    property.SetValue(breakingCapacity);
                    break;

                case ModelCode.PROTECTEDSWITCH_RECLOSESEQUENCES:
                    property.SetValue(recloseSequences);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PROTECTEDSWITCH_BREAKINGCAPACITY:
                    breakingCapacity = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

    }
}
