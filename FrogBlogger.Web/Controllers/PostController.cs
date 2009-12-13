using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Models;

namespace FrogBlogger.Web.Controllers
{
    public class PostController : Controller
    {
        /// <summary>
        /// GET: /Post/View/{id}
        /// </summary>
        /// <returns>The requested blog post</returns>
        public ActionResult View(Guid id)
        {
            ViewPostViewModel model;
            BlogPost post;
            IList<UserComment> comments;

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                post = repository.Single(x => x.BlogPostId == id);
                comments = post.UserComments.ToList();
                model = new ViewPostViewModel(post, comments);
            }

            return View(model);
        }

    }
}
