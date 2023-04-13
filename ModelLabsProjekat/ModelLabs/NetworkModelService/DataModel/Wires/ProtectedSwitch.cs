using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
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

        #region IReference implementation
        public override bool IsReferenced
        {
            get
            {
                return recloseSequences.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (recloseSequences != null && recloseSequences.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.PROTECTEDSWITCH_RECLOSESEQUENCES] = recloseSequences.GetRange(0, recloseSequences.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.RECLOSESEQUENCE_PROTECTEDSWITCH:
                    recloseSequences.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.RECLOSESEQUENCE_PROTECTEDSWITCH:

                    if (recloseSequences.Contains(globalId))
                    {
                        recloseSequences.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }


        #endregion IReference implementation
    }
}
