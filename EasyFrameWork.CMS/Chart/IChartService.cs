using Easy.IOC;

namespace Easy.Web.CMS.Chart
{
    public interface IChartService : IDependency
    {
        ChartDescriptor Create();
    }
}