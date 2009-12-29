using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Helpers;
using FrogBlogger.Web.Models;

namespace FrogBlogger.Web.Controllers
{
    /// <summary>
    /// Contains actions methods for administering the site
    /// </summary>
    [Authorize(Roles = FrogBlogger.Web.Helpers.Roles.Admin)]
    public class AdminController : Controller
    {
        /// <summary>
        /// GET: /Admin/
        /// </summary>
        /// <returns>Returns a list of blog posts</returns>
        public ActionResult Index()
        {
            AdminViewModel model;
            Blog blog;
            IList<BlogPost> posts;
            IList<aspnet_Users> users;
            Guid blogId = BlogUtility.GetBlogId();
            FrogBloggerEntities context = DatabaseUtility.GetContext();

            using (IDataRepository<Blog> blogRepository = new DataRepository<Blog>(context))
            using (IDataRepository<BlogPost> blogPostRepository = new DataRepository<BlogPost>(context))
            using (IDataRepository<Author> authorRepository = new DataRepository<Author>(context))
            using (IDataRepository<aspnet_Users> userRepository = new DataRepository<aspnet_Users>(context))
            {
                posts = (from m in blogPostRepository.Fetch()
                         orderby m.PostedDate descending
                         select m).ToList();

                users = (from a in authorRepository.Fetch()
                         where a.BlogId == blogId
                         join u in userRepository.Fetch() on a.UserId equals u.UserId
                         select u).ToList();

                blog = blogRepository.GetSingle(b => b.BlogId == blogId);
            }

            model = new AdminViewModel(posts, users, blog);

            return View(model);
        }

        /// <summary>
        /// Admin/Create
        /// </summary>
        /// <returns>Form for creating a blog post</returns>
        public ActionResult Create()
        {
            StringBuilder keywords = new StringBuilder();
            IList<Keyword> tags;
            Guid blogId = BlogUtility.GetBlogId();

            using (IDataRepository<Keyword> keywordRepository = new DataRepository<Keyword>())
            {
                tags = (from t in keywordRepository.Fetch()
                        where t.BlogId == blogId
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

            blogPost.BlogId = BlogUtility.GetBlogId();
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
        /// Adds a user as an author for the current blog
        /// </summary>
        /// <param name="name">Username for which to add</param>
        /// <returns>The user that was just created, serialized as a JSON result</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateUser(string name)
        {
            RoleProvider provider = System.Web.Security.Roles.Provider;
            Blog blog;
            Guid blogId = BlogUtility.GetBlogId();
            AjaxResponseStatus status = new AjaxResponseStatus("Successful");

            // Add user to the admin role
            provider.AddUsersToRoles(new string[] { name }, new string[] { FrogBlogger.Web.Helpers.Roles.Admin });

            // Add user as author of current blog
            using (IDataRepository<Blog> repository = new DataRepository<Blog>())
            {
                blog = repository.GetSingle(b => b.BlogId == blogId);
                blog.Authors.Add(new Author
                {
                    BlogId = blog.BlogId,
                    UserId = (Guid)Membership.GetUser(name).ProviderUserKey
                });

                repository.SaveChanges();
            }

            return Json(status);
        }

        /// <summary>
        /// Deletes the specified blog post
        /// </summary>
        /// <param name="id">Blog post to delete</param>
        /// <returns>Redirects to the listing page</returns>
        [AcceptVerbs(HttpVerbs.Delete)]
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
        /// Deletes the user specified by the user ID
        /// </summary>
        /// <param name="id">Unique ID of the user for which to delete</param>
        /// <returns>Redirects to the Index view</returns>
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult DeleteAuthor(Guid id)
        {
            using (IDataRepository<Author> repository = new DataRepository<Author>())
            {
                repository.Delete(a => a.UserId == id && a.BlogId == BlogUtility.GetBlogId());
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
                            BlogId = BlogUtility.GetBlogId(),
                            Keyword1 = tag.Trim()
                        });

                        repository.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// GET: /Admin/Edit
        /// </summary>
        /// <param name="id">ID of the record to edit</param>
        /// <returns>A page to edit the post</returns>
        public ActionResult Edit(Guid id)
        {
            BlogPost post;
            IList<Keyword> tags;
            Guid blogId = BlogUtility.GetBlogId();
            StringBuilder keywords = new StringBuilder();
            FrogBloggerEntities context = DatabaseUtility.GetContext();

            using (IDataRepository<BlogPost> blogPostRepository = new DataRepository<BlogPost>(context))
            using (IDataRepository<Keyword> keywordRepository = new DataRepository<Keyword>(context))
            {
                post = blogPostRepository.GetSingle(p => p.BlogPostId == id);

                tags = (from t in keywordRepository.Fetch()
                        where t.BlogId == blogId
                        select t).ToList();

                foreach (Keyword keyword in tags)
                {
                    keywords.AppendFormat("{0} ", keyword.Keyword1);
                }

                ViewData["tags"] = keywords.ToString().TrimEnd();
            }

            return View(post);
        }

        /// <summary>
        /// POST: /Admin/Update/
        /// </summary>
        /// <param name="blogPost">BlogPost to update</param>
        /// <param name="tags">Any tags that might be associated with the post</param>
        /// <returns>redirects to the blog post listing</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Update([Bind(Include = "BlogPostId, Title, Post, Visible")] BlogPost blogPost, string tags)
        {
            BlogPost tempPost;

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                tempPost = repository.GetSingle(p => p.BlogPostId == blogPost.BlogPostId && p.BlogId == BlogUtility.GetBlogId());

                // Update only specific properties
                tempPost.Title = blogPost.Title;
                tempPost.Post = blogPost.Post;
                tempPost.Visible = blogPost.Visible;

                // Save the changes
                repository.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// POST: /Admin/UpdateBlog/
        /// </summary>
        /// <param name="blog">Blog to update</param>
        /// <returns>Redirects to the Index page</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBlog([Bind(Include = "BlogId, Name, FriendlyName")]Blog blog)
        {
            Guid blogId = BlogUtility.GetBlogId();
            Blog tempBlog;

            using (IDataRepository<Blog> repository = new DataRepository<Blog>())
            {
                tempBlog = repository.GetSingle(b => b.BlogId == blogId);
                tempBlog.Name = blog.Name;
                tempBlog.FriendlyName = blog.FriendlyName;

                // Saves the changes to the database
                repository.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
