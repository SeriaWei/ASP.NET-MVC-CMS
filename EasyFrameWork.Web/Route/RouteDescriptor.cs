/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.Route
{
    public class RouteDescriptor
    {
        public RouteDescriptor()
        {

        }
        public RouteDescriptor(string routeName, string url)
        {
            this.Priority = 1;
            this.RouteName = RouteName;
            this.Url = url;
        }
        public RouteDescriptor(string routeName, string url, object defaults)
        {
            this.Priority = 1;
            this.RouteName = RouteName;
            this.Url = url;
            this.Defaults = defaults;
        }
        public RouteDescriptor(string routeName, string url, object defaults,object constraints)
        {
            this.Priority = 1;
            this.RouteName = RouteName;
            this.Url = url;
            this.Defaults = defaults;
            this.Constraints = constraints;
        }
        public RouteDescriptor(string routeName, string url, object defaults, object constraints, string[] namespaces)
        {
            this.Priority = 1;
            this.RouteName = RouteName;
            this.Url = url;
            this.Defaults = defaults;
            this.Constraints = constraints;
            this.Namespaces = namespaces;
        }
        public RouteDescriptor(string routeName, string url, object defaults, object constraints, string[] namespaces, int priority)
        {
            this.Priority = priority;
            this.RouteName = RouteName;
            this.Url = url;
            this.Defaults = defaults;
            this.Constraints = constraints;
            this.Namespaces = namespaces;
        }
        /// <summary>
        /// 优先级，数值越大，优先级越高。
        /// </summary>
        public int Priority { get; set; }
        public string RouteName { get; set; }
        public string Url { get; set; }
        public object Defaults { get; set; }
        public object Constraints { get; set; }
        public string[] Namespaces { get; set; }
    }


}
