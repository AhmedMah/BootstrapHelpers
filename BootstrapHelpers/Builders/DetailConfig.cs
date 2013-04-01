using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BootstrapHelpers.Builders
{
    public class DetailConfig : IHtmlString
    {
        private readonly HtmlHelper _htmlHelper;

        public DetailConfig(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public PropertyMetaData[] Properties { get; set; }

        public object Data { get; set; }

        public string ToHtmlString()
        {
            return _htmlHelper.Partial("Detail", this).ToString();
        }
    }
}