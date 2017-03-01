using Easy.CMS.Message.Models;
using Easy.CMS.Message.Service;
using Easy.Web.ValueProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Message.Controllers
{
    public class MessageHandleController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ICookie _cookie;
        public MessageHandleController(IMessageService messageService, ICookie cookie)
        {
            _messageService = messageService;
            _cookie = cookie;
        }

        public ActionResult PostMessage(MessageEntity entity, string redirect)
        {
            if (ModelState.IsValid)
            {
                _cookie.SetValue("Message", "true", 1);
                entity.Status = (int)Constant.RecordStatus.InActive;
                _messageService.Add(entity);
            }
            return Redirect(redirect);
        }

    }
}
