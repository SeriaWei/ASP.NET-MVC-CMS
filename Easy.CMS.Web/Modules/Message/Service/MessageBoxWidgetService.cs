using Easy.CMS.Message.Models;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Message.Service
{
    public class MessageBoxWidgetService : SimpleWidgetService<MessageBoxWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            return widget.ToWidgetPart(ServiceLocator.Current.GetInstance<IMessageService>().Get(m => m.Status == (int)Constant.RecordStatus.Active));
        }
    }
}