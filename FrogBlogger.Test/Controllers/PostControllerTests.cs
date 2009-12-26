using System;
using System.Web.Mvc;
using FrogBlogger.Dal;
using FrogBlogger.Web.Controllers;
using FrogBlogger.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrogBlogger.Test.Controllers
{
    /// <summary>
    /// Contains unit tests that verify the PostController
    /// </summary>
    [TestClass]
    public class PostControllerTests
    {
        /// <summary>
        /// Verifies that the comments on a blog post show up in ascending order
        /// </summary>
        [TestMethod]
        public void CommentOrderAscending()
        {
            bool foundComments = false;
            DateTime previousDate = DateTime.MinValue;
            Guid blogPostId = Guid.Empty;
            HomeController homeController = new HomeController();
            PostController postController = new PostController();
            ViewResult homeViewResult = homeController.Index() as ViewResult;
            HomeViewModel homeViewModel = homeViewResult.ViewData.Model as HomeViewModel;
            ViewResult postViewResult;
            ViewPostViewModel detailsPostViewModel;

            foreach (BlogPost post in homeViewModel.BlogPosts)
            {
                if (homeViewModel.BlogPostCommentCount[post.BlogPostId] > 1)
                {
                    foundComments = true;
                    blogPostId = post.BlogPostId;
                    break;
                }
            }

            if (!foundComments)
            {
                Assert.Inconclusive("No comments were found. Test results unknown");
            }
            else if (blogPostId != Guid.Empty)
            {
                postViewResult = postController.Details(blogPostId) as ViewResult;
                detailsPostViewModel = postViewResult.ViewData.Model as ViewPostViewModel;

                foreach (UserComment comment in detailsPostViewModel.Comments)
                {
                    Assert.IsTrue(comment.PostedDate >= previousDate);
                    previousDate = comment.PostedDate.Value;
                }
            }
            else
            {
                Assert.Fail("An unknown error occurred");
            }
        }
    }
}
