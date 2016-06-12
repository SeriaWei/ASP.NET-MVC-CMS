using Easy.CMS.Section.ContentJsonConvert;
using Easy.CMS.Section.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Easy.CMS.Section.ContentJsonConvert
{
    class SectionContentJsonConverter : JsonCreationConverter<SectionContent>
    {
        protected override SectionContent Create(Type objectType, JObject jObject)
        {
            var contentType = jObject["SectionContentType"].Value<int>();
            if (((int)SectionContent.Types.CallToAction).Equals(contentType))
            {
                return new SectionContentCallToAction();
            }
            else if (((int)SectionContent.Types.Image).Equals(contentType))
            {
                return new SectionContentImage();
            }
            else if (((int)SectionContent.Types.Paragraph).Equals(contentType))
            {
                return new SectionContentParagraph();
            }
            else if (((int)SectionContent.Types.Title).Equals(contentType))
            {
                return new SectionContentTitle();
            }
            return new SectionContent();
        }
    }
}
