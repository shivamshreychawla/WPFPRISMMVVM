using ITHelpDesk.Module.Navigation.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace ITHelpDesk.Module.Navigation
{
   public class NavigationModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        public NavigationModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion("NavigationRegion",
                                                       () => this.container.Resolve<NavigationBar>());
            
        }
    }
}