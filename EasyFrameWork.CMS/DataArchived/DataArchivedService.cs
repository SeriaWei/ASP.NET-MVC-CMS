/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

        public T Get<T>(string key, Func<T> fun) where T : class
        {
            var archived = Get(key);
            T result = null;
            if (archived != null && archived.Data.IsNotNullAndWhiteSpace())
            {
                result = Deserialize<T>(archived.Data);
            }
            if (result == null)
            {
                result = fun();
                Add(new DataArchived { ID = key, Data = Serialize(result) });
            }
            return result;
        }

        private string Serialize(object obj)
        {
            var serializable = obj.GetType().GetCustomAttributes(typeof(SerializableAttribute), false);
            if (serializable.Any())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(ms, obj);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            return JsonConvert.SerializeObject(obj);
        }

        private T Deserialize<T>(string data) where T : class
        {
            var serializable = typeof(T).GetCustomAttributes(typeof(SerializableAttribute), false);
            if (serializable.Any())
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(data)))
                    {
                        return (T)new BinaryFormatter().Deserialize(ms);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return null;
                }
            }
            return JsonConvert.DeserializeObject<T>(data, JsonConverters);
        }
    }
}