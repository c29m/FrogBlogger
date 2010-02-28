using System.Diagnostics.CodeAnalysis;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with Keyword records
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "IDisposable is part of the contract and it's implemented correctly in the base class.")]
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
