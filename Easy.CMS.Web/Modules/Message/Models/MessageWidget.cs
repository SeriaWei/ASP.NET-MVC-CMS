using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Message.Models
{
    [DataConfigure(typeof(MessageWidgetMetaData))]
    public class MessageWidget : WidgetBase
    {
    }
    class MessageWidgetMetaData : WidgetMetaData<MessageWidget>
    {

    }
}