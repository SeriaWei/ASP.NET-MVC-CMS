/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.Web.Resource;
using Easy.Web.Resource.Enums;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Microsoft.Practices.ServiceLocation;
using Easy.Security;

namespace Easy.Web.Page
{
    public abstract class ViewPage<TModel> : WebViewPage<TModel>
    {
        public const string PartScriptKey = "ViewDataKey_PartScript";
        public const string PartStyleKey = "ViewDataKey_PartStyle";

        public IHtmlString ScriptAtHead()
        {
            return GetResource(PartScriptKey, ResourcePosition.Head);
        }

        public IHtmlString ScriptAtFoot()
        {
            return GetResource(PartScriptKey, ResourcePosition.Foot);
        }

        public IHtmlString StyleAtHead()
        {
            return GetResource(PartStyleKey, ResourcePosition.Head);
        }

        public IHtmlString StyleAtFoot()
        {
            return GetResource(PartStyleKey, ResourcePosition.Foot);
        }

        private IHtmlString GetResource(string key, ResourcePosition position)
        {
            var writer = new HtmlStringWriter();
            switch (key)
            {
                case PartScriptKey:
                    ResourceManager.ScriptSource.Where(m => m.Value.Required && m.Value.Position == position)
                        .Each(m => m.Value.Each(r => writer.WriteLine(r.ToSource(this, this.Context)))); break;
                case PartStyleKey:
                    ResourceManager.StyleSource.Where(m => m.Value.Required && m.Value.Position == position)
                        .Each(m => m.Value.Each(r => writer.WriteLine(r.ToSource(this, this.Context)))); break;
            }
            if (TempData.ContainsKey(key))
            {
                var source = TempData[key] as Dictionary<string, ResourceCollection>;
                if (source != null)
                {
                    source.Each(m => m.Value.Where(n => n.Position == position).Each(l => writer.WriteLine(l.ToSource(this, this.Context))));
                }
            }
            return writer;
        }

        private ScriptRegister _script;
        public ScriptRegister Script
        {
            get
            {
                return _script ?? (_script = new ScriptRegister(this, appendResourceAction));
            }
        }

        private StyleRegister _style;
        public StyleRegister Style
        {
            get
            {
                return _style ?? (_style = new StyleRegister(this, appendResourceAction));
            }
        }
        private void appendResourceAction(ResourceCollection resource, string key)
        {
            Dictionary<string, ResourceCollection> source = null;
            if (TempData.ContainsKey(key))
            {
                source = TempData[key] as Dictionary<string, ResourceCollection>;
            }
            else
            {
                source = new Dictionary<string, ResourceCollection>();
            }
            if (!source.ContainsKey(resource.Name))
            {
                source.Add(resource.Name, resource);
                TempData[key] = source;
            }
        }

        private IApplicationContext _applicationContext;
        public IApplicationContext ApplicationContext
        {
            get
            {
                return _applicationContext ?? (_applicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>());
            }
        }

        private IAuthorizer _authorizer;
        public IAuthorizer Authorizer
        {
            get
            {
                return _authorizer ?? (_authorizer = ServiceLocator.Current.GetInstance<IAuthorizer>());
            }
        }
    }

    public abstract class ViewPage : ViewPage<dynamic>
    {
    }

}
