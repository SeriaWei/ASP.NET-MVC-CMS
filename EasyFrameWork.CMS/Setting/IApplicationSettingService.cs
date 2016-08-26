using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Setting
{
    public interface IApplicationSettingService : IService<ApplicationSetting>
    {
        string Get(string settingKey, string defaultValue);
    }
}