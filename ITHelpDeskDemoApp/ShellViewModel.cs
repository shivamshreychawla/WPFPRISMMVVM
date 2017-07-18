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
            var view = requestInfoRegion.GetView(viewName);
            requestInfoRegion.Activate(view);
        }
    }
}
