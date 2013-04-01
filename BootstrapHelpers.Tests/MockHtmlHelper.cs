using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace BootstrapHelpers.Tests
{
    public class MockHtmlHelper : MockHtmlHelper<object>
    {

    }

    public class MockHtmlHelper<TModel> : HtmlHelper<TModel>
    {
        public MockHtmlHelper()
            : this(new ViewDataDictionary())
        {
        }

        public MockHtmlHelper(ViewDataDictionary viewDataDictionary)
            : base(GetMockViewContext(viewDataDictionary), GetMockViewDataContainer(viewDataDictionary))
        {

        }

        private static IViewDataContainer GetMockViewDataContainer(ViewDataDictionary viewDataDictionary)
        {
            var viewDataContainer = new Mock<IViewDataContainer>();
            viewDataContainer.Setup(v => v.ViewData).Returns(viewDataDictionary);

            return viewDataContainer.Object;
        }

        private static ViewContext GetMockViewContext(ViewDataDictionary viewDataDictionary)
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.ApplicationPath).Returns("/");
            var response = new Mock<HttpResponseBase>();
            response.Setup(r => r.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(p => p);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Response)
                .Returns(response.Object);
            httpContext.Setup(c => c.Request)
                .Returns(request.Object);

            var controllerContext = new ControllerContext(
                httpContext.Object,
                new RouteData(),
                new Mock<ControllerBase>().Object);

            var viewContext = new ViewContext(
                controllerContext,
                new Mock<IView>().Object,
                viewDataDictionary,
                new TempDataDictionary(),
                TextWriter.Null
                );

            return viewContext;
        }
    }
}