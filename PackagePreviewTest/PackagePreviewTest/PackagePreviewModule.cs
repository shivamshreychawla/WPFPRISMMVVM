using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagePreviewTest
{
    public class PackagePreviewModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;


        public PackagePreviewModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }


        public void Initialize()
        {
            //var view = this.container.Resolve<SoftwareDetail>();
            //this.regionManager.Regions[RegionNames.mainRegion].Add(view, "SoftwareDetail");
            container.RegisterType<object, MainWindow>("PackagePreviewDetail");
        }     
    }
}