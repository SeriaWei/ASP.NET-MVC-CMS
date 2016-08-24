using System.Collections.Generic;
using System.Linq;
using Easy.Extend;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Chart
{
    public class ChartProviderService : IChartProviderService
    {
        private IEnumerable<IChartService> _chartServices;

        public IEnumerable<ChartDescriptor> GetAvailableChart()
        {
            _chartServices = ServiceLocator.Current.GetAllInstances<IChartService>();
            if (_chartServices == null || !_chartServices.Any()) return new List<ChartDescriptor>();
            return _chartServices.Select(chartService => chartService.Create());
        }
    }
}