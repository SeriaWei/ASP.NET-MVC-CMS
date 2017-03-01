using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Message.Models
{
    [DataConfigure(typeof(MessageBoxWidgetMetaData))]
    public class MessageBoxWidget : WidgetBase
    {
    }
    class MessageBoxWidgetMetaData : WidgetMetaData<MessageBoxWidget>
    {

    }
}