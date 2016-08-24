using Newtonsoft.Json;

namespace Easy.Web.CMS.Chart
{
    public class ChartOption
    {
        public ChartOption()
        {
            Data = new ChartData();
        }
        [JsonProperty("type")]
        public string ChartType { get; set; }
        [JsonProperty("data")]
        public ChartData Data { get; set; }
        [JsonProperty("options")]
        public ChartViewOption ViewOption { get; set; }
    }

    public class ChartType
    {
        public const string Line = "line";
        public const string Bar = "bar";
        public const string Doughnut = "doughnut";
    }
}