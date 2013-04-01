using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using BootstrapHelpers.Builders;

namespace BootstrapHelpers
{
    public class BootstrapHelper<TModel>
    {
        private readonly HtmlHelper<TModel> _htmlHelper;

        public BootstrapHelper(HtmlHelper<TModel> htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public BootstrapGridBuilder<TModel, TProp> GridFor<TProp>(Expression<Func<TModel, TProp[]>> propertyExpression) where TProp : new()
        {
            return new BootstrapGridBuilder<TModel, TProp>(_htmlHelper, propertyExpression);
        }

        public BootstrapDetailBuilder<TModel, TProp> DetailFor<TProp>(Expression<Func<TModel, TProp>> propertyExpression) where TProp : new()
        {
            return new BootstrapDetailBuilder<TModel, TProp>(_htmlHelper, propertyExpression);
        }

        public BootstrapButtonBuilder Button(string buttonText)
        {
            return new BootstrapButtonBuilder(_htmlHelper, buttonText);
        }
    }
}