using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.ValueProvider;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Theme
{
    public class ThemeService : ServiceBase<ThemeEntity>, IThemeService
    {
        private readonly ICookie _cookie;
        private const string PreViewCookieName = "PreViewTheme";
        public ThemeService()
        {
            _cookie = ServiceLocator.Current.GetInstance<ICookie>();
        }

        public void SetPreview(string id)
        {
            _cookie.SetValue(PreViewCookieName, id, true, true);
        }

        public void CancelPreview()
        {
            _cookie.GetValue<string>(PreViewCookieName, true);
        }

        public ThemeEntity GetCurrentTheme()
        {
            var id = _cookie.GetValue<string>(PreViewCookieName);
            ThemeEntity theme = null;
            if (id.IsNotNullAndWhiteSpace() && HttpContext.Current.Request.IsAuthenticated)
            {
                theme = Get(id);
                if (theme != null)
                {
                    theme.IsPreView = true;
                }
            }
            return theme ?? Get(m => m.IsActived).FirstOrDefault();
        }


        public void ChangeTheme(string id)
        {
            if (id.IsNullOrEmpty()) return;

            var theme = Get(id);
            if (theme != null)
            {
                Update(new ThemeEntity { IsActived = false }, new DataFilter(new List<string> { "IsActived" }));
                theme.IsActived = true;
                Update(theme);
            }
        }
    }
}