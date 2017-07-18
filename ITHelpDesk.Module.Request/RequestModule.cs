using ITHelpDesk.Common.Infrastructure;
using ITHelpDesk.Module.Request.ViewModels;
using ITHelpDesk.Module.Request.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace ITHelpDesk.Module.Request
{
   public class RequestModule: IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;


        public RequestModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            container.RegisterType<RequestViewModel>();
            container.RegisterType<RequestDetail>();
        }

        public void Initialize()
        {
            var view = this.container.Resolve<RequestDetail>();
            this.regionManager.Regions[RegionNames.mainRegion].Add(view, nameof(RequestDetail));
            
        }

    }
}