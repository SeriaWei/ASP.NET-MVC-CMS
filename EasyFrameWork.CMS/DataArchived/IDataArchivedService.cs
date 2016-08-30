using Easy.RepositoryPattern;
using System;

namespace Easy.Web.CMS.DataArchived
{
    public interface IDataArchivedService : IService<DataArchived>
    {
        T Get<T>(string key, Func<T> fun);
    }
}