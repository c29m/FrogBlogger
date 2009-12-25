using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Models;

namespace FrogBlogger.Web.Controllers
{
    /// <summary>
    /// Contains action methods for working with the home view
    /// </summary>
    [HandleError]
    public class HomeController : Controller
    {
        /// <summary>
        /// Stores the unique identifer for the current blog
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "I am using it. FxCop is acting crazy.")]
        private static Guid _blogId = new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C"); // TODO: Place this with the current BlogId

        /// <summary>
        /// Gets the home page
        /// </summary>
        /// <returns>The home page</returns>
        public ActionResult Index()
        {
            int count;
            int maxRecords = 10; // TODO: Replace the magic number 10 on this line with a user determined value
            HomeViewModel model;
            List<BlogPost> posts;
            Dictionary<Guid, int> blogPostCommentCount = new Dictionary<Guid, int>();

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                posts = (from m in repository.Fetch()
                         where m.BlogId == _blogId
                         orderby m.PostedDate descending
                         select m).Take(maxRecords).ToList();

                // Get the total blog post count
                count = (from m in repository.Fetch()
                         where m.BlogId == _blogId
                         select m).Count();

                // Determine the comment count for each blog post
                foreach (BlogPost post in posts)
                {
                    blogPostCommentCount.Add(post.BlogPostId, post.UserComments.Count);
                }

                model = new HomeViewModel(posts, blogPostCommentCount, count);
            }

            return View(model);
        }

        /// <summary>
        /// Gets the About page
        /// </summary>
        /// <returns>The About page</returns>
        public ActionResult About()
        {
            return View();
        }
    }
}
