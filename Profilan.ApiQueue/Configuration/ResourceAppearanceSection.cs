using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Profilan.ApiResource.Configuration
{
    public class ConnectionSection : ConfigurationSection
    {
        [ConfigurationProperty("Resources")]
        public ResourceAppearanceCollection ResourceElement
        {
            get { return ((ResourceAppearanceCollection)(base["Resources"])); }
            set { base["Resources"] = value; }
        }

    }
}
