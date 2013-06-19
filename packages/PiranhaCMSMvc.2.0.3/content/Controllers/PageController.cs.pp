using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Mvc;

namespace $rootnamespace$.Controllers
{
	/// <summary>
	/// The page controller is the standard controller displaying a page
	/// generated from the manager interface.
	/// </summary>
    public class PageController : SinglePageController
    {
		/// <summary>
		/// Gets a standard page.
		/// </summary>
		/// <returns>The view result</returns>
        public ActionResult Index() {
			var model = GetModel() ;

            return View(model.GetView(), model) ;
        }
    }
}
