/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;

namespace Easy.Web.CMS.ExtendField
{
    public interface IExtendField
    {
        IEnumerable<ExtendFieldEntity> ExtendFields { get; set; }
    }
}