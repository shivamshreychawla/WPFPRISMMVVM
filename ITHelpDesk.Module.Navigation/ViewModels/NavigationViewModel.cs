using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Windows.Input;

namespace ITHelpDesk.Module.Navigation.ViewModels
{
    public class NavigationViewModel : NotificationObject
    {
        #region Public Properties
        public ICommand LoadSoftwareCommand { get; set; }
        public ICommand LoadHardwareCommand { get; set; }
        public ICommand RequestCommand { get; set; }

        public string Vlasti
        {
            get { return vlasti; }

            set
            {
                vlasti = value;
                RaisePropertyChanged("Vlasti");
            }
        }

        public IRegionManager RegionManager { get; set; }
        public IUnityContainer UnityContainer { get; set; }
        public IModuleManager ModuleManager { get; set; }
        #endregion


        #region Private Fields
    
        private string vlasti;
        #endregion

        public NavigationViewModel(IRegionManager regionManager, IModuleManager moduleManager, IUnityContainer container)
        {
            this.RegionManager = regionManager;
            this.ModuleManager = moduleManager;
            this.UnityContainer = container;
            LoadSoftwareCommand = new DelegateCommand(loadSoftwareModule);
            LoadHardwareCommand = new DelegateCommand(loadHardwareModule);
            RequestCommand = new DelegateCommand(loadStatusModule);
            Vlasti = "NAVIGATION";
        }
      

        #region Private Methods

        private void loadSoftwareModule()
        {
            //LoadModule method is responsible to load and initialize the module
            //It loads only if module is not initialize already.
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
