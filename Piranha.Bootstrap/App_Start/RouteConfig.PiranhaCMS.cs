using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Piranha.Bootstrap
{
	public class RouteConfigPiranhaCMS
	{
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new [] { "Piranha.Bootstrap.Controllers" }
			).DataTokens["UseNamespaceFallback"] = false ;
		}
	}
}