using System.Web;
using System.Web.Mvc;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Web.ControllerActivator;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Easy.Web.ModelBinder;
using Easy.Web.ValidatorProvider;
using Microsoft.Practices.Unity;

namespace Easy.Web
{
    public abstract class UnityMvcApplication : HttpApplication
    {
        public IUnityContainer Container { get; private set; }
        public abstract void Application_StartUp();
        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            Container = new UnityContainer();
            Container.RegisterType<IApplicationContext, ApplicationContext>();
            Container.RegisterType<IDataDictionaryService, DataDictionaryService>();
            Container.RegisterType<ILanguageService, LanguageService>();

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_StartUp();

            new IOC.UnityRegister(Container).Regist();
        }
    }
}