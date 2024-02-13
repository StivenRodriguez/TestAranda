using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ProductCatalog.API
{
    public static class UnityConfig
    {
        
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}