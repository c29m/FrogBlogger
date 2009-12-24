using System.Collections.Generic;
using FrogBlogger.Dal;
using System.Web.Security;

namespace FrogBlogger.Web.Models
{
    /// <summary>
    /// Defines the ViewModel used on the admin page
    /// </summary>
    public class AdminViewModel : BlogListBase
    {
        #region Properties

        /// <summary>
        /// Gets a reference to a collection containing all authors for the requested blog
        /// </summary>
        public IList<aspnet_Users> Authors
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HomeViewModel class
        /// </summary>
        /// <param name="blogPosts">The blog posts to display on the admin page</param>
        public AdminViewModel(IList<BlogPost> blogPosts, IList<aspnet_Users> authors)
            : base(blogPosts)
        {
            Authors = authors;
        }

        #endregion
    }
}