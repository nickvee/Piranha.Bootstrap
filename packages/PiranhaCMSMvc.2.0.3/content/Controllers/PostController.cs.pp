using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Mvc;

namespace $rootnamespace$.Controllers
{
	/// <summary>
	/// The post controller is the standard controller displaying a post
	/// generated from the manager interface.
	/// </summary>
    public class PostController : SinglePostController
    {
		/// <summary>
		/// Gets a standard post.
		/// </summary>
		/// <returns>The view result</returns>
        public ActionResult Index() {
			var model = GetModel() ;

            return View(model.GetView(), model) ;
        }
    }
}
