using System.Collections.Generic;
using FrogBlogger.Dal;

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

        /// <summary>
        /// Gets a reference to the current blog
        /// </summary>
        public Blog CurrentBlog
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
        /// <param name="authors">A list of authors that are associated with the requested blog</param>
        /// <param name="currentBlog">The curent blog</param>
        public AdminViewModel(IList<BlogPost> blogPosts, IList<aspnet_Users> authors, Blog currentBlog)
            : base(blogPosts)
        {
            Authors = authors;
            CurrentBlog = currentBlog;
        }

        #endregion
    }
}