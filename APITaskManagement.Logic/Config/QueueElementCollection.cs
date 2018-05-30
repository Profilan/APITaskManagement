using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Config
{
    public class QueueElementCollection : ConfigurationElementCollection
    {
        public QueueElementCollection()
        {
            QueueElement details = (QueueElement)CreateNewElement();
            if (details.Name != "")
            {
                Add(details);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new QueueElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((QueueElement) element).Name;
        }

        public QueueElement this[int index]
        {
            get
            {
                return (QueueElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public QueueElement this[string name]
        {
            get
            {
                return (QueueElement)BaseGet(name);
            }
        }

        public int IndexOf(QueueElement details)
        {
            return BaseIndexOf(details);
        }

        public void Add(QueueElement details)
        {
            BaseAdd(details);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(QueueElement details)
        {
            if (BaseIndexOf(details) >= 0)
                BaseRemove(details.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override string ElementName
        {
            get
            {
                return "queue";
            }
        }
    }
}
