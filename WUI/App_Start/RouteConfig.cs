using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "list_course",
                url: "Admin/Course/Index",
                defaults: new { controller = "Race", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Configuration",
                url: "Configuration/{PersonneId}/{action}/{id}",
                defaults: new { controller = "DisplayConfiguration", PersonneId = "1", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}