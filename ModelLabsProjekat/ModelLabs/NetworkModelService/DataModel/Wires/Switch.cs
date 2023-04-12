using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch : ConductingEquipment
    {
        private bool normalOpen;

        private float ratedCurrent;

        private bool retained;

        private int switchOnCount;

        private DateTime switchOnDate;

        public Switch(long globalId) : base(globalId)
        {
        }

        public bool NormalOpen
        {
            get { return normalOpen; }
            set { normalOpen = value; }
        }

        public float RatedCurrent
        {
            get { return ratedCurrent; }
            set { ratedCurrent = value; }
        }

        public bool Retained
        {
            get { return retained; }
            set { retained = value; }
        }

        public int SwitchOnCount
        {
            get { return switchOnCount; }
            set { switchOnCount = value; }
        }

        public DateTime SwitchOnDate
        {
            get { return switchOnDate; }
            set { switchOnDate = value; }
        }
    }
}
