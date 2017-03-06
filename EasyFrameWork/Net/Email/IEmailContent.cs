/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Easy.Net.Email
{
    public interface IEmailContent
    {
        string GetSubject();
        string GetBody();
        bool IsBodyHtml();

        IEnumerable<MailAddress> GetReceivers();
        IEnumerable<MailAddress> GetCCReceivers();
        IEnumerable<MailAddress> GetBCCReceivers();
        IEnumerable<Attachment> GetAttachments();
        MailAddress GetSender();
        string GetSmtpHost();
        NetworkCredential GetCredential();

        SmtpClient GetSmtpClient();

        void OnSendComplete(IEmailContent content);
    }
}
