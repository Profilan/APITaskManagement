using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Profilan.ApiResource.Configuration
{
    [ConfigurationCollection(typeof(Resource))]
    public class ResourceAppearanceCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "resource";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Resource();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Resource)(element)).name;
        }

        public Resource this[int idx]
        {
            get
            {
                return (Resource)BaseGet(idx);
            }
        }
    }
}
