using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easy.Web.Page
{
    public interface IOnPageFinished
    {
        void Finish(WebViewPage page);
    }
}
