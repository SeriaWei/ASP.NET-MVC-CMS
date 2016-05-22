using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.Widget
{
    public class WidgetPart
    {
        public WidgetBase Widget { get; set; }
        public object ViewModel { get; set; }

        public override bool Equals(object obj)
        {
            var target = obj as WidgetPart;
            if (target != null && target.Widget != null && this.Widget != null)
            {
                return target.Widget.ID == this.Widget.ID;
            }
            return false;
        }
        public override int GetHashCode()
        {
            if (Widget != null)
            {
                return Widget.ID.GetHashCode();
            }
            return base.GetHashCode();
        }
    }
}
