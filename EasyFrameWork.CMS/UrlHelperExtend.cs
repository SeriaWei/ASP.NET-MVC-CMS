using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.Web.CMS
{
    public static class UrlHelperExtend
    {
        public static string PathContent(this UrlHelper helper, string path)
        {
            return helper.Content(path.IsNullOrEmpty() ? "~/" : path);
        }

        public static string ValidateCode(this UrlHelper helper)
        {
            return helper.Action("Code", "Validation", new { module = "Validation" });
        }

    }
}
