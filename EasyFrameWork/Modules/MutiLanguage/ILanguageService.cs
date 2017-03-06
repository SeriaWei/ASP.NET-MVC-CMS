/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.RepositoryPattern;
using System.Collections.Generic;
using Easy.IOC;

namespace Easy.Modules.MutiLanguage
{
    public interface ILanguageService : IService<LanguageEntity>, IFreeDependency
    {
        IEnumerable<LanguageEntity> GetAllTypes();
        Dictionary<string, string> InitLan(Dictionary<string, string> source);
    }
}
