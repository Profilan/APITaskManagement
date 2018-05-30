using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Profilan.ApiResource.Configuration
{
    public class ConfigSettings
    {
        public ConnectionSection ResourceConfiguration
        {
            get
            {
                return (ConnectionSection)ConfigurationManager.GetSection("resourceSection");
            }
        }

        public ResourceAppearanceCollection ResourceAppearances
        {
            get
            {
                return this.ResourceConfiguration.ResourceElement;
            }
        }

        public IEnumerable<Resource> ResourceElements
        {
            get
            {
                foreach (Resource relement in this.ResourceAppearances)
                {
                    if (relement != null)
                        yield return relement;
                }
            }
        }
    }

    
    
}
