using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.jsTree
{
    public class Contextmenu : List<ContextmenuItem>
    {
       
    }
    public class ContextmenuItem
    {
        public bool SeparatorBefore { get; set; }
        public bool SeparatorAfter { get; set; }
        public bool Disabled { get; set; }
        public string Label { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public int Shortcut { get; set; }
        public string ShortcutLabel { get; set; }
    }
}
