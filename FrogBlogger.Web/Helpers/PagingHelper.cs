using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// Contsains static helper methods for paging
    /// </summary>
    public static class PagingHelper
    {
        /// <summary>
        /// Helper method for returning HTML pagination
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">The current page</param>
        /// <param name="currentUrl">The current URL</param>
        /// <param name="pageQueryStringParameter">The query string parameter used to indicate the page number</param>
        /// <param name="hasNext">Indicates whether or not there are more records</param>
        /// <param name="hasPrevious">Indicates whether there are any previous records</param>
        /// <returns>The HTML for a pagination</returns>
        public static string Pager(this HtmlHelper helper, Uri currentUrl, string pageQueryStringParameter, int page, bool hasNext, bool hasPrevious)
        {
            int previousPage = page - 1;
            int nextpage = page + 1;
            StringBuilder html = new StringBuilder();
            string initialHtml = "<div id=\"pagination\"{0}</div>";

            if (hasPrevious)
            {
                html.AppendFormat("<a href=\"{0}?{1}={2}\">Previous</a>{3}", currentUrl.ToString(), pageQueryStringParameter, previousPage, hasNext ? " - " : String.Empty);
            }

            if (hasNext)
            {
                html.AppendFormat("<a href=\"{0}?{1}={2}\">Next</a>", currentUrl.ToString(), pageQueryStringParameter, nextpage);
            }

            return String.Format(initialHtml, html.ToString());
        }
    }
}