using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;

namespace Easy.Web
{
    public class DisplayViewSupport
    {
        public static void SupportMobileView()
        {
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("mobile")
            {
                ContextCondition = (context => context.GetOverriddenUserAgent().IndexOf("iPhone", StringComparison.OrdinalIgnoreCase) >= 0 ||
                context.GetOverriddenUserAgent().IndexOf("android", StringComparison.OrdinalIgnoreCase) >= 0)
            });
        }
        public static void SupportIEView()
        {
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("ie")
            {
                ContextCondition = (context => context.GetOverriddenUserAgent().IndexOf("msie", StringComparison.OrdinalIgnoreCase) >= 0)
            });
        }
    }
}
