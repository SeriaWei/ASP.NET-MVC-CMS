using System.Web;
using System.Web.Mvc;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Autofac;
using Easy.Web.ControllerActivator;
using Easy.Web.ModelBinder;
using Easy.Web.ValidatorProvider;

namespace Easy.Web
{
    public abstract class AutofacMvcApplication : HttpApplication
    {
        public ContainerBuilder AutofacContainerBuilder { get; private set; }
        public abstract void Application_StartUp();
        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            AutofacContainerBuilder = new ContainerBuilder();
            AutofacContainerBuilder.RegisterType<ApplicationContext>().As<IApplicationContext>();
            AutofacContainerBuilder.RegisterType<DataDictionaryService>().As<IDataDictionaryService>();
            AutofacContainerBuilder.RegisterType<LanguageService>().As<ILanguageService>();

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_StartUp();
            new IOC.AutofacRegister(AutofacContainerBuilder).Regist(AutofacContainerBuilder.Build());
        }
    }
}