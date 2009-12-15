using System;
using System.Collections.Generic;
using FrogBlogger.Dal;

namespace FrogBlogger.Web.Models
{
    /// <summary>
    /// Defines the ViewModel used on the home page
    /// </summary>
    public class HomeViewModel : BlogListBase
    {
        /// <summary>
        /// Gets a reference to a dictionary containing the number of comments for each blog post
        /// </summary>
        public Dictionary<Guid, uint> BlogPostCommentCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total blog post count for the current blog
        /// </summary>
        public uint BlogPostCount
        {
            get;
            private set;
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HomeViewModel class
        /// </summary>
        /// <param name="blogPosts">The blog posts to display on the home page</param>
        /// <param name="blogPostCommentCount">A dictionary containing the comment count for each blog post</param>
        /// <param name="blogPostCount">The total number of blog posts for the current blog</param>
        public HomeViewModel(IList<BlogPost> blogPosts, Dictionary<Guid, uint> blogPostCommentCount, uint blogPostCount)
            : base(blogPosts)
        {
            BlogPostCommentCount = blogPostCommentCount;
            BlogPostCount = blogPostCount;
        }

        #endregion
    }
}