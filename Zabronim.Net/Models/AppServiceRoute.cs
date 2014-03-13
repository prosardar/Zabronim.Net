using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace Zabronim.Net.Models {
    public class AppServiceRoute : ServiceRoute {
        public AppServiceRoute(string routePrefix, ServiceHostFactoryBase serviceHostFactory, Type serviceType)
            : base(routePrefix, serviceHostFactory, serviceType) {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
            return null;
        }
    }
}