using Easy.CMS.Message.Models;
using Easy.CMS.Message.Service;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Message.Controllers
{
    [AdminTheme]
    public class MessageController : BasicController<MessageEntity, int, IMessageService>
    {
        public MessageController(IMessageService service) : base(service)
        {
        }
    }
}
