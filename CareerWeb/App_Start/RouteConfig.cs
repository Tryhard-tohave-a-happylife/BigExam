using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CareerWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Tim-kiem-ung-vien",
                url: "SearchCandidate",
                defaults: new { controller = "Employee", action = "SearchCandidate", id = UrlParameter.Optional },
                namespaces : new[]{ "CareerWeb.Controllers"}
            );

            routes.MapRoute(
                name: "Result",
                url: "SearchCandidateResult",
                defaults: new { controller = "Employee", action = "SearchCandidateResult", id = UrlParameter.Optional },
                namespaces: new[] { "CareerWeb.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
