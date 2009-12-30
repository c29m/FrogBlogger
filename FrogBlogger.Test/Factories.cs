using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;

namespace FrogBlogger.Test
{
    /// <summary>
    /// Contains factory methods for initializing common mocked objects
    /// </summary>
    internal static class Factories
    {
        /// <summary>
        /// Gets a mocked HtmlHelper object
        /// </summary>
        /// <returns>An HtmlHelper object that can be used for testing</returns>
        internal static HtmlHelper InitializeHtmlHelper()
        {
            MockRepository mocks = new MockRepository();
            ControllerContext context = mocks.StrictMock<ControllerContext>(
                mocks.StrictMock<HttpContextBase>(),
                new RouteData(),
                mocks.StrictMock<ControllerBase>());
            ViewContext viewContext = mocks.StrictMock<ViewContext>(context, mocks.StrictMock<IView>(), new ViewDataDictionary(), new TempDataDictionary());
            HtmlHelper helper = mocks.StrictMock<HtmlHelper>(viewContext, mocks.StrictMock<IViewDataContainer>());

            return helper;
        }
    }
}
