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
        #region Properties

        /// <summary>
        /// Gets a reference to a dictionary containing the number of comments for each blog post
        /// </summary>
        public Dictionary<Guid, int> BlogPostCommentCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total blog post count for the current blog
        /// </summary>
        public int BlogPostCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a reference to a collection containing the latest tags
        /// </summary>
        public IList<Keyword> LatestTags
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the page number
        /// </summary>
        public int Page
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether or not there are more records to display
        /// </summary>
        public bool HasNext
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether or not there were any records skipped
        /// </summary>
        public bool HasPrevious
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HomeViewModel class
        /// </summary>
        /// <param name="blogPosts">The blog posts to display on the home page</param>
        /// <param name="latestTags">The latest tags</param>
        /// <param name="blogPostCommentCount">A dictionary containing the comment count for each blog post</param>
        /// <param name="blogPostCount">The total number of blog posts for the current blog</param>
        /// <param name="page">The current page</param>
        /// <param name="hasNext">Indicates whether or not there are more records</param>
        /// <param name="hasPrevious">Indicates whether there are any previous records</param>
        public HomeViewModel(IList<BlogPost> blogPosts, IList<Keyword> latestTags, Dictionary<Guid, int> blogPostCommentCount, int blogPostCount, int page, bool hasNext, bool hasPrevious)
            : base(blogPosts)
        {
            BlogPostCommentCount = blogPostCommentCount;
            LatestTags = latestTags;
            BlogPostCount = blogPostCount;
            Page = page;
            HasNext = hasNext;
            HasPrevious = hasPrevious;
        }

        #endregion
    }
}