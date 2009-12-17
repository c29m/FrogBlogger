using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;

namespace FrogBlogger.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "blog_post",                                              // Route name
              "{year}/{month}/{day}/{id}",                              // URL with parameters
              new { controller = "Posts", action = "Details"}           // Parameter defaults
            );

            routes.MapRoute(
              "multipleblog_post",                                      // Route name
              "{blog}/{year}/{month}/{day}/{id}",                       // URL with parameters
              new { controller = "Posts", action = "Details" }          // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        /// <summary>
        /// The application start event
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to be static because of the way ASP.NET works")]
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}