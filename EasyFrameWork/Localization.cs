/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
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
        private static readonly ILanguageService LanService;

        static Localization()
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                LanService = ServiceLocator.Current.GetInstance<ILanguageService>();
            }
        }

        public static bool IsMultiLanReady()
        {
            return LanService != null;
        }
        public static string Get(string lanKey)
        {
            var lanCache = new StaticCache();
            LanguageEntity lan = lanCache.Get(lanKey, m =>
            {
                m.When(LanguageService.SignalLanguageUpdate);

                if (LanService == null)
                    return new LanguageEntity { LanKey = lanKey, LanValue = lanKey };
                var language = LanService.Get(lanKey, GetCurrentLanID());
                if (language == null)
                {
                    string lanValue = lanKey;
                    string lanType = "UnKnown";
                    string module = "Unknown";
                    if (lanKey.Contains("@"))
                    {
                        lanValue = lanKey.Split('@')[1];
                        lanType = "EntityProperty";
                        module = lanKey.Split('@')[0];
                    }
                    language = new LanguageEntity
                    {
                        LanID = GetCurrentLanID(),
                        LanValue = lanValue,
                        LanKey = lanKey,
                        LanType = lanType,
                        Module = module
                    };
                    LanService.Add(language);
                }
                return language;
            });
            return lan.LanValue;
        }
        public static Dictionary<string, string> InitLan(Dictionary<string, string> source)
        {
            if (LanService != null)
                return LanService.InitLan(source);

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
