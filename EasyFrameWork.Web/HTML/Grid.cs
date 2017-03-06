/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Easy.Constant;
using Easy.Extend;
using Easy.ViewPort.Grid;
using Easy.ViewPort.Descriptor;

namespace Easy.Web.HTML
{
    public class Grid<T> : EasyGrid<T> where T : class
    {
        private readonly ViewContext _viewContex;
        public Grid(ViewContext viewContex)
        {
            this._viewContex = viewContex;
            var controller = this._viewContex.Controller as System.Web.Mvc.Controller;
            base.DataSource(controller.Url.Action("GetList"));
            base.DeleteUrl(controller.Url.Action("Delete"));
            var configAttribute = Easy.MetaData.DataConfigureAttribute.GetAttribute<T>();
            if (configAttribute != null)
            {
                configAttribute.GetViewPortDescriptors(false).Each(m =>
                {
                    if (!this.DropDownOptions.ContainsKey(m.Name) &&
                        m is DropDownListDescriptor &&
                        (m as DropDownListDescriptor).SourceType == SourceType.ViewData &&
                        viewContex.ViewData.ContainsKey((m as DropDownListDescriptor).SourceKey))
                    {
                        var selectList = viewContex.ViewData[(m as DropDownListDescriptor).SourceKey] as SelectList;
                        if (selectList != null)
                        {
                            if (!this.DropDownOptions.ContainsKey(m.Name))
                            {
                                this.DropDownOptions.Add(m.Name, new Dictionary<string, string>());
                            }
                            var options = this.DropDownOptions[m.Name];
                            selectList.Each(n =>
                            {
                                if (!options.ContainsKey(n.Value))
                                {
                                    options.Add(n.Value, n.Text);
                                }
                            });

                        }
                    }

                });
            }
        }
        public override EasyGrid<T> DataSource(string url)
        {
            var controller = this._viewContex.Controller as System.Web.Mvc.Controller;
            if (controller != null)
                return base.DataSource(controller.Url.Content(url));
            return this;
        }

        public EasyGrid<T> DataSource(string action, string controller)
        {
            var contro = this._viewContex.Controller as System.Web.Mvc.Controller;
            if (contro != null)
                return base.DataSource(contro.Url.Action(action, controller));
            return this;
        }
        public EasyGrid<T> DataSource(string action, string controller, object routeValues)
        {
            var contro = this._viewContex.Controller as System.Web.Mvc.Controller;
            if (contro != null)
                return base.DataSource(contro.Url.Action(action, controller, routeValues));
            return this;
        }
        public override EasyGrid<T> DeleteUrl(string url)
        {
            var controller = this._viewContex.Controller as System.Web.Mvc.Controller;
            if (controller != null)
                return base.DeleteUrl(controller.Url.Content(url));
            return this;
        }

        public EasyGrid<T> DeleteUrl(string action, string controller)
        {
            var contro = this._viewContex.Controller as System.Web.Mvc.Controller;
            if (contro != null)
                return base.DeleteUrl(contro.Url.Action(action, controller));
            return this;
        }
        public EasyGrid<T> DeleteUrl(string action, string controller, object routeValues)
        {
            var contro = this._viewContex.Controller as System.Web.Mvc.Controller;
            if (contro != null)
                return base.DeleteUrl(contro.Url.Action(action, controller, routeValues));
            return this;
        }

        public override string ToString()
        {

            using (var writer = new HtmlTextWriter(_viewContex.Writer))
            {
                writer.Write(base.ToString());
            }
            return string.Empty;
        }
        public MvcHtmlString ToHtmlString()
        {
            return new MvcHtmlString(base.ToString());
        }
    }
}
