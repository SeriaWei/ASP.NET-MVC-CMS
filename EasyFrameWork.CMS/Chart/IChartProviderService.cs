using System.Collections.Generic;
using Easy.IOC;

namespace Easy.Web.CMS.Chart
{
    public interface IChartProviderService : IDependency
    {
        IEnumerable<ChartDescriptor> GetAvailableChart();
    }
}