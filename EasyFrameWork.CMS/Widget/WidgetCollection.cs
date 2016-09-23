/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;

namespace Easy.Web.CMS.Widget
{
    public class WidgetCollection : List<WidgetPart>
    {
        public void TryAdd(WidgetPart part)
        {
            if (!Contains(part))
            {
                Add(part);
            }
        }
    }
}
