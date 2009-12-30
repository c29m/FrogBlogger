using System;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace FrogBlogger.Test.Helpers
{
    /// <summary>
    /// Contains unit tests for testing the NavMenuHelper functionality
    /// </summary>
    [TestClass]
    public class NavMenuHelperTests
    {
        /// <summary>
        /// Verify that an exception is thrown if the html parameter is null
        /// </summary>
        [TestMethod]
        public void NullHtmlParameterShouldThrowException()
        {
            bool exceptionThrown = false;
            MockRepository mocks = new MockRepository();
            IPrincipal mockUser = mocks.StrictMock<IPrincipal>();

            try
            {
                NavMenuHelper.AdminMenuItem(null, mockUser);
            }
            catch (ArgumentNullException err)
            {
                exceptionThrown = err.Message.Contains("html");
            }

            Assert.IsTrue(exceptionThrown);
        }

        /// <summary>
        /// Verifies that a null user causes the method to throw an exception
        /// </summary>
        [TestMethod]
        public void NullUserParameterShouldThrowException()
        {
            bool exceptionThrown = false;
            HtmlHelper helper = MockRepository.GenerateStrictMock<HtmlHelper>();

            try
            {
                NavMenuHelper.AdminMenuItem(helper, null);
            }
            catch (ArgumentNullException err)
            {
                exceptionThrown = err.Message.Contains("user");
            }

            Assert.IsTrue(exceptionThrown);
        }

        /// <summary>
        /// Verifies that the helper method returns an empty string if the user does not belong to the Admin role
        /// </summary>
        [TestMethod]
        public void AdminMenuHelperShouldReturnEmptyStringIfNotInAdminRole()
        {
            string returnValue = "GarbageData";
            MockRepository mocks = new MockRepository();
            IPrincipal mockUser = mocks.StrictMock<IPrincipal>();
            HtmlHelper helper = mocks.StrictMock<HtmlHelper>();

            Assert.IsTrue(String.IsNullOrEmpty(returnValue));
        }
    }
}
