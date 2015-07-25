using Easy.Web.Page;
using Easy.Web.Resource.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using Easy.Extend;

namespace Easy.Web.Resource
{
    public abstract class ResourceRegister
    {
        protected WebPageBase _page;
        protected Action<ResourceCollection, string> _callBack;
        protected ResourceRegister(WebPageBase page, Action<ResourceCollection, string> callBack)
        {
            this._page = page;
            _callBack = callBack;
        }

        public abstract IDisposable AtHead();

        public abstract IDisposable AtFoot();

        public abstract ResourceCapture Reqiured(string name);

    }

    public class Capture : IDisposable
    {
        private WebPageBase _page;
        private string _key;
        private ResourcePosition _position;
        private Action<ResourceCollection, string> _callBack;
        public Capture(WebPageBase page, Action<ResourceCollection, string> callBack, ResourcePosition position, string key)
        {
            _key = key;
            this._page = page;
            _position = position;
            _callBack = callBack;
        }
        public void Dispose()
        {
            ResourceEntity resource = new ResourceEntity();
            resource.Position = _position;
            resource.Source = _page.OutputStack.Pop();
            var resources=new ResourceCollection();
            resources.Name = Guid.NewGuid().ToString("N");
            resources.Add(resource);
            _callBack(resources, _key);
        }
    }

    public class ResourceCapture
    {
        private WebPageBase _page;
        private ResourceCollection _resource;
        private Action<ResourceCollection, string> _callBack;
        private string _key;
        public ResourceCapture(WebPageBase page, Action<ResourceCollection, string> callBack, ResourceCollection source, string key)
        {
            _page = page;
            _callBack = callBack;
            _resource = source;
            _key = key;
        }
        public void AtHead()
        {
            var resources = new ResourceCollection();
            resources.Name = _resource.Name;
            _resource.Each(m =>
            {
                ResourceEntity entity = m.ToNew();
                entity.Position = ResourcePosition.Head;
                resources.Add(entity);
            });
            _callBack(resources, _key);
        }
        public void AtFoot()
        {
            var resources = new ResourceCollection();
            resources.Name = _resource.Name;
            _resource.Each(m =>
            {
                ResourceEntity entity = m.ToNew();
                entity.Position = ResourcePosition.Foot;
                resources.Add(entity);
            });
            _callBack(resources, _key);
        }
    }


    public class ScriptRegister : ResourceRegister
    {
        public ScriptRegister(WebPageBase page, Action<ResourceCollection, string> callBack)
            : base(page, callBack)
        {
        }

        public override IDisposable AtHead()
        {
            _page.OutputStack.Push(new StringWriter());
            return new Capture(this._page, _callBack, ResourcePosition.Head, ViewPage.PartScriptKey);
        }

        public override IDisposable AtFoot()
        {
            _page.OutputStack.Push(new StringWriter());
            return new Capture(this._page, _callBack, ResourcePosition.Foot, ViewPage.PartScriptKey);
        }

        public override ResourceCapture Reqiured(string name)
        {
            if (!Resource.ResourceManager.ScriptSource.ContainsKey(name))
                throw new Exception("找不到名称为“{0}”的相关资源".FormatWith(name));
            return new ResourceCapture(_page, _callBack, Resource.ResourceManager.ScriptSource[name], ViewPage.PartScriptKey);
        }


    }

    public class StyleRegister : ResourceRegister
    {
        public StyleRegister(WebPageBase page, Action<ResourceCollection, string> callBack)
            : base(page, callBack)
        {
        }

        public override IDisposable AtHead()
        {
            return new Capture(this._page, _callBack, ResourcePosition.Head, ViewPage.PartStyleKey);
        }

        public override IDisposable AtFoot()
        {
            return new Capture(this._page, _callBack, ResourcePosition.Foot, ViewPage.PartStyleKey);
        }

        public override ResourceCapture Reqiured(string name)
        {
            return new ResourceCapture(_page, _callBack, Resource.ResourceManager.StyleSource[name], ViewPage.PartStyleKey);
        }
    }

}
