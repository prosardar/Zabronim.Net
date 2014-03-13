using System.ServiceModel.Activation;
using System.Web.Mvc;
using System.Web.Routing;
using Zabronim.Net.Models;
using Zabronim.Net.Models.Wcf.Contracts;
using Zabronim.Net.ZaEnviroment;

namespace Zabronim.Net {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new AppServiceRoute("RestaurantService", new ServiceHostFactory(), typeof(ErpModule)));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}