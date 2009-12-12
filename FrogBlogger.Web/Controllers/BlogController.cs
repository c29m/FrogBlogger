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
        //
        // GET: /Blog/

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

        //
        // GET: /Blog/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Blog/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Blog/Create

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

        //
        // GET: /Blog/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
