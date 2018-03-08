namespace SiCo.Utilities.I18n
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;

    public static class ResourceManagers
    {
        private static Dictionary<Type, ResourceManager> resourceManagers = new Dictionary<Type, ResourceManager>();

        public static ResourceManager GetResourceManager(Type t)
        {
            try
            {
                return resourceManagers.Single(r => r.Key == t).Value;
            }
            catch
            {
                ResourceManager mgr = new ResourceManager(t);

                resourceManagers.Add(t, mgr);

                return mgr;
            }
        }
    }
}