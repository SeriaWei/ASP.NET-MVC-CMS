/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.IOC;

namespace Easy.Models
{
    [Serializable]
    public class AutoComplete : IEntity
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
