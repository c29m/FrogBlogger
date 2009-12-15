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
        private static Guid _blogId = new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C"); // TODO: Place this with the current BlogId

        public ActionResult Index()
        {
            int count;
            int maxRecords = 10; // TODO: Replace the magic number 10 on this line with a user determined value
            HomeViewModel model;
            List<BlogPost> posts;
            Dictionary<Guid, uint> blogPostCommentCount = new Dictionary<Guid,uint>();

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
                    blogPostCommentCount.Add(post.BlogPostId, (uint)post.UserComments.Count);
                }

                model = new HomeViewModel(posts, blogPostCommentCount, (uint)count);
            }

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
