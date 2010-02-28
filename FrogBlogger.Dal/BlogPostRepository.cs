﻿using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with BlogPost records
    /// </summary>
    public class BlogPostRepository : DataRepository<BlogPost>, IBlogPostRepository
    {
        /// <summary>
        /// Initializes a new instance of the BlogPostRepository class
        /// </summary>
        public BlogPostRepository()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BlogPostRepository class
        /// </summary>
        /// <param name="context">An abstract IObjectContext object</param>
        public BlogPostRepository(IObjectContext context)
            : base(context)
        {
        }
    }
}
