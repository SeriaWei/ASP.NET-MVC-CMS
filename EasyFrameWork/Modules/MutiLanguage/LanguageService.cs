using Easy.Cache;
using Easy.Data;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.MutiLanguage
{
    public class LanguageService : ServiceBase<LanguageEntity>, ILanguageService
    {
        public const string SignalLanguageUpdate = "SignalLanguageUpdate";
        LanguageRepository rep = new LanguageRepository();
        public IEnumerable<LanguageEntity> GetAllTypes()
        {
            IEnumerable<LanguageEntity> result = rep.GetAllTypes();
            foreach (LanguageEntity item in result)
            {
                item.LanKey = item.Module;
            }
            return result;
        }
        public Dictionary<string, string> InitLan(Dictionary<string, string> source)
        {
            return rep.InitLan(source);
        }

        public override bool Update(LanguageEntity item, DataFilter filter)
        {
            new Signal().Trigger(SignalLanguageUpdate);
            return base.Update(item, filter);
        }

        public override bool Update(LanguageEntity item, params object[] primaryKeys)
        {
            new Signal().Trigger(SignalLanguageUpdate);
            return base.Update(item, primaryKeys);
        }
    }
}
