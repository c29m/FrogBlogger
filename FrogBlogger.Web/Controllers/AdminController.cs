using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using FrogBlogger.Web.Models;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Dal;
using System.Web.Security;

namespace FrogBlogger.Web.Controllers
{
    /// <summary>
    /// Contains actions methods for administering the site
    /// </summary>
    //[Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        /// <summary>
        /// GET: /Admin/
        /// </summary>
        /// <returns>Returns a list of blog posts</returns>
        public ActionResult Index()
        {
            AdminViewModel model;
            IList<BlogPost> posts;
            IList<aspnet_Users> users;
            FrogBloggerEntities context = DatabaseUtility.GetContext();

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                posts = (from m in repository.Fetch()
                         orderby m.PostedDate descending
                         select m).ToList();
            }

            using (IDataRepository<Author> authorRepository = new DataRepository<Author>(context))
            using (IDataRepository<aspnet_Users> userRepository = new DataRepository<aspnet_Users>(context))
            {
                users = (from a in authorRepository.Fetch()
                        where a.BlogId == new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C")
                        join u in userRepository.Fetch() on a.UserId equals u.UserId
                        select u).ToList();
            }

            model = new AdminViewModel(posts, users);

            return View(model);
        }

        /// <summary>
        /// Admin/Create
        /// </summary>
        /// <returns>Form for creating a blog</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Admin/Create
        /// </summary>
        /// <param name="blogPost">BlogPost record to create</param>
        /// <returns>Redirects to the listing</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(BlogPost blogPost)
        {
            blogPost.BlogId = new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C"); // TODO: Replace with current blog ID
            blogPost.UserId = null; // TODO: Replace with current user ID
            blogPost.PostedDate = DateTime.Now;
            blogPost.BlogPostId = Guid.NewGuid();

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                repository.Create(blogPost);
                repository.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes the specified blog post
        /// </summary>
        /// <param name="id">Blog post to delete</param>
        /// <returns>Redirects to the listing page</returns>
        public ActionResult Delete(Guid id)
        {
            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                repository.Delete(x => x.BlogPostId == id);
                repository.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
