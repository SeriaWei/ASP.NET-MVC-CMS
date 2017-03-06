/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Web.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Easy.Web.MetadataProvider
{
    public class EasyModelMetaDataProvider : ModelMetadataProvider
    {
        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            List<EasyModelMetaData> result = new List<EasyModelMetaData>();
            foreach (var item in containerType.GetProperties())
            {
                EasyModelMetaData emMetaData = new EasyModelMetaData(this, containerType, null, item.PropertyType, item.Name);
                result.Add(emMetaData);
            }
            return result;
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            EasyModelMetaData emMetaData = new EasyModelMetaData(this, containerType, modelAccessor, containerType.GetProperty(propertyName).PropertyType, propertyName);
            return emMetaData;
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            EasyModelMetaData emMetaData = new EasyModelMetaData(this, null, modelAccessor, modelType, null);
            return emMetaData;
        }
    }
}
