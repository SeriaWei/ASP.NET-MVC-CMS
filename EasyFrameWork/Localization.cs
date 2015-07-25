using Easy.Cache;
using Easy.Modules.MutiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy
{
    public static class Localization
    {
        public static string Get(string lanKey)
        {
            var lanCache = new StaticCache();
            LanguageEntity lan = lanCache.Get(lanKey, m =>
            {
                m.When(LanguageService.SignalLanguageUpdate);
                var lanService = ServiceLocator.Current.GetInstance<ILanguageService>();
                if (lanService == null)
                    return new LanguageEntity { LanKey = lanKey, LanValue = lanKey };
                var language = lanService.GetGeneric<LanguageEntity>(lanKey, GetCurrentLanID());
                if (language == null)
                {
                    string lanValue = lanKey;
                    string LanType = "UnKnown";
                    string Module = "Unknown";
                    if (lanKey.Contains("@"))
                    {
                        lanValue = lanKey.Split('@')[1];
                        LanType = "EntityProperty";
                        Module = lanKey.Split('@')[0];
                    }
                    language = new LanguageEntity
                    {
                        LanID = GetCurrentLanID(),
                        LanValue = lanValue,
                        LanKey = lanKey,
                        LanType = LanType,
                        Module = Module
                    };
                    lanService.AddGeneric(language);
                }
                return language;
            });
            return lan.LanValue;
        }
        public static Dictionary<string, string> InitLan(Dictionary<string, string> source)
        {
            var lanService = ServiceLocator.Current.GetInstance<ILanguageService>();

            if (lanService != null)
                return lanService.InitLan(source);

            foreach (string item in source.Keys.ToArray<string>())
            {
                source[item] = item;
            }
            return source;

        }
        public static int GetCurrentLanID()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
        }
    }
}
