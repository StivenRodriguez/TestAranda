namespace ProductCatalog.API
{
    using ProductCatalog.Business.Implementations;
    using ProductCatalog.Business.Interfaces;
    using ProductCatalog.Data.Products;
    using System;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Unity;
    using Unity.WebApi;
    using AutoMapper;
    using ProductCatalog.Business.DTOs;
    using ProductCatalog.Data.Models;

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IServiceProvider serviceProvider;
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var container = new Unity.UnityContainer();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IRepositoryProductsCatalog, RepositoryProductsCatalog>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }      

    }

}
