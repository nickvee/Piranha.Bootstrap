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

        #region Edit Link

        public static MvcHtmlString EditLink(this PostModel postModel, string cssClass = "", string before = "<p>",
                                                 string after = "</p>", string linkText = "Edit")
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return
                    MvcHtmlString.Create(RenderEditLink(postModel.Post.Id, LinkType.Post, cssClass, before, after,
                                                        linkText));
            }

            return null;
        }

        public static MvcHtmlString EditLink(this PageModel pageModel, string cssClass = "", string before = "<p>",
                                                 string after = "</p>", string linkText = "Edit")
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return
                    MvcHtmlString.Create(RenderEditLink(pageModel.Page.Id, LinkType.Page, cssClass, before, after,
                                                        linkText));
            }
            return null;
        }

        public static MvcHtmlString EditLink(this Piranha.Entities.Page pageModel, string cssClass = "",
                                                 string before = "<p>", string after = "</p>", string linkText = "Edit")
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return
                    MvcHtmlString.Create(RenderEditLink(pageModel.Id, LinkType.Page, cssClass, before, after, linkText));
            }
            return null;
        }

        public static MvcHtmlString EditLink(this Post postModel, string cssClass = "", string before = "<p>",
                                                 string after = "</p>", string linkText = "Edit")
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return
                    MvcHtmlString.Create(RenderEditLink(postModel.Id, LinkType.Post, cssClass, before, after, linkText));
            }
            return null;
        }


        private static string RenderEditLink(Guid id, LinkType type, string cssClass, string before, string after,
                                             string linkText)
        {
            //Generate the url
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("edit", type.ToString().ToLower(), new {area = "Manager", id});

            //check to see if the css class was included otherwise don't render the class=""
            string css = string.Empty;

            if (!String.IsNullOrEmpty(cssClass))
            {
                css = String.Format("class=\"{0}\"", cssClass);
            }

            //Return the html
            return String.Format("{0}<a {1} href='{2}'>{3}</a>{4}", before, css, url, linkText, after);
        }

        private static string SiteUrl()
        {
            Uri uri = HttpContext.Current.Request.Url;
            return uri.GetLeftPart(UriPartial.Authority);
        }

        public enum LinkType
        {
            Page = 0,
            Post = 1
        }

        #endregion
    }
}