﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //            routes.MapRoute("AddContollerRoute", "Home/{action}/{id}/{*catchall}",
            //new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //new[] { "AdditionalControllers" });

            //            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "URLsAndRoutes.Controllers" });

            //            routes.RouteExistingFiles = true;
            //            routes.MapRoute("DiskFile", "Content/StaticContent.html",
            //            new
            //            {
            //                controller = "Account",
            //                action = "LogOn",
            //            },
            //            new
            //            {
            //                customConstraint = new UserAgentConstraint("IE")
            //            });
            //            routes.IgnoreRoute("Content/{filename}.html");
            //            routes.MapRoute("MyNewRoute", "{controller}/{action}");

            //            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //new
            //{
            //    controller = "^H.*",
            //    action = "Index|About",
            //    httpMethod = new HttpMethodConstraint("GET"),
            //    customConstraint = new UserAgentConstraint("IE")
            //},
            //new[] { "URLsAndRoutes.Controllers" });
            //routes.MapRoute("ShopSchema2", "Shop/OldAction",new { controller = "Home", action = "Index" });
            //routes.MapRoute("ShopSchema", "Shop/{action}", new { controller = "Home" });

            ////Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            ////routes.Add("MyRoute", myRoute);
            //routes.MapRoute("MyRoute", "{controller}/{action}", new { controller = "Home", action = "Index" });

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default", // 路由名称
            //    "{controller}/{action}/{id}", // 带有参数的 URL
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            //);

            routes.MapRoute("MyRoute",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}