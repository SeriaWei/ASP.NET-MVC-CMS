/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Autofac;
using Easy.Extend;
using Easy.Web.ControllerActivator;
using Easy.Web.ValidatorProvider;
using System;
using Easy.IOC;
using Easy.IOC.Autofac;
using Easy.IOC.Unity;
using Easy.Web.ControllerFactory;
using Easy.Web.Filter;

namespace Easy.Web.Application
{
    public abstract class AutofacMvcApplication : TaskApplication
    {
        private static ILifetimeScopeProvider _lifetimeScopeProvider;

        public override void Init()
        {
            base.Init();
            BeginRequest += AutofacMvcApplication_BeginRequest;
            EndRequest += AutofacMvcApplication_EndRequest;
        }

        void AutofacMvcApplication_BeginRequest(object sender, EventArgs e)
        {
            _lifetimeScopeProvider.BeginLifetimeScope();
        }

        void AutofacMvcApplication_EndRequest(object sender, EventArgs e)
        {
            _lifetimeScopeProvider.EndLifetimeScope();
        }

        private ContainerBuilder _autofacContainerBuilder;

        private IContainerAdapter _containerAdapter;
        public override IContainerAdapter ContainerAdapter
        {
            get { return _containerAdapter ?? (_containerAdapter = new AutofacContainerAdapter(_autofacContainerBuilder)); }
        }


        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            _autofacContainerBuilder = new ContainerBuilder();
            _autofacContainerBuilder.RegisterType<FilterControllerFactory>().As<IControllerFactory>();
            _autofacContainerBuilder.RegisterType<EasyControllerActivator>().As<IControllerActivator>();
            _autofacContainerBuilder.RegisterType<HttpItemsValueProvider>().As<IHttpItemsValueProvider>().SingleInstance();
            //AutofacContainerBuilder.RegisterType<ApplicationContext>().As<IApplicationContext>().InstancePerLifetimeScope();

            _autofacContainerBuilder.RegisterType<RequestLifetimeScopeProvider>().As<ILifetimeScopeProvider>().SingleInstance();
            //AutofacContainerBuilder.RegisterType<DataDictionaryService>().As<IDataDictionaryService>();
            //AutofacContainerBuilder.RegisterType<LanguageService>().As<ILanguageService>().SingleInstance();


           

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_Starting();

            _lifetimeScopeProvider = new AutofacRegister(_autofacContainerBuilder,typeof(System.Web.Mvc.Controller)).Regist(_autofacContainerBuilder.Build());

            TaskManager.ExcuteAll();

            Application_Started();
        }
    }
}