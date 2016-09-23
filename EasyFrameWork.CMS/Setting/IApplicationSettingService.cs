/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Setting
{
    public interface IApplicationSettingService : IService<ApplicationSetting>
    {
        string Get(string settingKey, string defaultValue);
    }
}