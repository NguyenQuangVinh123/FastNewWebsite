using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastNews
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Post Detail",
                url: "the-loai/{metaTitleCategory}/tin-chi-tiet/{metaTitlePost}-{postID}",
                defaults: new { controller = "Post", action = "PostDetail", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );

            routes.MapRoute(
                name: "Category Detail",
                url: "the-loai/{metaTitleCategory}/{categoryID}",
                defaults: new { controller = "Category", action = "CategoryDetail", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );

            routes.MapRoute(
                name: "Account Register",
                url: "dang-ky",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );
            routes.MapRoute(
                name: "Account Login",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );
            routes.MapRoute(
                name: "Password Change",
                url: "doi-mat-khau",
                defaults: new { controller = "Account", action = "PasswordChange", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Post", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "FastNews.Controllers" }
            );
            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                    ,
                    namespaces: new[] { "FastNews.Controllers" }
                );
        }
    }
}
