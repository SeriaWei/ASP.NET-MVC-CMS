using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Modules.SystemSetting;
using Microsoft.Practices.ServiceLocation;

namespace Easy
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public static class SystemSetting
    {
        static SystemSettingService Service;
        static SystemSetting()
        {
            Service = ServiceLocator.Current.GetInstance<SystemSettingService>();
        }
        public static SystemSettingBase Get()
        {
            return Service.Get();
        }
        public static T Get<T>() where T : SystemSettingBase
        {
            return Service.Get() as T;
        }
        public static void Update(SystemSettingBase setting)
        {
            Service.Update(setting);
        }
    }
}
