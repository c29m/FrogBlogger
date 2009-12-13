using System.Collections.Generic;
using FrogBlogger.Dal;

namespace FrogBlogger.Web.Models
{
    /// <summary>
    /// Base class for any page that works with blog posts
    /// </summary>
    public class BlogListBase
    {
        #region Properties

        /// <summary>
        /// Gets a reference to a list of BlogPosts
        /// </summary>
        public IList<BlogPost> BlogPosts
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BlogListBase class
        /// </summary>
        /// <param name="blogPosts">The blog posts to display on the page</param>
        public BlogListBase(IList<BlogPost> blogPosts)
        {
            BlogPosts = blogPosts;
        }

        #endregion
    }
}