using System;
using Easy.Extend;
using Easy.RepositoryPattern;
using Newtonsoft.Json;

namespace Easy.Web.CMS.DataArchived
{
    public class DataArchivedService : ServiceBase<DataArchived>, IDataArchivedService
    {
        private const string ArchiveLock = "ArchiveLock";
        public JsonConverter[] JsonConverters { get; set; }
        public override void Add(DataArchived item)
        {
            lock (ArchiveLock)
            {
                Delete(item.ID);
                base.Add(item);
            }

        }

        public T Get<T>(string key, Func<T> fun)
        {
            var archived = Get(key);
            T result;
            if (archived == null || archived.Data.IsNullOrWhiteSpace())
            {
                result = fun();
                Add(new DataArchived { ID = key, Data = JsonConvert.SerializeObject(result) });
            }
            else
            {
                result = JsonConvert.DeserializeObject<T>(archived.Data, JsonConverters);
            }
            return result;
        }
    }
}