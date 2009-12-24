using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrogBlogger.Web.Controllers;
using System.Web.Mvc;
using FrogBlogger.Web.Models;
using FrogBlogger.Dal;

namespace FrogBlogger.Test.Controllers
{
    /// <summary>
    /// Contains test methods for the home controller
    /// </summary>
    [TestClass]
    public class HomeControllerTests
    {
        #region Fields

        /// <summary>
        /// Stores the HomeController
        /// </summary>
        private HomeController _controller = new HomeController();

        /// <summary>
        /// Stores the result from an action method
        /// </summary>
        private ViewResult _viewResult;

        /// <summary>
        /// Stores the ViewModel returned from an action method
        /// </summary>
        private HomeViewModel _viewModel;

        #endregion

        #region Test Methods

        /// <summary>
        /// Initializes the test class
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _viewResult = _controller.Index() as ViewResult;
            _viewModel = _viewResult.ViewData.Model as HomeViewModel;
        }

        /// <summary>
        /// Verifies that a blog contains blog posts
        /// </summary>
        [TestMethod]
        public void ContainsBlogPosts()
        {
            Assert.IsTrue(_viewModel.BlogPosts.Count > 0);
        }

        /// <summary>
        /// Verifies that the maximum blog post count does not exceed the threshold
        /// </summary>
        [TestMethod]
        public void BlogPostCount()
        {
            Assert.IsTrue(_viewModel.BlogPostCount <= 10); // TODO: Threshold should be user configurable
            Assert.AreEqual<int>(_viewModel.BlogPostCount, _viewModel.BlogPosts.Count);
        }

        /// <summary>
        /// Verifies that the msot recent blog posts fall at the top of the list
        /// </summary>
        [TestMethod]
        public void BlogOrderDescending()
        {
            DateTime previousDate = DateTime.MaxValue;

            foreach (BlogPost post in _viewModel.BlogPosts)
            {
                Assert.IsTrue(post.PostedDate <= previousDate);

                previousDate = post.PostedDate.Value;
            }
        }

        #endregion
    }
}
