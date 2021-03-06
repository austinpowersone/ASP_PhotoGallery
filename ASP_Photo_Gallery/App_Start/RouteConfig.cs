﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ASP_Photo_Gallery
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Image", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ASP_Photo_Gallery.Controllers" }
            );
        }
    }
}
