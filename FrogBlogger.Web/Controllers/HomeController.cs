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

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                model = new HomeViewModel(
                    (from m in repository.Fetch()
                     select m).Take(maxRecords).ToList());
            }

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
