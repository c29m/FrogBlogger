namespace FrogBlogger.Web.Helpers
{
    /// <summary>
    /// Specifies the type of syndication feed
    /// </summary>
    public enum FeedFormat
    {
        /// <summary>
        /// Atom feed
        /// </summary>
        Atom,

        /// <summary>
        /// RSS 2.0 feed
        /// </summary>
        Rss
    }

    /// <summary>
    /// Contains string constants that represent Roles are used throughout the project
    /// </summary>
    public static class Roles
    {
        /// <summary>
        /// Admin role
        /// </summary>
        public const string Admin = "Admin";
    }
}