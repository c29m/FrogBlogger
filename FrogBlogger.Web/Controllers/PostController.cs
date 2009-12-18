using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Models;

namespace FrogBlogger.Web.Controllers
{
    public class PostController : Controller
    {
        /// <summary>
        /// GET: /Post/Details/{id}
        /// </summary>
        /// <returns>The requested blog post</returns>
        public ActionResult Details(Guid id)
        {
            ViewPostViewModel model;
            BlogPost post;
            IList<UserComment> comments;

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                post = repository.GetSingle(x => x.BlogPostId == id);
                comments = post.UserComments.ToList();
                model = new ViewPostViewModel(post, comments);
            }

            return View(model);
        }

        /// <summary>
        /// Leaves a comment on a blog post
        /// </summary>
        /// <param name="userComment">The user comment</param>
        /// <returns>Redirects back to the blog post</returns>
        public ActionResult Comment(UserComment userComment)
        {
            userComment.UserCommentId = Guid.NewGuid();
            userComment.PostedDate = DateTime.Now;

            using (IDataRepository<UserComment> repository = new DataRepository<UserComment>())
            {
                repository.Create(userComment);
                repository.SaveChanges();
            }

            return RedirectToAction("View", new { id = userComment.BlogPostId });
        }
    }
}
