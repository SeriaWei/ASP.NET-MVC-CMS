using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Easy.Extend;
using Easy.Web.CMS.Layout;

namespace Easy.Web.CMS.Zone
{
    public static class Helper
    {
        public static ZoneCollection GetZones(string[] html, out LayoutHtmlCollection result)
        {
            Regex zoneRegex = new Regex("name=\"(ZoneName|LayoutId|ID)\".+value=\"(.+)\">");
            result = new LayoutHtmlCollection();
            ZoneCollection zones = new ZoneCollection();
            for (int i = 0; i < html.Count(); i++)
            {
                var item = html[i];
                if (item == ZoneEntity.ZoneTag)
                {
                    i++;
                    item = html[i];
                    ZoneEntity zone = new ZoneEntity();
                    item.Split(new[] { "<input" }, StringSplitOptions.RemoveEmptyEntries).Each(part =>
                    {
                        zoneRegex.Replace(part, evaluator =>
                        {
                            if (evaluator.Groups[1].Value.Equals("ZoneName"))
                            {
                                zone.ZoneName = evaluator.Groups[2].Value;
                            }
                            else if (evaluator.Groups[1].Value.Equals("LayoutId"))
                            {
                                zone.LayoutId = evaluator.Groups[2].Value;
                            }
                            else if (evaluator.Groups[1].Value.Equals("ID"))
                            {
                                zone.ID = evaluator.Groups[2].Value;
                            }
                            return "";
                        });
                    });
                    zone.ID = zone.ID ?? Guid.NewGuid().ToString("N");
                    zones.Add(zone);
                    result.Add(new LayoutHtml { Html = ZoneEntity.ZoneTag });
                    result.Add(new LayoutHtml { Html = zone.ID });
                    result.Add(new LayoutHtml { Html = ZoneEntity.ZoneEndTag });
                    i++;
                }
                else
                {
                    result.Add(new LayoutHtml { Html = item });
                }
            }
            return zones;
        }
    }
}
