using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Web.Controllers
{
    public class BlogController : Controller
    {
        /// <summary>
        /// GET: /Blog/
        /// </summary>
        /// <returns>Returns a listing of all blogs hosted on the site</returns>
        public ActionResult Index()
        {
            List<Blog> blogs;

            using (IDataRepository<Blog> repository = new DataRepository<Blog>())
            {
                blogs = (from b in repository.Fetch()
                         where b.Name == "Sean Test"
                         select b).ToList();
            }

            return View(blogs);
        }

        /// <summary>
        /// GET: /Blog/Details/5
        /// </summary>
        /// <param name="id">ID for which to get the details on</param>
        /// <returns>The details page for a blog</returns>
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// GET: /Blog/Create
        /// </summary>
        /// <returns>A page from which a user can create a blog</returns>
        public ActionResult Create()
        {
            return View();
        } 

        /// <summary>
        /// POST: /Blog/Create
        /// </summary>
        /// <param name="blog">Blog to create</param>
        /// <returns>Returns to the blog listing page</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Blog blog)
        {
            using (IDataRepository<Blog> repository = new DataRepository<Blog>())
            {
                blog.BlogId = Guid.NewGuid();

                repository.Create(blog);
                repository.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
