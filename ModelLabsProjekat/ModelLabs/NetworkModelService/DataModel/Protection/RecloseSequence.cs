using FTN.Common;
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

        private long protectedSwitch = 0;

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

        public long ProtectedSwitch
        {
            get { return protectedSwitch; }
            set { protectedSwitch = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RecloseSequence x = (RecloseSequence)obj;
                return (x.recloseDelay == this.recloseDelay &&
                        x.recloseStep == this.recloseStep &&
                        x.protectedSwitch == this.protectedSwitch);
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
                case ModelCode.RECLOSESEQUENCE_RECLOSEDELAY:
                case ModelCode.RECLOSESEQUENCE_RECLOSESTEP:
                case ModelCode.RECLOSESEQUENCE_PROTECTEDSWITCH:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.RECLOSESEQUENCE_RECLOSEDELAY:
                    property.SetValue(recloseDelay);
                    break;

                case ModelCode.RECLOSESEQUENCE_RECLOSESTEP:
                    property.SetValue(recloseStep);
                    break;

                case ModelCode.RECLOSESEQUENCE_PROTECTEDSWITCH:
                    property.SetValue(protectedSwitch);
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
                case ModelCode.RECLOSESEQUENCE_RECLOSEDELAY:
                    recloseDelay = property.AsFloat();
                    break;

                case ModelCode.RECLOSESEQUENCE_RECLOSESTEP:
                    recloseStep = property.AsInt();
                    break;

                case ModelCode.RECLOSESEQUENCE_PROTECTEDSWITCH:
                    protectedSwitch = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

    }
}
