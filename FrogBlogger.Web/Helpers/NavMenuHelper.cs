using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Helpers;

namespace System.Web.Mvc
{
    /// <summary>
    /// Contains helper methods for working with the main navigation menu
    /// </summary>
    public static class NavMenuHelper
    {
        /// <summary>
        /// Renders an admin menu item on the navbar if the user is an admin
        /// </summary>
        /// <param name="helper">Extends the HtmlHelper class</param>
        /// <param name="user">The current user</param>
        /// <returns>Renders a list item to add to the menu bar for an admin, if the user is a member
        /// of the admin role. Otherwise, an emptyh string is returned.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "This is an extension method.")]
        public static string AdminMenuItem(this HtmlHelper helper, IPrincipal user)
        {
            string menuItem = string.Empty;

            if (user.IsInRole(FrogBlogger.Web.Helpers.Roles.Admin))
            {
                Guid blogId = BlogUtility.GetBlogId();

                using (IDataRepository<Author> repository = new DataRepository<Author>())
                {
                    if (repository.Fetch(a => a.BlogId == blogId).Any())
                    {
                        menuItem = "<li><a href=\"/Admin/\">Admin</a></li>";
                    }
                }
            }

            return menuItem;
        }
    }
}