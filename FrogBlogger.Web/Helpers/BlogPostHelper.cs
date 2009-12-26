using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        /// <param name="averageRating">The average user rating</param>
        /// <param name="maxPossible">Maximum possible rating</param>
        /// <returns>A string, which indicates the average rating for a post</returns>
        public static string AverageRating(this HtmlHelper helper, int totalRatings, int averageRating, int maxPossible)
        {
            if (totalRatings <= 0)
            {
                return "No ratings...";
            }

            return String.Format("Average user rating - {0}/{1}", averageRating, maxPossible);
        }
    }
}