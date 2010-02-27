using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;
using FrogBlogger.Web.Helpers;
using FrogBlogger.Web.Models;
using StructureMap;

namespace FrogBlogger.Web.Controllers
{
    /// <summary>
    /// Contains action methods for working with the home view
    /// </summary>
    [HandleError]
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets the home page
        /// </summary>
        /// <param name="page">Requested page number</param>
        /// <returns>The home page</returns>
        public ActionResult Index(int? page)
        {
            int count;
            int maxRecords = BlogUtility.GetPageSize();
            int skip = (int)(!page.HasValue ? 0 : (page - 1) * maxRecords);
            Guid blogId = BlogUtility.GetBlogId();
            HomeViewModel model;
            List<BlogPost> posts;
            List<Keyword> tags;
            IObjectContext context = new ObjectContextAdapter(DatabaseUtility.GetContext());
            Dictionary<Guid, int> blogPostCommentCount = new Dictionary<Guid, int>();

            using (IBlogPostRepository repository = ObjectFactory.With(context).GetInstance<IBlogPostRepository>())
            using (IDataRepository<Keyword> keywordRepository = new DataRepository<Keyword>(context))
            {
                posts = (from m in repository.Fetch()
                         where m.BlogId == blogId
                         orderby m.PostedDate descending
                         select m).Skip(skip).Take(maxRecords).ToList();

                tags = (from t in keywordRepository.Fetch()
                        where t.BlogId == blogId
                        select t).Take(maxRecords).ToList();

                // Get the total blog post count
                count = (from m in repository.Fetch()
                         where m.BlogId == blogId
                         select m).Count();

                // Determine the comment count for each blog post
                foreach (BlogPost post in posts)
                {
                    blogPostCommentCount.Add(post.BlogPostId, post.UserComments.Count);
                }

                model = new HomeViewModel(posts, tags, blogPostCommentCount, count, page ?? 1, skip + maxRecords < count, skip - maxRecords >= 0);
            }

            return View(model);
        }

        /// <summary>
        /// Gets the About page
        /// </summary>
        /// <returns>The About page</returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Searches the database for blog posts containing the specified keywords
        /// </summary>
        /// <param name="searchTerm">Search term for which to query the model against</param>
        /// <returns>The search results of the query</returns>
        public ActionResult Search(string searchTerm)
        {
            BlogListBase model;
            List<BlogPost> posts;

            using (IDataRepository<BlogPost> repository = new DataRepository<BlogPost>())
            {
                posts = (from b in repository.Fetch()
                         where b.Title.Contains(searchTerm) || b.Post.Contains(searchTerm)
                         select b).ToList();
            }

            model = new BlogListBase(posts);

            return View(model);
        }

        /// <summary>
        /// Searches the database for blog posts containing the specified tags
        /// </summary>
        /// <param name="tag">Search term for which to query the model against</param>
        /// <returns>The search results of the query</returns>
        public ActionResult SearchByTag(string tag)
        {
            Guid blogId = BlogUtility.GetBlogId();
            BlogListBase model;
            List<BlogPost> posts;
            IObjectContext context = new ObjectContextAdapter(DatabaseUtility.GetContext());

            using (IDataRepository<BlogPost> blogPostRepository = new DataRepository<BlogPost>(context))
            using (IDataRepository<Keyword> keywordRepository = new DataRepository<Keyword>(context))
            {
                // TODO: There must be a better way to do this
                posts = (from k in keywordRepository.Fetch()
                            where k.BlogId == blogId && k.Keyword1 == tag
                            select k.BlogPosts).ToList()[0].ToList();
            }

            model = new BlogListBase(posts);

            return View("Search", model);
        }
    }
}
