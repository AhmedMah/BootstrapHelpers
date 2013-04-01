using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace BootstrapHelpers.Builders
{
    public class BootstrapGridBuilder<TModel, TProp> where TProp : new()
    {
        private readonly HtmlHelper<TModel> _htmlHelper;
        private readonly Expression<Func<TModel, TProp[]>> _propertyExpression;
        private readonly GridConfig _gridConfig;

        public BootstrapGridBuilder(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProp[]>> propertyExpression)
        {
            _htmlHelper = htmlHelper;
            _propertyExpression = propertyExpression;
            _gridConfig = new GridConfig(_htmlHelper)
            {
                ModelType = typeof(TModel)
            };
        }

        public BootstrapGridBuilder<TModel, TProp> Id(string id)
        {
            _gridConfig.Id = id;
            return this;
        }

        public BootstrapGridBuilder<TModel, TProp> DetailsAction<TController>(Expression<Action<TController>> expression) where TController : Controller
        {
            _gridConfig.DetailsItemUrl = _htmlHelper.BuildUrlFromExpression(expression);
            return this;
        }

        public BootstrapGridBuilder<TModel, TProp> EditAction<TController>(Expression<Action<TController>> expression) where TController : Controller
        {
            _gridConfig.EditItemUrl = _htmlHelper.BuildUrlFromExpression(expression);
            return this;
        }

        public BootstrapGridBuilder<TModel, TProp> DeleteAction<TController>(Expression<Action<TController>> expression) where TController : Controller
        {
            _gridConfig.DeleteItemUrl = _htmlHelper.BuildUrlFromExpression(expression);
            return this;
        }

        public GridConfig Finish()
        {
            var propertyMetaData = new PropertyMetaDataBuilder<TProp>();
            var data = _propertyExpression.Compile()(_htmlHelper.ViewData.Model);

            _gridConfig.Properties = propertyMetaData.GetPropertyMetaData();
            _gridConfig.Data = data.Select(prop => (object)prop).ToArray();

            return _gridConfig;
        }
    }
}