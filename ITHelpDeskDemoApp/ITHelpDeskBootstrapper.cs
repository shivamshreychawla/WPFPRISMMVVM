using ITHelpDesk.Common.Infrastructure.Repository;
using ITHelpDesk.Common.Infrastructure.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace ITHelpDeskDemoApp
{
    public class ITHelpDeskBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
       
        //protected override void ConfigureModuleCatalog()
        //{
        //    ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
        //    moduleCatalog.AddModule(typeof(EmployeeModule));
        //    moduleCatalog.AddModule(typeof(NavigationModule));
        //    moduleCatalog.AddModule(typeof(SoftwareModule), InitializationMode.OnDemand);
        //    moduleCatalog.AddModule(typeof(HardwareModule), InitializationMode.OnDemand);
        //    moduleCatalog.AddModule(typeof(RequestModule), InitializationMode.OnDemand);
        //}

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IHelpDeskRepository, HelpDeskXMLRepository>();
            Container.RegisterType<IEmployeeService, EmployeeService>(new ContainerControlledLifetimeManager());

          
            base.ConfigureContainer();
        }

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
        //       new Uri("ITHelpDeskDemoApp;component/ModuleCatalog.xaml", UriKind.Relative));



        //}

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog =  new ConfigurationModuleCatalog();
            return catalog;
        }



    }
}