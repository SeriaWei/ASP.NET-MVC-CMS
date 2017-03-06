/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Constant
{


    public enum SourceType
    {
        Dictionary,
        ViewData
    }
    public enum RecordStatus
    {
        Active = 1,
        InActive = 2
    }
    [Serializable]
    public enum ActionType
    {
        Create = 1,
        Update = 2,
        Delete = 3,
        Design = 4,
        Publish = 5,
        Unattached = 6,
        Continue = 7
    }
}
