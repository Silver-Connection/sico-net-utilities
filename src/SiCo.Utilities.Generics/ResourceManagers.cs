namespace SiCo.Utilities.Generics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;

    internal static class ResourceManagers
    {
        private static Dictionary<Type, ResourceManager> resourceManagers = new Dictionary<Type, ResourceManager>();

        internal static ResourceManager GetResourceManager(Type t)
        {
            try
            {
                return resourceManagers.Single(r => r.Key == t).Value;
            }
            catch
            {
                var mgr = new ResourceManager(t);

                resourceManagers.Add(t, mgr);

                return mgr;
            }
        }
    }
}