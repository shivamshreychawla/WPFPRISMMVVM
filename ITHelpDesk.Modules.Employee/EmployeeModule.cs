using ITHelpDesk.Modules.Employee.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace ITHelpDesk.Modules.Employee
{
    public class EmployeeModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public EmployeeModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
           // var view = this.container.Resolve<EmployeeDetail>();
           // this.regionManager.Regions["EmployeeInfoRegion"].Add(view, "EmployeeDetail");

            this.regionManager.RegisterViewWithRegion("EmployeeInfoRegion",
                                                       () => container.Resolve<EmployeeDetail>());



        }
    }
}
