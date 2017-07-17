using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Windows.Input;

namespace ITHelpDesk.Module.Navigation.ViewModels
{
    public class NavigationViewModel //: INavigationViewModel
    {
        #region Private Fields

        private DelegateCommand loadSoftwareCommand;
        private DelegateCommand loadHardwareCommand;
        private DelegateCommand requestCommand;
        #endregion
        public NavigationViewModel(IRegionManager regionManager, IModuleManager moduleManager, IUnityContainer container)
        {
            this.RegionManager = regionManager;
            this.ModuleManager = moduleManager;
            this.UnityContainer = container;
            loadSoftwareCommand = new DelegateCommand(loadSoftwareModule, canLoadSoftwareModule);
            loadHardwareCommand = new DelegateCommand(loadHardwareModule, canLoadHardwareModule);
            requestCommand = new DelegateCommand(loadStatusModule, canLoadStatusModule);
        }
        #region Public Properties
        public IRegionManager RegionManager { get; set; }
        public IUnityContainer UnityContainer { get; set; }
        public IModuleManager ModuleManager { get; set; }
        #endregion

        #region Command
        public ICommand LoadSoftwareModuleCommand
        {
            get
            {
                return loadSoftwareCommand;
            }
        }
        public ICommand LoadHardwareModuleCommand
        {
            get
            {
                return loadHardwareCommand;
            }
        }
        public ICommand RequestModuleCommand
        {
            get
            {
                return requestCommand;
            }
        }
        #endregion

        #region Private Methods
       
       
        private bool canLoadHardwareModule()
        {
            return true;
        }
        private bool canLoadSoftwareModule()
        {
            return true;
        }
        private bool canLoadStatusModule()
        {
            return true;
        }

        private void loadSoftwareModule()
        {
            // LoadModule method is responsible to load and initialize the module
            // It loads only if module is not initialize already.
            ModuleManager.LoadModule("SoftwareModule");
            var requestInfoRegion = RegionManager.Regions["RequestInfoRegion"];
            var newView = requestInfoRegion.GetView("SoftwareDetail");
            // As RequestInfoRegion uses ContentControlRegionAdapter so at a time only one view will be activated.
            requestInfoRegion.Activate(newView);
        }
        private void loadStatusModule()
        {
            ModuleManager.LoadModule("RequestModule");
            var requestInfoRegion = RegionManager.Regions["RequestInfoRegion"];
            var newView = requestInfoRegion.GetView("RequestDetail");
            requestInfoRegion.Activate(newView);
        }
        private void loadHardwareModule()
        {
            ModuleManager.LoadModule("HardwareModule");
            var requestInfoRegion = RegionManager.Regions["RequestInfoRegion"];
            var newView = requestInfoRegion.GetView("HardwareDetail");
            requestInfoRegion.Activate(newView);
        }

        #endregion
    }
}
