using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using HtmlTags;
using Microsoft.Web.Mvc;

namespace BootstrapHelpers.Builders
{
    public class BootstrapButtonBuilder
    {
        private readonly HtmlHelper _htmlHelper;
        private readonly string _buttonText;
        private string _id;
        private string _actionUrl;
        private readonly List<string> _additionalCssClasses;
        private string[] _iconClasses;

        public BootstrapButtonBuilder(HtmlHelper htmlHelper, string buttonText)
        {
            _htmlHelper = htmlHelper;
            _buttonText = buttonText;
            _additionalCssClasses = new List<string>();
        }

        public BootstrapButtonBuilder Id(string id)
        {
            _id = id;
            return this;
        }

        public BootstrapButtonBuilder Action<TController>(Expression<Action<TController>> expression) where TController : Controller
        {
            _actionUrl = _htmlHelper.BuildUrlFromExpression(expression);
            return this;
        }

        public BootstrapButtonBuilder ButtonSuccess()
        {
            _additionalCssClasses.Add("btn-success");
            return this;
        }

        public BootstrapButtonBuilder Icon(string iconClass)
        {
            _iconClasses = new[] { iconClass };
            return this;
        }

        public BootstrapButtonBuilder Icon(string[] iconClasses)
        {
            _iconClasses = iconClasses;
            return this;
        }

        public HtmlTag Finish()
        {
            var anchor = new HtmlTag("a")
                .AddClass("btn")
                .Attr("href", _actionUrl);

            if (!string.IsNullOrEmpty(_id))
                anchor.Id(_id);

            if (_additionalCssClasses.Count > 0)
                anchor.AddClasses(_additionalCssClasses);

            if (_iconClasses != null)
            {
                anchor.Add("i").AddClasses(_iconClasses);
                anchor.AppendHtml(string.Format(" {0}", _buttonText));
            }
            else
            {
                anchor.AppendHtml(_buttonText);
            }

            return anchor;
        }
    }
}