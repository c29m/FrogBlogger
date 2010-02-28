using System.Diagnostics.CodeAnalysis;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with Blog records
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "IDisposable is part of the contract and it's implemented correctly in the base class.")]
    public class BlogRepository : DataRepository<Blog>, IBlogRepository
    {
        /// <summary>
        /// Initializes a new instance of the BlogRepository class
        /// </summary>
        public BlogRepository()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BlogRepository class
        /// </summary>
        /// <param name="context">An abstract IObjectContext object</param>
        public BlogRepository(IObjectContext context)
            : base(context)
        {
        }
    }
}
