using ITHelpDesk.Module.RequestM.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace ITHelpDesk.Module.RequestM
{
   public class RequestModule: IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;


        public RequestModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            var view = this.container.Resolve<RequestDetail>();
            this.regionManager.Regions["RequestInfoRegion"].Add(view, "RequestDetail");

        }         
    }
}