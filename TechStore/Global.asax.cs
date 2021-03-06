﻿using DataAccess.Abstract;
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
using DataAccess.Repositories;

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

            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IOrderRepository>().To<OrderRepository>();
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>();

           // kernel.Bind<ICategoryRepository>().To<CategoryRepository>().InSingletonScope();
           // kernel.Bind<IProductRepository>().To<ProductRepository>().InSingletonScope();
           // kernel.Bind<IOrderRepository>().To<OrderRepository>().InSingletonScope();
           // kernel.Bind<ICustomerRepository>().To<CustomerRepository>().InSingletonScope();




        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }
    }
}
