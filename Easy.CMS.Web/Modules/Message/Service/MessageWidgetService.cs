using Easy.CMS.Message.Models;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Message.Service
{
    public class MessageWidgetService : SimpleWidgetService<MessageWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            return widget.ToWidgetPart(new MessageEntity());
        }
    }
}