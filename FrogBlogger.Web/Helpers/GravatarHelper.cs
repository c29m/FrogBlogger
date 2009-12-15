using System.Security.Cryptography;
using System.Web.Mvc;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

// Thanks to Rick Strahl and Rob Conery for this
namespace System.Web.Mvc
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
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper", Justification = "This is an extension method.")]
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
        /// MD5 encrypts the supplied value
        /// </summary>
        /// <param name="value">Value for which to encrypt</param>
        /// <returns>An MD5 encrypted string</returns>
        private static string EncryptMD5(string value)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] valueArray = System.Text.Encoding.ASCII.GetBytes(value);
            string encrypted = "";

            valueArray = md5.ComputeHash(valueArray);

            for (int i = 0; i < valueArray.Length; i++)
                encrypted += valueArray[i].ToString("x2", CultureInfo.InvariantCulture).ToLower(CultureInfo.CurrentCulture);

            return encrypted;
        }
    }
}