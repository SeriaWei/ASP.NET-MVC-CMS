namespace Easy.Web.CMS.Chart
{
    public class ChartDescriptor
    {
        public ChartDescriptor()
        {
            Column = 6;
        }
        public int Order { get; set; }
        public string Title { get; set; }
        public bool IsFullRow { get; set; }
        public int Column { get; set; }
        public ChartOption Option { get; set; }
    }
}