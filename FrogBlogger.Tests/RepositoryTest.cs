using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrogBlogger.Dal;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Tests
{
    /// <summary>
    /// Summary description for RepositoryTest
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {
        public RepositoryTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateTest()
        {
            Guid blogId = Guid.NewGuid();
            string blogName = Guid.NewGuid().ToString();
            Blog fetched;
            Blog original = new Blog
            {
                BlogId = blogId,
                Name = blogName
            };

            using (DataRepository<Blog> repository = new DataRepository<Blog>())
            {
                repository.Create(original);
                repository.SaveChanges();
                fetched = (from x in repository.Fetch()
                          where x.BlogId == blogId
                          select x).First();
            }

            Assert.AreEqual<Guid>(blogId, fetched.BlogId);
            Assert.AreEqual<string>(blogName, fetched.Name);
        }

        [TestMethod]
        public void TestFetch()
        {
            List<Blog> records;

            using (IDataRepository<Blog> repostitory = new DataRepository<Blog>())
            {
                records = new List<Blog>(repostitory.Fetch());
            }

            Assert.IsTrue(records.Count > 0);
        }
    }
}
