using System;
using Easy.RepositoryPattern;
using Newtonsoft.Json;

namespace Easy.Web.CMS.DataArchived
{
    public interface IDataArchivedService : IService<DataArchived>
    {
        JsonConverter[] JsonConverters { get; set; }
        T Get<T>(string key, Func<T> fun) where T : class;
    }
}