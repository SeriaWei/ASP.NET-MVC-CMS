using Easy.CMS.Message.Models;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Message.Service
{
    public class MessageService : ServiceBase<MessageEntity>, IMessageService
    {
    }
}