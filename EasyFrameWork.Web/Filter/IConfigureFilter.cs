/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.IOC;
using Easy.StartTask;
namespace Easy.Web.Filter
{
    public interface IConfigureFilter : IStartTask, ISingleInstance, IDependency
    {
        IFilterRegister Registry { get; set; }
        void Configure();
    }
}