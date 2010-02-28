using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with Author records
    /// </summary>
    public class AuthorRepository : DataRepository<Author>, IAuthorRepository
    {
        /// <summary>
        /// Initializes a new instance of the AuthorRepository class
        /// </summary>
        public AuthorRepository()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AuthorRepository class
        /// </summary>
        /// <param name="context">An abstract IObjectContext object</param>
        public AuthorRepository(IObjectContext context)
            : base(context)
        {
        }
    }
}
