using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Helpers;
using FrogBlogger.Web.Infrastructure;
using FrogBlogger.Web.Models;
using StructureMap;

namespace FrogBlogger.Web.Controllers
{
    /// <summary>
    /// Contains methods for working with blog posts
    /// </summary>
    public class PostController : Controller
    {
        #region Fields

        /// <summary>
        /// The context used for database transactions
        /// </summary>
        private IObjectContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PostController class
        /// </summary>
        public PostController()
        {
            _context = new ObjectContextAdapter();
        }

        /// <summary>
        /// Initializes a new instance of the PostController class
        /// </summary>
        /// <param name="context">The context used for database transactions</param>
        public PostController(IObjectContext context)
        {
            _context = context;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// GET: /Post/Details/{id}
        /// </summary>
        /// <returns>The requested blog post</returns>
        public ActionResult Details(Guid id)
        {
            int totalRatings;
            int averageRating = 0;
            ViewPostViewModel model;
            BlogPost post;
            IList<UserComment> comments;

            using (IBlogPostRepository repository = ObjectFactory.With(_context).GetInstance<IBlogPostRepository>())
            {
                post = repository.GetSingle(x => x.BlogPostId == id);
                comments = post.UserComments.OrderBy(c => c.PostedDate).ToList();

                if (post.BlogPostRatings.Count > 0)
                {
                    averageRating = (int)post.BlogPostRatings.Average(m => m.Rating);
                    totalRatings = post.BlogPostRatings.Count;
                }
                else
                {
                    totalRatings = 0;
                }

                model = new ViewPostViewModel(post, comments, averageRating, totalRatings);
            }

            return View(model);
        }

        /// <summary>
        /// GET: /Post/Feed
        /// </summary>
        /// <returns>The feed action result</returns>
        public ActionResult Feed()
        {
            List<BlogPost> posts;
            Guid blogId = BlogUtility.GetBlogId();

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                posts = (from p in repository.Fetch()
                        where p.BlogId == blogId
                        select p).Take(10).ToList(); // TODO: Remove the magic number 10 to a user configurable value
            }

            return new FeedActionResult(BlogUtility.BlogName, String.Format("{0} feed", BlogUtility.BlogName), FeedFormat.Atom, Url, posts);
        }

        /// <summary>
        /// Leaves a comment on a blog post
        /// </summary>
        /// <param name="userComment">The user comment</param>
        /// <returns>Redirects back to the blog post</returns>
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(UserComment userComment)
        {
            userComment.UserCommentId = Guid.NewGuid();
            userComment.PostedDate = DateTime.Now;

            using (IDataRepository<UserComment> repository = new DataRepository<UserComment>())
            {
                repository.Create(userComment);
                repository.SaveChanges();
            }

            return RedirectToAction("Details", new { id = userComment.BlogPostId });
        }

        /// <summary>
        /// POST: Rate a blog post
        /// </summary>
        /// <param name="id">Unique identifier of the blog post for which to rate</param>
        /// <param name="rating">User rating of a blog post</param>
        /// <returns>A JSON result containing a status message</returns>
        public JsonResult Rate(Guid id, int rating)
        {
            AjaxResponseStatus status = new AjaxResponseStatus();
            BlogPostRating postRating = new BlogPostRating
                {
                    BlogPostId = id,
                    IpAddress = Request.ServerVariables["REMOTE_ADDR"],
                    Rating = rating
                };

            using (IDataRepository<BlogPostRating> repository = new DataRepository<BlogPostRating>())
            {
                if (!repository.Fetch().Any(r => r.BlogPostId == id && r.IpAddress == postRating.IpAddress))
                {
                    repository.Create(postRating);
                    repository.SaveChanges();

                    // Acknowledge that the operation was successful
                    status.Status = "Successful";
                }
                else
                {
                    // Acknowledge that the user already rated the post
                    status.Status = "Already rated";
                }
            }

            return Json(status);
        }

        #endregion
    }
}