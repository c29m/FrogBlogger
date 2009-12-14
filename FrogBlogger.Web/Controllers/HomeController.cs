using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrogBlogger.Web.Models;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int maxRecords = 10; // TODO: Replace the magic number 10 on this line with a user determined value
            HomeViewModel model;
            List<BlogPost> posts;
            Dictionary<Guid, uint> blogPostCommentCount = new Dictionary<Guid,uint>();

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                posts = (from m in repository.Fetch()
                         orderby m.PostedDate descending
                         select m).Take(maxRecords).ToList();

                // Determine the comment count for each blog post
                foreach (BlogPost post in posts)
                {
                    blogPostCommentCount.Add(post.BlogPostId, (uint)post.UserComments.Count);
                }

                model = new HomeViewModel(posts, blogPostCommentCount);
            }

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
