using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Easy.HTML.Tab
{
    public class EasyTab
    {
        public EasyTab(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }
        public List<string> tabs = new List<string>();
        public List<string> tabContents = new List<string>();
        public EasyTab Add(string tabName, string tabContent)
        {
            tabs.Add(tabName);
            tabContents.Add(tabContent);
            return this;
        }
        public string Complete()
        {
            StringBuilder lis = new StringBuilder();
            foreach (var item in tabs)
            {
                lis.AppendFormat("<li><button class='TabBtn'>{0}</button></li>", item);
            }
            StringBuilder topBuilder = new StringBuilder();
            topBuilder.AppendFormat("<div class='Tabtitle'><ul class='TopMenu'>{0}</ul><div style='clear:both'></div></div>", lis);
            StringBuilder tabCon = new StringBuilder();
            foreach (var item in tabContents)
            {
                if (tabCon.Length == 0)
                {
                    tabCon.AppendFormat("<div class='Contents Active'>{0}</div>", item);
                }
                else
                {
                    tabCon.AppendFormat("<div class='Contents'>{0}</div>", item);
                }
            }
            StringBuilder downBuilder = new StringBuilder();
            downBuilder.AppendFormat("<div class='TabContent'>{0}</div>", tabCon.ToString());
            string script = "<script type='text/javascript'>var tab_{0}= Easy.Tab('#{0}', '.TopMenu', '.TabContent')</script>";
            script = string.Format(script, this.Name);
            return string.Format("<div id='{0}'>{1}{2}</div>{3}", this.Name, topBuilder.ToString(), downBuilder.ToString(), script);
        }
    }
}
