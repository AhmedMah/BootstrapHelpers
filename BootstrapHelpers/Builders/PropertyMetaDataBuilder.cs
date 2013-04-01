using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using MvcContrib.UI.InputBuilder.Conventions;

namespace BootstrapHelpers.Builders
{
    public class PropertyMetaDataBuilder<TProp> where TProp : new()
    {
        public PropertyMetaData[] GetPropertyMetaData()
        {
            var properties = typeof(TProp).GetProperties();
            var columns = new PropertyMetaData[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                var metadata = new PropertyMetaData
                {
                    PropertyName = properties[i].Name,
                    PropertyType = properties[i].PropertyType,
                    Label = GetLabel(properties[i]),
                    HiddenForDisplay = GetHiddenForDisplay(properties[i]),
                    DataFormatString = GetDataFormatString(properties[i])
                };

                columns[i] = metadata;
            }

            return columns;
        }

        private string GetLabel(PropertyInfo propertyInfo)
        {
            var displayAttribute = propertyInfo.GetAttribute<DisplayAttribute>();

            if (displayAttribute != null)
                return displayAttribute.Name;

            var displayNameAttribute = propertyInfo.GetAttribute<DisplayNameAttribute>();

            if (displayNameAttribute != null)
                return displayNameAttribute.DisplayName;

            return propertyInfo.Name;
        }

        private bool GetHiddenForDisplay(PropertyInfo propertyInfo)
        {
            var hiddenInput = propertyInfo.GetAttribute<HiddenInputAttribute>();

            if (hiddenInput != null && !hiddenInput.DisplayValue)
                return true;

            return false;
        }

        private string GetDataFormatString(PropertyInfo propertyInfo)
        {
            var dataFormatString = propertyInfo.GetAttribute<DisplayFormatAttribute>();

            if (dataFormatString != null)
                return dataFormatString.DataFormatString;

            return "{0}";
        }
    }
}