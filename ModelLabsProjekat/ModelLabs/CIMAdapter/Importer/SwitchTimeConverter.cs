namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class SwitchTimeConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}



		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				SwitchTimeConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}



		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				SwitchTimeConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}
				if (cimEquipment.NormallyInService)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				SwitchTimeConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);
			}
		}



		public static void PopulateSeasonProperties(FTN.Season cimSeason , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
            if ((cimSeason != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateIdentifiedObjectProperties(cimSeason, rd);

				if(cimSeason.EndDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEASON_ENDDATE, cimSeason.EndDate));
				}

				if(cimSeason.StartDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEASON_STARTDATE, cimSeason.StartDate));
				}
            }
        }

        public static void PopulateDayTypeProperties(FTN.DayType cimDayType , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimDayType != null) && (rd != null))
            {
				SwitchTimeConverter.PopulateIdentifiedObjectProperties(cimDayType, rd);
            }
        }

        public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularTimePoint != null) && (rd != null))
            {
				SwitchTimeConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

                if (cimRegularTimePoint.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER, cimRegularTimePoint.SequenceNumber));
                }

                if (cimRegularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE1, cimRegularTimePoint.Value1));
                }

                if (cimRegularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE2, cimRegularTimePoint.Value2));
                }

                if(cimRegularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }
            }
        }

        public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimBasicIntervalSchedule != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTTIME, cimBasicIntervalSchedule.StartTime));
                }

                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT, (short)SwitchTimeConverter.GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
            }
        }

        public static void PopulateRecloseSequenceProperties(FTN.RecloseSequence cimRecloseSequence , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRecloseSequence != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateIdentifiedObjectProperties(cimRecloseSequence, rd);

                if (cimRecloseSequence.RecloseDelayHasValue)
                {
                    //here
                }

                if (cimRecloseSequence.RecloseStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.RECLOSESEQUENCE_RECLOSESTEP, cimRecloseSequence.RecloseStep));
                }

                if(cimRecloseSequence.ProtectedSwitchHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRecloseSequence.ProtectedSwitch.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRecloseSequence.GetType().ToString()).Append(" rdfID = \"").Append(cimRecloseSequence.ID);
                        report.Report.Append("\" - Failed to set reference to ProtectedSwitch: rdfID \"").Append(cimRecloseSequence.ProtectedSwitch.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }    
            }
        }

        public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularIntervalSchedule != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd, importHelper, report);
            }
        }

        public static void PopulateSeasonDayTypeScheduleProperties(FTN.SeasonDayTypeSchedule cimSeasonDayTypeSchedule , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSeasonDayTypeSchedule != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeSchedule, rd, importHelper, report);

                if(cimSeasonDayTypeSchedule.SeasonHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.Season.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Season: rdfID \"").Append(cimSeasonDayTypeSchedule.Season.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }

                if(cimSeasonDayTypeSchedule.DayTypeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.DayType.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to DayType: rdfID \"").Append(cimSeasonDayTypeSchedule.DayType.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }
            }
        }


        public static void PopulateSwitchScheduleProperties(FTN.SwitchSchedule cimSwitchSchedule , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitchSchedule != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateSeasonDayTypeScheduleProperties(cimSwitchSchedule, rd, importHelper, report);

                if (cimSwitchSchedule.SwitchHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSwitchSchedule.Switch.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSwitchSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Switch: rdfID \"").Append(cimSwitchSchedule.Switch.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }
            }
        }

        public static void PopulateSwitchProperties(FTN.Switch cimSwitch , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitch != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateConductingEquipmentProperties(cimSwitch, rd, importHelper, report);

                if (cimSwitch.NormalOpenHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_NORMALOPEN, cimSwitch.NormalOpen));
                }

                if (cimSwitch.RatedCurrentHasValue)
                {
                    //here
                }

                if (cimSwitch.RetainedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
                }

                if (cimSwitch.SwitchOnCountHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONCOUNT, cimSwitch.SwitchOnCount));
                }

                if (cimSwitch.SwitchOnDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONDATE, cimSwitch.SwitchOnDate));
                }
            }
        }

        public static void PopulateProtectedSwitchProperties(FTN.ProtectedSwitch cimProtectedSwitch , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimProtectedSwitch != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateSwitchProperties(cimProtectedSwitch, rd, importHelper, report);

                if (cimProtectedSwitch.BreakingCapacityHasValue)
                {
                    //here
                }
            }
        }

        public static void PopulateLoadBreakSwitchProperties(FTN.LoadBreakSwitch cimLoadBreakSwitch, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimLoadBreakSwitch != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateProtectedSwitchProperties(cimLoadBreakSwitch, rd, importHelper, report);
            }
        }

        public static void PopulateBreakerProperties(FTN.Breaker cimBreaker , ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimBreaker != null) && (rd != null))
            {
                SwitchTimeConverter.PopulateProtectedSwitchProperties(cimBreaker, rd, importHelper, report);

                if (cimBreaker.InTransitTimeHasValue)
                {
                    //here
                }
            }
        }
        #endregion Populate ResourceDescription

        #region Enums convert

		public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
		{
            switch (unitSymbol)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
					return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
					return UnitSymbol.F;
                case FTN.UnitSymbol.g:
					return UnitSymbol.g;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.H:
					return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.m:
					return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
					return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.none:
                    return UnitSymbol.none;
                case FTN.UnitSymbol.ohm:
					return UnitSymbol.ohm;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh:
					return UnitSymbol.VArh;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                default:
                    return UnitSymbol.none;
            }
        }
        #endregion Enums convert

        //public static void PopulateTransformerWindingProperties(FTN.TransformerWinding cimTransformerWinding, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimTransformerWinding != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateConductingEquipmentProperties(cimTransformerWinding, rd, importHelper, report);

        //		if (cimTransformerWinding.ConnectionTypeHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_CONNTYPE, (short)GetDMSWindingConnection(cimTransformerWinding.ConnectionType)));
        //		}
        //		if (cimTransformerWinding.GroundedHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_GROUNDED, cimTransformerWinding.Grounded));
        //		}
        //		if (cimTransformerWinding.RatedSHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDS, cimTransformerWinding.RatedS));
        //		}
        //		if (cimTransformerWinding.RatedUHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDU, cimTransformerWinding.RatedU));
        //		}
        //		if (cimTransformerWinding.WindingTypeHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_WINDTYPE, (short)GetDMSWindingType(cimTransformerWinding.WindingType)));
        //		}
        //		if (cimTransformerWinding.PhaseToGroundVoltageHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE, cimTransformerWinding.PhaseToGroundVoltage));
        //		}
        //		if (cimTransformerWinding.PhaseToPhaseVoltageHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE, cimTransformerWinding.PhaseToPhaseVoltage));
        //		}
        //		if (cimTransformerWinding.PowerTransformerHasValue)
        //		{
        //			long gid = importHelper.GetMappedGID(cimTransformerWinding.PowerTransformer.ID);
        //			if (gid < 0)
        //			{
        //				report.Report.Append("WARNING: Convert ").Append(cimTransformerWinding.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerWinding.ID);
        //				report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimTransformerWinding.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
        //			}
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, gid));
        //		}
        //	}
        //}

        //public static void PopulateWindingTestProperties(FTN.WindingTest cimWindingTest, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimWindingTest != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimWindingTest, rd);

        //		if (cimWindingTest.LeakageImpedanceHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN, cimWindingTest.LeakageImpedance));
        //		}
        //		if (cimWindingTest.LoadLossHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LOADLOSS, cimWindingTest.LoadLoss));
        //		}
        //		if (cimWindingTest.NoLoadLossHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_NOLOADLOSS, cimWindingTest.NoLoadLoss));
        //		}
        //		if (cimWindingTest.PhaseShiftHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_PHASESHIFT, cimWindingTest.PhaseShift));
        //		}
        //		if (cimWindingTest.LeakageImpedance0PercentHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, cimWindingTest.LeakageImpedance0Percent));
        //		}
        //		if (cimWindingTest.LeakageImpedanceMaxPercentHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, cimWindingTest.LeakageImpedanceMaxPercent));
        //		}
        //		if (cimWindingTest.LeakageImpedanceMinPercentHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, cimWindingTest.LeakageImpedanceMinPercent));
        //		}

        //		if (cimWindingTest.From_TransformerWindingHasValue)
        //		{
        //			long gid = importHelper.GetMappedGID(cimWindingTest.From_TransformerWinding.ID);
        //			if (gid < 0)
        //			{
        //				report.Report.Append("WARNING: Convert ").Append(cimWindingTest.GetType().ToString()).Append(" rdfID = \"").Append(cimWindingTest.ID);
        //				report.Report.Append("\" - Failed to set reference to TransformerWinding: rdfID \"").Append(cimWindingTest.From_TransformerWinding.ID).AppendLine(" \" is not mapped to GID!");
        //			}
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_POWERTRWINDING, gid));
        //		}
        //	}
        //}
    }
}
