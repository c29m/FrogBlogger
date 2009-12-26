using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using FrogBlogger.Dal;

namespace FrogBlogger.Test.Helpers
{
    [TestClass]
    public class BlogPostHelperTests
    {
        /// <summary>
        /// Verifies that the AverageRating helper method indicates that there were no ratings, if no ratings have been supplied
        /// </summary>
        [TestMethod]
        public void NoAverageRatings()
        {
            string result = BlogPostHelper.AverageRating(null, 0, 0, 5);

            Assert.IsTrue(result.Contains("No ratings"));
        }

        /// <summary>
        /// Verifies that the AverageRating method returns an average rating when supplied one
        /// </summary>
        [TestMethod]
        public void AverageRating()
        {
            string result = BlogPostHelper.AverageRating(null, 10, 5, 5);

            Assert.IsTrue(result.Contains("Average user rating - 5/5"));
        }

        /// <summary>
        /// Verifies that the FormatAuthor method does not return a link if no URL was supplied
        /// </summary>
        [TestMethod]
        public void FormatAuthorWithNoUrl()
        {
            string result;
            UserComment comment = new UserComment
            {
                Author = "FrogBlogger"
            };

            // Get the formatted author
            result = BlogPostHelper.FormatAuthor(null, comment);

            // No URL was provided, so result should not include a link
            Assert.AreEqual<string>(comment.Author, result);
        }

        /// <summary>
        /// Verifies that the FormatAuthor method returns a link if a URL was supplied
        /// </summary>
        [TestMethod]
        public void FormatAuthoWithUrl()
        {
            string url = "http://www.google.com/";
            string result;
            UserComment comment = new UserComment
            {
                Author = "FrogBlogger",
                Url = url
            };

            // Get the formatted author
            result = BlogPostHelper.FormatAuthor(null, comment);

            // No URL was provided, so result should not include a link
            Assert.AreEqual<string>(String.Format("<a href=\"{0}\">{1}</a>", comment.Url, comment.Author), result);
        }
    }
}
