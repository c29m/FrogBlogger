using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with Keyword records
    /// </summary>
    public class KeywordRepository : DataRepository<Keyword>, IKeywordRepository
    {
        /// <summary>
        /// Initializes a new instance of the KeywordRepository class
        /// </summary>
        public KeywordRepository()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the KeywordRepository class
        /// </summary>
        /// <param name="context">An abstract IObjectContext object</param>
        public KeywordRepository(IObjectContext context)
            : base(context)
        {
        }
    }
}
