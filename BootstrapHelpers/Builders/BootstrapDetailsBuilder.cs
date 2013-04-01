using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace BootstrapHelpers.Builders
{
    public class BootstrapDetailBuilder<TModel, TProp> where TProp : new()
    {
        private readonly HtmlHelper<TModel> _htmlHelper;
        private readonly Expression<Func<TModel, TProp>> _propertyExpression;
        private readonly DetailConfig _detailConfig;

        public BootstrapDetailBuilder(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProp>> propertyExpression)
        {
            _htmlHelper = htmlHelper;
            _propertyExpression = propertyExpression;
            _detailConfig = new DetailConfig(_htmlHelper);
        }

        public DetailConfig Finish()
        {
            var propertyMetaData = new PropertyMetaDataBuilder<TProp>();
            var data = _propertyExpression.Compile()(_htmlHelper.ViewData.Model);

            _detailConfig.Properties = propertyMetaData.GetPropertyMetaData();
            _detailConfig.Data = data;

            return _detailConfig;
        }
    }
}