using ITHelpDesk.Common.Infrastructure;
using ITHelpDesk.Modules.Software.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace ITHelpDesk.Modules.Software
{
    public class SoftwareModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;


        public SoftwareModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            var view = this.container.Resolve<SoftwareDetail>();
            this.regionManager.Regions[RegionNames.mainRegion].Add(view, nameof(SoftwareDetail));


        }
    }
}