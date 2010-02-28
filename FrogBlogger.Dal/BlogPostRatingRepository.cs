using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with BlogPostRating records
    /// </summary>
    public class BlogPostRatingRepository : DataRepository<BlogPostRating>, IBlogPostRatingRepository
    {
        /// <summary>
        /// Initializes a new instance of the BlogPostRatingRepository class
        /// </summary>
        public BlogPostRatingRepository()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BlogPostRatingRepository class
        /// </summary>
        /// <param name="context">An abstract IObjectContext object</param>
        public BlogPostRatingRepository(IObjectContext context)
            : base(context)
        {
        }
    }
}
