/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.IOC;
using Easy.StartTask;

namespace Easy.Web.Filter
{
    public abstract class ConfigureFilterBase : IConfigureFilter
    {
        protected ConfigureFilterBase(IFilterRegister register)
        {
            Registry = register;
        }
        public IFilterRegister Registry { get; set; }
        public abstract void Configure();
        public void Excute()
        {
            Configure();
        }
    }
}