using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using Zabronim.Net.Migrations;

namespace Zabronim.Net {
    public class MvcApplication : HttpApplication {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start() {
            Logger.Info("Application Start");

            new DbMigrator(new Configuration()).Update();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}