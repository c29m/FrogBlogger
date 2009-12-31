using System.Data.Objects;
using System.Web.Mvc;

namespace FrogBlogger.Web.Infrastructure
{
    /// <summary>
    /// The base class for controllers in FrogBlogger
    /// </summary>
    public class FrogBloggerControllerBase : Controller
    {
        #region Fields

        /// <summary>
        /// The object context from which to query
        /// </summary>
        protected ObjectContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes a new instance of the FrogBloggerControllerBase class
        /// </summary>
        protected FrogBloggerControllerBase()
        {
        }

        /// <summary>
        /// Intializes a new instance of the FrogBloggerControllerBase class
        /// </summary>
        /// <param name="context">The context from which to query</param>
        protected FrogBloggerControllerBase(ObjectContext context)
        {
            _context = context;
        }

        #endregion
    }
}