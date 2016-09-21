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
