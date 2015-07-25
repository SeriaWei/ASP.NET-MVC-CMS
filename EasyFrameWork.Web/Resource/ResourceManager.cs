using Easy.Web.Resource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.Resource
{
    public abstract class ResourceManager
    {
        public static Dictionary<string, ResourceCollection> ScriptSource { get; private set; }
        public static Dictionary<string, ResourceCollection> StyleSource { get; private set; }
        static ResourceManager()
        {
            ScriptSource = new Dictionary<string, ResourceCollection>();
            StyleSource = new Dictionary<string, ResourceCollection>();
        }
        protected ResourceHelper Script(string name)
        {
            return new ResourceHelper(name, ResourceType.Script);
        }
        protected ResourceHelper Style(string name)
        {
            return new ResourceHelper(name, ResourceType.Style);
        }

        public abstract void InitScript();
        public abstract void InitStyle();
    }

}
