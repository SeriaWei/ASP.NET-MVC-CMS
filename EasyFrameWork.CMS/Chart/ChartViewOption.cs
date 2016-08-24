using System.Collections.Generic;
using Newtonsoft.Json;

namespace Easy.Web.CMS.Chart
{
    public class ChartViewOption
    {
        [JsonProperty("scales")]
        public ChartViewOptionScales Scales { get; set; }
    }

    public class ChartViewOptionScales
    {
        [JsonProperty("yAxes")]
        public List<ChartViewOptionAxes> YAxes { get; set; } 
    }

    public class ChartViewOptionAxes
    {
        [JsonProperty("ticks")]
        public ChartViewOptionTicks Tickses { get; set; } 
    }

    public class ChartViewOptionTicks
    {
        [JsonProperty("beginAtZero")]
        public bool BeginAtZero { get; set; }
    }
}