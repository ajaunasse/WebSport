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
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );
            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "list_course",
                url: "Admin/Course/Index",
                defaults: new { controller = "Race", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "resultat",
                url: "Admin/Course/Index",
                defaults: new { controller = "Race", action = "Importresult", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "add_course",
                url: "Admin/Course/Create",
                defaults: new { controller = "Race", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "edit_course",
                url: "Admin/Course/Edit/{id}",
                defaults: new { controller = "Race", action = "Edit", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "statistiques",
                url: "Admin/Stats/Index",
                defaults: new { controller = "Statistique", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "list_personne",
                url: "Admin/Personne/Index",
                defaults: new { controller = "Personne", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Configuration",
                url: "Configuration/{PersonneId}/{action}/{id}",
                defaults: new { controller = "DisplayConfiguration", PersonneId = "1", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "deconnect",
                url: "Logout",
                defaults: new { controller = "Register", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}