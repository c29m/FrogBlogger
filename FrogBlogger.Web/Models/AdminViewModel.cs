using System.Collections.Generic;
using FrogBlogger.Dal;

namespace FrogBlogger.Web.Models
{
    /// <summary>
    /// Defines the ViewModel used on the admin page
    /// </summary>
    public class AdminViewModel : BlogListBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HomeViewModel class
        /// </summary>
        /// <param name="blogPosts">The blog posts to display on the admin page</param>
        public AdminViewModel(IList<BlogPost> blogPosts)
            : base(blogPosts)
        {
        }

        #endregion
    }
}