using System;

namespace BootstrapHelpers.Builders
{
    public class PropertyMetaData
    {
        public string PropertyName { get; set; }

        public Type PropertyType { get; set; }

        public string Label { get; set; }

        public bool HiddenForDisplay { get; set; }

        public string DataFormatString { get; set; }
    }
}