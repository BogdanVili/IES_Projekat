using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class DayType : IdentifiedObject
    {
        private List<long> seasonDayTypeSchedules = new List<long>();

        public DayType(long globalId) : base(globalId)
        {
        }

        public List<long> SeasonDayTypeSchedules
        {
            get { return seasonDayTypeSchedules; }
            set { seasonDayTypeSchedules = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                DayType x = (DayType)obj;
                return (CompareHelper.CompareLists(x.SeasonDayTypeSchedules, this.SeasonDayTypeSchedules));
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
                case ModelCode.DAYTYPE_SEASONDAYTYPESCHEDULES:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.DAYTYPE_SEASONDAYTYPESCHEDULES:
                    property.SetValue(seasonDayTypeSchedules);
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
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
