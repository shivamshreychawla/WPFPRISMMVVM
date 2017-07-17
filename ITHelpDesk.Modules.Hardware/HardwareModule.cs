using ITHelpDesk.Modules.Hardware.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Modules.Hardware
{
  public class HardwareModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;


        public HardwareModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            var view = this.container.Resolve<HardwareDetail>();
            this.regionManager.Regions["RequestInfoRegion"].Add(view, "HardwareDetail");
        }
    }
}