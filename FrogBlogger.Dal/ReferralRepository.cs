using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Repository for working with Referral records
    /// </summary>
    public class ReferralRepository : DataRepository<Referral>, IReferralRepository
    {
        /// <summary>
        /// Initializes a new instance of the ReferralRepository class
        /// </summary>
        public ReferralRepository()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReferralRepository class
        /// </summary>
        /// <param name="context">An abstract IObjectContext object</param>
        public ReferralRepository(IObjectContext context)
            : base(context)
        {
        }
    }
}
