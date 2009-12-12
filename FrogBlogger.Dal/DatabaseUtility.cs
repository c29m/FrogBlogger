using System.Configuration;
using System.Data.Objects;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Basic utility class for database-related methods
    /// </summary>
    public static class DatabaseUtility
    {
        /// <summary>
        /// Gets the default ObjectContext for the project
        /// </summary>
        /// <returns>The default ObjectContext for the project</returns>
        public static FrogBloggerContainer GetContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FrogBloggerContainer"].ConnectionString;

            return GetContext(connectionString);
        }

        /// <summary>
        /// Gets the default ObjectContext for the project
        /// </summary>
        /// <param name="connectionString">Connection string to use for database queries</param>
        /// <returns>The default ObjectContext for the project</returns>
        public static FrogBloggerContainer GetContext(string connectionString)
        {
            return new FrogBloggerContainer(connectionString);
        }
    }
}
