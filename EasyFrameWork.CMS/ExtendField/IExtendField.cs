using System.Collections.Generic;

namespace Easy.Web.CMS.ExtendField
{
    public interface IExtendField
    {
        IEnumerable<ExtendFieldEntity> ExtendFields { get; set; }
    }
}