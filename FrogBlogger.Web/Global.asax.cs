using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Dal;

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
              "BlogPost",                                               // Route name
              "{year}/{month}/{day}/{id}",                              // URL with parameters
              new { controller = "Post", action = "Details"}            // Parameter defaults
            );

            routes.MapRoute(
              "MultipleBlogPost",                                       // Route name
              "{blog}/{year}/{month}/{day}/{id}",                       // URL with parameters
              new { controller = "Post", action = "Details" }           // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        /// <summary>
        /// The application start event handler
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Can't be static because of the way ASP.NET works")]
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            ObjectFactory.Initialize(x =>
                {
                    x.ForRequestedType<IObjectContext>().TheDefaultIsConcreteType<ObjectContextAdapter>();
                    x.ForRequestedType<IBlogPostRepository>().TheDefaultIsConcreteType<BlogPostRepository>();
                });
        }
    }
}