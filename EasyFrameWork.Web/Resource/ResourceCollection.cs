/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Web.Resource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.Resource
{
    public class ResourceCollection : List<ResourceEntity>
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public ResourcePosition Position { get; set; }
    }
}
