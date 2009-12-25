using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Models;

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

            using (IDataRepository<BlogPost> blogPostRepository = new DataRepository<BlogPost>(context))
            using (IDataRepository<Author> authorRepository = new DataRepository<Author>(context))
            using (IDataRepository<aspnet_Users> userRepository = new DataRepository<aspnet_Users>(context))
            {
                posts = (from m in blogPostRepository.Fetch()
                         orderby m.PostedDate descending
                         select m).ToList();

                users = (from a in authorRepository.Fetch()
                        where a.BlogId == new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C") // TODO: Replace this
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
            StringBuilder keywords = new StringBuilder();
            IList<Keyword> tags;

            using (IDataRepository<Keyword> keywordRepository = new DataRepository<Keyword>())
            {
                tags = (from t in keywordRepository.Fetch()
                        where t.BlogId == new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C") // TODO: Replace this
                        select t).ToList();
            }

            foreach (Keyword keyword in tags)
            {
                keywords.AppendFormat("{0} ", keyword.Keyword1);
            }

            ViewData["tags"] = keywords.ToString().TrimEnd();

            return View();
        }

        /// <summary>
        /// Admin/Create
        /// </summary>
        /// <param name="blogPost">BlogPost record to create</param>
        /// <param name="tags">Any tags that might be associated with the post</param>
        /// <returns>Redirects to the listing</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(BlogPost blogPost, string tags)
        {
            FrogBloggerEntities context = DatabaseUtility.GetContext();

            // Create the tags
            CreateTags(tags);

            blogPost.BlogId = new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C"); // TODO: Replace with current blog ID
            blogPost.UserId = null; // TODO: Replace with current user ID
            blogPost.PostedDate = DateTime.Now;
            blogPost.BlogPostId = Guid.NewGuid();

            using (IDataRepository<BlogPost> blogPostRepository = new DataRepository<BlogPost>(context))
            using (IDataRepository<Keyword> keywordRepository = new DataRepository<Keyword>(context))
            {
                // Associate any specified tags
                foreach (string tag in tags.Split(','))
                {
                    blogPost.Keywords.Add(keywordRepository.GetSingle(k => k.Keyword1 == tag.Trim()));
                }

                // Create the blog post
                blogPostRepository.Create(blogPost);

                // Save the changes to the database
                context.SaveChanges();
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

        /// <summary>
        /// Creates tags in the database if they don't already exist
        /// </summary>
        /// <param name="tags">Tags to create</param>
        private static void CreateTags(string tags)
        {
            using (IDataRepository<Keyword> repository = new DataRepository<Keyword>())
            {
                foreach (string tag in tags.Trim().Split(','))
                {
                    if (!repository.Fetch(k => k.Keyword1 == tag).Any())
                    {
                        repository.Create(new Keyword
                        {
                            KeywordId = Guid.NewGuid(),
                            BlogId = new Guid("7F2C3923-5FC8-4A8C-8ABF-21DD40F16C6C"), // TODO: Replace with current blog ID
                            Keyword1 = tag.Trim()
                        });

                        repository.SaveChanges();
                    }
                }
            }
        }
    }
}
