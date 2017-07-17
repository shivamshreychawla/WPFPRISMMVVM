using ITHelpDesk.Common.Infrastructure.Repository;
using ITHelpDesk.Common.Infrastructure.Services;
using ITHelpDesk.Module.Navigation;
using ITHelpDesk.Module.Navigation.ViewModels;
using ITHelpDesk.Module.Navigation.Views;
using ITHelpDesk.Module.RequestM;
using ITHelpDesk.Module.RequestM.ViewModels;
using ITHelpDesk.Module.RequestM.Views;
using ITHelpDesk.Modules.Employee;
using ITHelpDesk.Modules.Employee.ViewModels;
using ITHelpDesk.Modules.Employee.Views;
using ITHelpDesk.Modules.Hardware;
using ITHelpDesk.Modules.Hardware.ViewModels;
using ITHelpDesk.Modules.Hardware.Views;
using ITHelpDesk.Modules.Software;
using ITHelpDesk.Modules.Software.ViewModels;
using ITHelpDesk.Modules.Software.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
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
       
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(EmployeeModule));
            moduleCatalog.AddModule(typeof(NavigationModule));
            moduleCatalog.AddModule(typeof(SoftwareModule), InitializationMode.OnDemand);
            moduleCatalog.AddModule(typeof(HardwareModule), InitializationMode.OnDemand);
            moduleCatalog.AddModule(typeof(RequestModule), InitializationMode.OnDemand);
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IHelpDeskRepository, HelpDeskXMLRepository>();
            Container.RegisterType<IEmployeeService, EmployeeService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<RequestViewModel>();
            Container.RegisterType<RequestDetail>();
            base.ConfigureContainer();
        }

    }
}