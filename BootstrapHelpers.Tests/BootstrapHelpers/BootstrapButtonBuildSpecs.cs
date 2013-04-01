using System.Web.Mvc;
using BootstrapHelpers.Builders;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace BootstrapHelpers.Tests.BootstrapHelpers
{
    public class BootstrapButtonBuilderSpecs
    {
        public class when_creating_a_basic_button : SpecsFor<BootstrapButtonBuilder>
        {
            private string _html;

            protected override void InitializeClassUnderTest()
            {
                var helper = new MockHtmlHelper();
                SUT = new BootstrapButtonBuilder(helper, "Go somewhere").Action<TestController>(i => i.Create());
            }

            protected override void When()
            {
                _html = SUT.Finish().ToHtmlString();
            }

            [Test]
            public void then_it_returns_an_anchor_tag_styled_as_a_button()
            {
                // TODO: Fix me
                _html.ShouldStartWith("<a class=\"btn\"");
                _html.EndsWith("</a>").ShouldBeTrue();
            }
        }
    }

    public class TestController : Controller
    {
        public ActionResult Create()
        {
            return View("fake/view/path");
        }
    }
}