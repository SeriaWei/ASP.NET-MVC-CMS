using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.Widget
{
    public class WidgetCollection : List<WidgetPart>
    {
        public void TryAdd(WidgetPart part)
        {
            if (!this.Contains(part))
            {
                this.Add(part);
            }
        }
    }
}
