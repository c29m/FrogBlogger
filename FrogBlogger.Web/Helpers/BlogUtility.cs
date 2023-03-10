using System;
using System.Configuration;
using System.Linq;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Web.Helpers
{
    /// <summary>
    /// Contains helper methods for working with blogs
    /// </summary>
    public static class BlogUtility
    {
        #region Properties

        /// <summary>
        /// Gets the name of the current blog
        /// </summary>
        public static string BlogName
        {
            get
            {
                string blogName;
                Guid blogId = GetBlogId();

                using (IDataRepository<Blog> repository = new DataRepository<Blog>())
                {
                    blogName = (from b in repository.Fetch()
                                where b.BlogId == blogId
                                select b).Single().Name;
                }

                return blogName;
            }
        }

        /// <summary>
        /// Gets the name of the default blog
        /// </summary>
        public static string DefaultBlog
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigKeys.DefaultBlog];
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the blog ID for the default blog
        /// </summary>
        /// <returns>The unique identifier for the default blog</returns>
        public static Guid GetBlogId()
        {
            return GetBlogId(DefaultBlog);
        }

        /// <summary>
        /// Gets the blog ID for the requested blog
        /// </summary>
        /// <param name="blogName">Name of the requested blog for which to retrieve the ID for</param>
        /// <returns>The unique identifier for the blog</returns>
        /// <exception cref="InvalidOperationException">If <paramref name="blogName"/> is null or empty</exception>
        public static Guid GetBlogId(string blogName)
        {
            Guid blogId;

            if (String.IsNullOrEmpty(blogName))
            {
                throw new InvalidOperationException("blogName cannot be null or empty");
            }

            using (IDataRepository<Blog> repository = new DataRepository<Blog>())
            {
                blogId = repository.GetSingle(b => b.FriendlyName == blogName).BlogId;
            }

            return blogId;
        }

        /// <summary>
        /// Gets the default page size for a blog
        /// </summary>
        /// <returns>The default page size for a blog</returns>
        public static int GetPageSize()
        {
            return GetPageSize(DefaultBlog);
        }

        /// <summary>
        /// Gets the default page size for a blog
        /// </summary>
        /// <param name="blogName">>Name of the requested blog for which to retrieve the page size for</param>
        /// <returns>The default page size for a blog</returns>
        public static int GetPageSize(string blogName)
        {
            int pageSize;

            if (String.IsNullOrEmpty(blogName))
            {
                throw new InvalidOperationException("blogName cannot be null or empty");
            }

            using (IDataRepository<Blog> repository = new DataRepository<Blog>())
            {
                pageSize = repository.GetSingle(b => b.FriendlyName == blogName).MaxHomePagePosts;
            }

            return pageSize;
        }

        #endregion
    }
}