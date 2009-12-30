using System.Diagnostics.CodeAnalysis;
using FrogBlogger.Dal;

namespace System.Web.Mvc
{
    /// <summary>
    /// Contains helper methods for blog posts
    /// </summary>
    public static class BlogPostHelper
    {
        /// <summary>
        /// Forms a string that indicates the average rating for a post
        /// </summary>
        /// <param name="helper">Extends the HtmlHelper class</param>
        /// <param name="totalRatings">Total number of ratings</param>
        /// <param name="rating">The average user rating</param>
        /// <param name="maxPossible">Maximum possible rating</param>
        /// <returns>A string, which indicates the average rating for a post</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper")]
        public static string AverageRating(this HtmlHelper helper, int totalRatings, int rating, int maxPossible)
        {
            if (totalRatings <= 0)
            {
                return "No ratings...";
            }

            return String.Format("Average user rating - {0}/{1}", rating, maxPossible);
        }

        /// <summary>
        /// Returns an author that has been formatted to include a link to his/her blog, if provided
        /// </summary>
        /// <param name="helper">Extends the HtmlHelper class</param>
        /// <param name="comment">UserComment for which to obtain an author from</param>
        /// <returns>An author that has been formatted to include a link to his/her blog, if provided</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper")]
        public static string FormatAuthor(this HtmlHelper helper, UserComment comment)
        {
            if (!String.IsNullOrEmpty(comment.Url) && Uri.IsWellFormedUriString(comment.Url, UriKind.Absolute))
            {
                Uri uri = new Uri(comment.Url);

                return String.Format("<a href=\"{0}\">{1}</a>", uri.ToString(), comment.Author);
            }

            return comment.Author;
        }
    }
}