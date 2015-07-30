using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.CMS.Section.Models;
using Easy.IOC;
using Easy.Models;

namespace Easy.CMS.Section.Service
{
    interface ISectionContentService
    {
        SectionContent.Types ContentType { get; }
        void AddContent(SectionContent content);
        SectionContent GetContent(int contentId);
        void DeleteContent(int contentId);
    }
}
