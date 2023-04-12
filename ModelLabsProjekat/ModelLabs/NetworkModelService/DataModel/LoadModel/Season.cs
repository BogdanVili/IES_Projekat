using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class Season : IdentifiedObject
    {
        private DateTime endDate;

        private DateTime startDate;

        private List<long> seasonDayTypeSchedules = new List<long>();

        public Season(long globalId) : base(globalId)
        {
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
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
                Season x = (Season)obj;
                return (x.endDate == this.endDate &&
                        x.startDate == this.startDate &&
                        CompareHelper.CompareLists(x.seasonDayTypeSchedules, this.seasonDayTypeSchedules));
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
                case ModelCode.SEASON_ENDDATE:
                case ModelCode.SEASON_STARTDATE:
                case ModelCode.SEASON_SEASONDAYTYPESCHEDULES:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEASON_ENDDATE:
                    property.SetValue(endDate);
                    break;

                case ModelCode.SEASON_STARTDATE:
                    property.SetValue(startDate);
                    break;

                case ModelCode.SEASON_SEASONDAYTYPESCHEDULES:
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
                case ModelCode.SEASON_ENDDATE:
                    endDate = property.AsDateTime();
                    break;

                case ModelCode.SEASON_STARTDATE:
                    startDate = property.AsDateTime();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
