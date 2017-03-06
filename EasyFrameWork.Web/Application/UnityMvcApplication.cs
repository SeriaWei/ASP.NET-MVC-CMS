/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Web;
using System.Web.Mvc;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Web.ControllerActivator;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Easy.Web.ValidatorProvider;
using Microsoft.Practices.Unity;
using Easy.Extend;
using Easy.IOC;
using Easy.IOC.Unity;
using Easy.Web.ControllerFactory;

namespace Easy.Web.Application
{
    public abstract class UnityMvcApplication : TaskApplication
    {
        private IUnityContainer _container;

        private IContainerAdapter _containerAdapter;

        public override IContainerAdapter ContainerAdapter
        {
            get { return _containerAdapter ?? (_containerAdapter = new UnityContainerAdapter(_container)); }
        }

        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            _container = new UnityContainer();
            _container.RegisterType<IControllerFactory, FilterControllerFactory>();
            _container.RegisterType<IControllerActivator, EasyControllerActivator>();
            _container.RegisterType<IHttpItemsValueProvider, HttpItemsValueProvider>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IApplicationContext, ApplicationContext>(new PerRequestLifetimeManager());

            //Container.RegisterType<IDataDictionaryService, DataDictionaryService>();
            //Container.RegisterType<ILanguageService, LanguageService>(new ContainerControlledLifetimeManager());
           

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_Starting();

            new UnityRegister(_container).Regist();

            TaskManager.ExcuteAll();

            Application_Started();
        }
    }
}