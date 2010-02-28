using System.Diagnostics.CodeAnalysis;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with BlogPostRating records
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "IDisposable is part of the contract and it's implemented correctly in the base class.")]
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
