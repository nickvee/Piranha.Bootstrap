using System.Linq;
using Piranha.Models;
using Post = Piranha.Entities.Post;

namespace System.Web.Mvc
{
    public static class PiranhaHelpers
    {
        //Will return the css class "active" and the "permalink" in lowercase format (for css purposes), used on the main navigatio menu.
        public static MvcHtmlString GetActiveClass(this Sitemap page)
        {
            var currentPage = (Page) HttpContext.Current.Items["Piranha_CurrentPage"];
            
            if (currentPage != null)
            {
                if (currentPage.Id == page.Id || page.Pages.Any(x => x.Id == currentPage.Id))
                {
                    return MvcHtmlString.Create(string.Format("{0} {1}", "active", page.Permalink.ToLower()));
                }
            }

            return MvcHtmlString.Create(page.Permalink.ToLower());
        }

        //Will return the css class "active" for the current post(for css purposes), used on the sidebar.
        public static MvcHtmlString GetActiveClass(this Post post)
        {
            var currentPage = (Piranha.Models.Post) HttpContext.Current.Items["Piranha_CurrentPost"];

            if (currentPage != null)
            {
                if (currentPage.Id == post.Id)
                {
                    return MvcHtmlString.Create("active");
                }
            }

            return null;
        }

       //Replaces the StartPage url with a "/" and if a menu has no permalink will set to a "#"
        public static MvcHtmlString Permalink(this Sitemap page)
        {
            return page.IsStartpage
                       ? MvcHtmlString.Create("/")
                       : MvcHtmlString.Create(string.IsNullOrEmpty(page.Permalink) ? "#" : page.Permalink);
        }
    }
}