/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Cache;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.SystemSetting
{
    public abstract class SystemSettingService
    {
        SystemSettingRepository rep = new SystemSettingRepository();
        StaticCache cache = new StaticCache();
        private const string SignalSystemSettingUpdate = "Signal_SystemSettingUpdate";
        public virtual SystemSettingBase Get()
        {
            return cache.Get("CacheKey_SystemSettingAll", m =>
            {
                m.When(SignalSystemSettingUpdate);
                return rep.Get();
            });
        }
        public virtual void Update(SystemSettingBase setting)
        {
            Signal.Trigger(SignalSystemSettingUpdate);
            rep.Update(setting);
        }
    }
}
