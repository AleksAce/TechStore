using DataAccess.Services;
using Models;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TechStore
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            //override the Ninject stuff so you can Create the kernel by base.OnAppStart
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        private void RegisterServices(IKernel kernel)
        {
            //Bind everything Here
            kernel.Bind<IStoreService<Order>>().To<StoreBaseService<Order>>();
            kernel.Bind<ProductService>().To<ProductService>();
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }
    }
}
