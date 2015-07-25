using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Models
{
    public class AutoComplete : IEntity
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
