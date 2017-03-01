using Easy.MetaData;
using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Message.Models
{
    [DataConfigure(typeof(MessageMetaData))]
    public class MessageEntity : EditorEntity
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string PostMessage { get; set; }
        public string Reply { get; set; }
    }
    class MessageMetaData : DataViewMetaData<MessageEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Message");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().Required().MaxLength(50).Order(1);
            ViewConfig(m => m.Email).AsTextBox().Email().Required().MaxLength(50).Order(2);
            ViewConfig(m => m.PostMessage).AsTextArea().Required().MaxLength(1000).Order(3);
            ViewConfig(m => m.Reply).AsTextArea().AddClass("html").Order(4);
        }
    }
}