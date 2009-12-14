using System.Security.Cryptography;
using System.Web.Mvc;

// Thanks to Rick Strahl and Rob Conery for this
namespace FrogBlogger.Web.Helpers
{
    /// <summary>
    /// Contains helper methods for working with gravatar images
    /// </summary>
    public static class GravatarHelper
    {
        /// <summary>
        /// Renders an img tag with a gravatar image as its source
        /// </summary>
        /// <param name="helper">The HtmlHelper class</param>
        /// <param name="email">Email address for which to revrieve gravatar for</param>
        /// <param name="size">Image size</param>
        /// <returns>An img tag with a gravatar image as its source</returns>
        public static string Gravatar(this HtmlHelper helper, string email, int size)
        {
            var result = "<img src=\"{0}\" alt=\"Gravatar\" class=\"gravatar\" />";
            var url = GetGravatarUrl(email, size);

            return string.Format(result, url);
        }

        /// <summary>
        /// Gets the image source for a gravatar img
        /// </summary>
        /// <param name="email">Email address for which to retrieve the gravatar image for</param>
        /// <param name="size">Image size</param>
        /// <returns>The URL (image source) for a gravatar image</returns>
        private static string GetGravatarUrl(string email, int size)
        {
            return (string.Format("http://www.gravatar.com/avatar/{0}?s={1}&r=PG", EncryptMD5(email), size.ToString()));
        }

        /// <summary>
        /// Gets the URL to the requested gravatar, plus the URL to the default image path
        /// </summary>
        /// <param name="email">Email address for which to retrieve the gravatar image for</param>
        /// <param name="size">Image size</param>
        /// <param name="defaultImagePath">The default image</param>
        /// <returns>the URL to the requested gravatar, plus the URL to the default image path</returns>
        private static string GetGravatarUrl(string email, int size, string defaultImagePath)
        {
            return GetGravatarUrl(email, size) + string.Format("&default={0}", defaultImagePath);
        }

        /// <summary>
        /// MD5 encrypts the supplied value
        /// </summary>
        /// <param name="value">Value for which to encrypt</param>
        /// <returns>An MD5 encrypted string</returns>
        private static string EncryptMD5(string value)
        {
            var md5 = new MD5CryptoServiceProvider();
            var valueArray = System.Text.Encoding.ASCII.GetBytes(value);
            var encrypted = "";

            valueArray = md5.ComputeHash(valueArray);

            for (var i = 0; i < valueArray.Length; i++)
                encrypted += valueArray[i].ToString("x2").ToLower();

            return encrypted;
        }
    }
}