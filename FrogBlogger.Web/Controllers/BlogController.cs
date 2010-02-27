using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Helpers;
using MvcContrib;

namespace FrogBlogger.Web.Controllers
{
    /// <summary>
    /// Contains action methods for working with the blog view
    /// </summary>
    public class BlogController : Controller
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BlogController class
        /// </summary>
        public BlogController()
        {
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// GET: /Blog/
        /// </summary>
        /// <returns>Returns a listing of all blogs hosted on the site</returns>
        public ActionResult Index()
        {
            List<Blog> blogs;
            Guid blogId = BlogUtility.GetBlogId();

            using (IDataRepository<Blog> repository = new DataRepository<Blog>())
            {
                blogs = (from b in repository.Fetch()
                         where b.BlogId == blogId
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

            return this.RedirectToAction(a => a.Index());
        }

        #endregion
    }
}
