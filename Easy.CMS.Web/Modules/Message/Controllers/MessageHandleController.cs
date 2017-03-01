using Easy.CMS.Message.Models;
using Easy.CMS.Message.Service;
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
        public MessageHandleController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public ActionResult PostMessage(MessageEntity entity, string redirect)
        {
            if (ModelState.IsValid)
            {
                entity.Status = (int)Constant.RecordStatus.InActive;
                _messageService.Add(entity);
            }
            return Redirect(redirect);
        }

    }
}
