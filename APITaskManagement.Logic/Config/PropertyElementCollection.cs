using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Config
{
    public class PropertyElementCollection : ConfigurationElementCollection
    {
        public new PropertyElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;

                return (PropertyElement)BaseGet(name);
            }
        }

        public PropertyElement this[int index]
        {
            get { return (PropertyElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name)
                    return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap;  }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PropertyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PropertyElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "property";  }
        }
    }
}
