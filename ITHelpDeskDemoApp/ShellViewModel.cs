using ITHelpDesk.Common.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace ITHelpDeskDemoApp
{
    public class MyModules
    {
        public string ModuleDisplay { get; set; }
        public string ModuleName { get; set; }
    }
    public class ShellViewModel : NotificationObject
    {
        public IRegionManager RegionManager { get; set; }
        public IUnityContainer UnityContainer { get; set; }
        public IModuleManager ModuleManager { get; set; }
        public List<MyModules> Modules { get; set; }
        public ShellViewModel(IRegionManager regionManager, IModuleManager moduleManager, IUnityContainer container)
        {
            CreateModulesList();

            this.RegionManager = regionManager;
            this.ModuleManager = moduleManager;
            this.UnityContainer = container;
            LoadSoftwareCommand = new DelegateCommand<string>(LoadTabs);
        }
        public void CreateModulesList()
        {
            var modules = ConfigurationManager.AppSettings["Modules"].Split(',');
            Modules = new List<MyModules>();
            foreach (var module in modules)
            {
                Modules.Add(new MyModules() { ModuleDisplay = module.Split(':')[0], ModuleName = module.Split(':')[1] });
            }
        }
        public ICommand LoadSoftwareCommand { get; set; }

        private void LoadTabs(string ModuleName)
        {
            var requestInfoRegion = RegionManager.Regions[RegionNames.mainRegion];
            string viewName = ModuleName.Substring(0, ModuleName.IndexOf("Module")) + "Detail";
            ModuleManager.LoadModule(ModuleName);
            //var view = requestInfoRegion.GetView(viewName);
            //requestInfoRegion.Activate(view);
            //requestInfoRegion.RequestNavigate("View");
           // var p = new NavigationParameters();
            RegionManager.RequestNavigate(RegionNames.mainRegion, viewName);

            //Crazy reflection time

            //    foreach(Assembly asse in AppDomain.CurrentDomain.GetAssemblies())
            //    {
            //        if (asse.GetName().Name.Contains(ModuleName.Substring(0, ModuleName.IndexOf("Module"))))
            //        {
            //            foreach(var x in asse.GetTypes())
            //            {
            //                var attributes = x.GetCustomAttributes(typeof(ShivamModuleAttribute), true);

            //                if (attributes.Length > 0)
            //                {
            //                    //we found the attribute, yay
            //                    foreach (var m in x.GetMethods())
            //                    { 
            //                        var mattributes = m.GetCustomAttributes(typeof(InitializationAttribute), true);

            //                        if (mattributes.Length > 0)
            //                        {
            //                            m.Invoke()
            //                        }
            //                    }
            //                }
            //        }

            //    }
            //}
        }
    }
}
