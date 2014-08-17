using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Article
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        public override void InitScript()
        {
            
        }

        public override void InitStyle()
        {
            Style("Article").Include("~/Modules/Article/Content/Article.css");
        }
    }
}