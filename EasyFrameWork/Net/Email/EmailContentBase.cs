/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Easy.Net.Email
{
    public abstract class EmailContentBase : IEmailContent
    {
        protected SmtpClient SmtpClient;
        public abstract string GetSubject();

        public abstract string GetBody();

        public virtual bool IsBodyHtml()
        {
            return true;
        }

        public abstract IEnumerable<MailAddress> GetReceivers();

        public virtual IEnumerable<MailAddress> GetCCReceivers()
        {
            return null;
        }

        public virtual IEnumerable<MailAddress> GetBCCReceivers()
        {
            return null;
        }

        public virtual IEnumerable<Attachment> GetAttachments()
        {
            return null;
        }

        public abstract MailAddress GetSender();

        public abstract string GetSmtpHost();

        public abstract NetworkCredential GetCredential();

        public virtual SmtpClient GetSmtpClient()
        {
            return SmtpClient ?? (SmtpClient = new SmtpClient
            {
                Host = GetSmtpHost(),
                Credentials = GetCredential()
            });
        }


        public virtual void OnSendComplete(IEmailContent content)
        {

        }
    }
}
