using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with UserComment records
    /// </summary>
    public class UserCommentRepository : DataRepository<UserComment>, IUserCommentRepository
    {
        /// <summary>
        /// Initializes a new instance of the UserCommentRepository class
        /// </summary>
        public UserCommentRepository()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserCommentRepository class
        /// </summary>
        /// <param name="context">An abstract IObjectContext object</param>
        public UserCommentRepository(IObjectContext context)
            : base(context)
        {
        }
    }
}
