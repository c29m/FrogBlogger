using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrogBlogger.Web.Helpers;
using System.ServiceModel.Syndication;
using FrogBlogger.Dal;
using System.Xml;

namespace FrogBlogger.Web.Infrastructure
{
    /// <summary>
    /// Extends ActionResult to provide the ability to return a syndicated result
    /// </summary>
    public class FeedActionResult : ActionResult
    {
        #region Properties

        /// <summary>
        /// Specifies the format of the feed
        /// </summary>
        public FeedFormat Format
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a reference to the feed
        /// </summary>
        public SyndicationFeed Feed
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents an instance of the FeedActionResult class from being insantiated
        /// </summary>
        private FeedActionResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the FeedActionResult class
        /// </summary>
        /// <param name="blogName">Name of the blog</param>
        /// <param name="description">Feed description</param>
        /// <param name="format">Format of the feed</param>
        /// <param name="url">A URL Helper</param>
        /// <param name="posts">The posts to include in the feed</param>
        public FeedActionResult(string blogName, string description, FeedFormat format, UrlHelper url, IEnumerable<BlogPost> posts)
        {
            Guid blogPostId;
            string postRelative;
            SyndicationItem item;
            List<SyndicationItem> items = new List<SyndicationItem>();

            // Specify the type of feed
            Format = format;

            // Initialize the current feed
            Feed = new SyndicationFeed(blogName, description, new Uri(url.RouteUrl("Default"), UriKind.Relative));

            //load the posts as items
            foreach (BlogPost post in posts)
            {
                blogPostId = post.BlogPostId;
                postRelative = url.Action(
                    "Details", "Posts",
                    new
                    {
                        year = post.PostedDate.Value.Year,
                        month = post.PostedDate.Value.Month,
                        day = post.PostedDate.Value.Day,
                        id = blogPostId
                    });

                item = new SyndicationItem(post.Title, post.Post,
                    new Uri(postRelative, UriKind.Relative), post.BlogPostId.ToString(), post.PostedDate.Value);

                items.Add(item);
            }

            Feed.Items = items.OrderByDescending(x => x.LastUpdatedTime);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Serializes the results using the specified syndication format provider
        /// </summary>
        /// <param name="context">The context in which the result is executed</param>
        public override void ExecuteResult(ControllerContext context)
        {
            SyndicationFeedFormatter formatter;

            if (Format == FeedFormat.Rss)
            {
                formatter = new Rss20FeedFormatter(Feed);
                context.HttpContext.Response.ContentType = "application/rss+xml";              
            }
            else
            {
                formatter = new Atom10FeedFormatter(Feed);
                context.HttpContext.Response.ContentType = "application/atom+xml";
            }

            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }

        #endregion
    }
}