using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoffeShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MainPage",
                url: "{controller}",
                defaults: new { controller = "WebPage", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Login",
               url: "{controller}",
               defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Register",
              url: "{controller}",
              defaults: new { controller = "Register", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "Admin",
               url: "{controller}/{Action}/{id}",
               defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Marketing",
              url: "{controller}",
              defaults: new { controller = "Marketing", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "AboutUs",
             url: "{controller}",
             defaults: new { controller = "AboutUs", action = "Index", id = UrlParameter.Optional }
         );
        }
    }
}
