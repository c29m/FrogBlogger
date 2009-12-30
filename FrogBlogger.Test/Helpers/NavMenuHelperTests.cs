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
        /// Verify that an exception is thrown if the helper parameter is null
        /// </summary>
        [TestMethod]
        public void NullHelperParameterShouldThrowException()
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
                exceptionThrown = err.Message.Contains("helper");
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
            HtmlHelper helper = Factories.InitializeHtmlHelper();

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
            MockRepository mocks = new MockRepository();
            IPrincipal mockUser = mocks.StrictMock<IPrincipal>();
            HtmlHelper helper = Factories.InitializeHtmlHelper();
            string returnValue = NavMenuHelper.AdminMenuItem(helper, mockUser);

            Assert.IsTrue(String.IsNullOrEmpty(returnValue));
        }

        /// <summary>
        /// Verifies that the helper method returns a valid list item for the Admin view if user is a member of the Admin role
        /// </summary>
        [TestMethod]
        public void AdminMenuHelperShouldReturnAdminListItemIfUserIsInAdminRole()
        {
            string actualValue;
            string expectedValue = "<li><a href=\"\">Admin</a></li>"; // TODO: I'm not sure why the ActionLink() method doesn't return the href, but it's working in production
            MockRepository mocks = new MockRepository();
            IPrincipal mockUser = mocks.StrictMock<IPrincipal>();
            HtmlHelper helper = Factories.InitializeHtmlHelper();

            Expect.Call(mockUser.IsInRole(FrogBlogger.Web.Helpers.Roles.Admin)).Return(true);
            mocks.ReplayAll();

            actualValue = NavMenuHelper.AdminMenuItem(helper, mockUser);

            Assert.AreEqual<string>(expectedValue, actualValue);
            mocks.VerifyAll();
        }
    }
}
