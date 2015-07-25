using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Easy.Extend;

namespace Easy.Net.Email
{
    public class EmailSender
    {
        public void Send(IEmailContent email)
        {
            MailMessage mailmsg = new MailMessage();
            IEnumerable<MailAddress> rec = email.GetReceivers();
            if (rec == null || !rec.Any()) return;
            rec.Each(mailmsg.To.Add);
            var cc = email.GetCCReceivers();
            if (cc != null)
            {
                cc.Each(mailmsg.CC.Add);
            }
            var bcc = email.GetBCCReceivers();
            if (bcc != null)
            {
                bcc.Each(mailmsg.Bcc.Add);
            }
            var attachments = email.GetAttachments();
            if (attachments != null)
            {
                attachments.Each(mailmsg.Attachments.Add);
            }
            mailmsg.Subject = email.GetSubject();
            mailmsg.Body = email.GetBody();
            mailmsg.BodyEncoding = Encoding.UTF8;
            mailmsg.IsBodyHtml = email.IsBodyHtml();
            mailmsg.From = email.GetSender();
            mailmsg.Priority = MailPriority.Normal;
            var smtp = email.GetSmtpClient();
            smtp.Send(mailmsg);
            mailmsg.Attachments.Each(m => m.Dispose());
            mailmsg.Dispose();
            smtp.Dispose();
            email.OnSendComplete(email);
        }
    }
}
