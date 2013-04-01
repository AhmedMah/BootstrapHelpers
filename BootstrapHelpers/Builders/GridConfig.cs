using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BootstrapHelpers.Builders
{
    public class GridConfig : IHtmlString
    {
        private readonly HtmlHelper _htmlHelper;

        public GridConfig(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public string Id { get; set; }

        public Type ModelType { get; set; }

        public PropertyMetaData[] Properties { get; set; }

        public object[] Data { get; set; }

        public string EditItemUrl { get; set; }

        public bool DetailsEnabled
        {
            get { return !string.IsNullOrEmpty(DetailsItemUrl); }
        }

        public string DetailsItemUrl { get; set; }

        public bool EditingEnabled
        {
            get { return !string.IsNullOrEmpty(EditItemUrl); }
        }

        public string DeleteItemUrl { get; set; }

        public bool DeletingEnabled
        {
            get { return !string.IsNullOrEmpty(DeleteItemUrl); }
        }

        public string ToHtmlString()
        {
            return _htmlHelper.Partial("Grid", this).ToString();
        }
    }
}