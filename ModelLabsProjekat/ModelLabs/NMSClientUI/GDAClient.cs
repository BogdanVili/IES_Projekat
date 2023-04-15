using FTN.Common;
using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NMSClientUI
{
    internal class GDAClient : IDisposable
    {
        private ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();

        private NetworkModelGDAProxy gdaQueryProxy = null;
        private NetworkModelGDAProxy GdaQueryProxy
        {
            get
            {
                if (gdaQueryProxy != null)
                {
                    gdaQueryProxy.Abort();
                    gdaQueryProxy = null;
                }

                gdaQueryProxy = new NetworkModelGDAProxy("NetworkModelGDAEndpoint");
                gdaQueryProxy.Open();

                return gdaQueryProxy;
            }
        }

        public GDAClient()
        {
        }

        public List<long> GetAllGID()
        {
            List<long> returnGIDs = new List<long>();

            List<ModelCode> modelCodesClasses = GetConcreteModelCodes();

            int numberOfResources = 2;
            int resourcesLeft = 0;
            int iteratorId = 0;

            foreach (ModelCode modelCodeClass in modelCodesClasses) 
            {
                List<ModelCode> properties = new List<ModelCode>();

                iteratorId = GdaQueryProxy.GetExtentValues(modelCodeClass, properties);
                resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);

                while (resourcesLeft > 0)
                {
                    List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                    for (int i = 0; i < rds.Count; i++)
                    {
                        returnGIDs.Add(rds[i].Id);
                    }

                    resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                }
            }

            GdaQueryProxy.IteratorClose(iteratorId);

            return returnGIDs;
        }

        public List<ModelCode> GetModelCodes(long globalId)
        {
            short type = ModelCodeHelper.ExtractTypeFromGlobalId(globalId);
            return modelResourcesDesc.GetAllPropertyIds((DMSType)type);
        }

        public List<ModelCode> GetModelCodes(ModelCode concrete)
        {
            return modelResourcesDesc.GetAllPropertyIds(concrete);
        }

        public List<ModelCode> GetConcreteModelCodes()
        {
            return new List<ModelCode>() {
                                            ModelCode.LOADBREAKSWITCH,
                                            ModelCode.BREAKER,
                                            ModelCode.SEASON,
                                            ModelCode.DAYTYPE,
                                            ModelCode.SWITCHSCHEDULE,
                                            ModelCode.REGULARTIMEPOINT,
                                            ModelCode.RECLOSESEQUENCE
                                         };
        }

        public string GetValues(long globalId, List<ModelCode> properties)
        {
            string valueString = "";

            short type = ModelCodeHelper.ExtractTypeFromGlobalId(globalId);

            ResourceDescription rd = GdaQueryProxy.GetValues(globalId, properties);

            foreach (Property property in rd.Properties)
            {
                valueString += BuildPropertyString(property);
            }

            return valueString;
        }

        public string GetExtentValues(ModelCode concrete, List<ModelCode> properties)
        {
            string extentValueString = "";
            int numberOfResources = 2;
            int resourcesLeft = 0;
            int iteratorId = 0;

            iteratorId = GdaQueryProxy.GetExtentValues(concrete, properties);
            resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);

            while (resourcesLeft > 0)
            {
                List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                for (int i = 0; i < rds.Count; i++)
                {
                    foreach (Property property in rds[i].Properties)
                    {
                        extentValueString += BuildPropertyString(property);
                    }
                    extentValueString += "\n";
                }

                resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
            }

            GdaQueryProxy.IteratorClose(iteratorId);

            return extentValueString;
        }

        public string BuildPropertyString(Property property)
        {
            string propertyString = "";

            switch (property.Type)
            {
                case PropertyType.Enum:
                    List<string> enums = new EnumDescs().GetEnumList(property.Id);
                    propertyString += property.Id + " : " + enums[property.AsEnum()] + "\n";
                    break;

                case PropertyType.ReferenceVector:
                    bool firstElement = true;

                    foreach (long gidReference in property.AsLongs())
                    {
                        if (firstElement)
                        {
                            propertyString += property.Id + " : " + gidReference;
                            firstElement = false;
                        }
                        else
                        {
                            propertyString += ", " + gidReference;
                        }
                    }

                    propertyString += "\n";
                    break;

                default:
                    propertyString += property.Id + " : " + property.ToString() + "\n";
                    break;
            }

            return propertyString;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
