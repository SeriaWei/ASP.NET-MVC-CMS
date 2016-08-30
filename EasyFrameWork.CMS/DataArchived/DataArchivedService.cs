using System;
using Easy.Extend;
using Easy.RepositoryPattern;
using Newtonsoft.Json;

namespace Easy.Web.CMS.DataArchived
{
    public class DataArchivedService : ServiceBase<DataArchived>, IDataArchivedService
    {
        static string ArchiveLock = "ArchiveLock";

        public T Get<T>(string key, Func<T> fun)
        {
            var archived = Get(key);
            T result;
            if (archived == null || archived.Data.IsNullOrWhiteSpace())
            {
                result = fun();
                lock (ArchiveLock)
                {
                    Delete(key);
                    Add(new DataArchived { ID = key, Data = JsonConvert.SerializeObject(result) });
                }
                
            }
            else
            {
                result = JsonConvert.DeserializeObject<T>(archived.Data);
            }
            return result;
        }
    }
}