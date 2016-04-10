using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SRM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "AdminLte", action = "Index", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "Login",
                url: "UserAuth/Login/{id}",
                defaults: new {controller = "UserAuth", action = "Login", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "LogOff",
                url: "UserAuth/LogOff/{id}",
                defaults: new {controller = "UserAuth", action = "LogOff", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "Home",
                url: "AdminLte/index/{id}",
                defaults: new {controller = "AdminLte", action = "Index", id = UrlParameter.Optional}
                );

            //Student
            routes.MapRoute(
                name: "RegisterStudent",
                url: "Students/Create/{id}",
                defaults: new {controller = "Students", action = "Create", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "StudentInfo",
                url: "Students/index/{id}",
                defaults: new {controller = "Students", action = "index", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "Class",
                url: "Classes/index/{id}",
                defaults: new {controller = "Classes", action = "index", id = UrlParameter.Optional}
                );
        }
    }
}