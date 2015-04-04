using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TCMSFRONTEND.Core
{
    public class Builder
    {
        public static List<Bo.Site.siteModules> SelectMenuModules(string Alias)
        {
            return Dal.SiteConfig.modulesSelectByPageAlias(Alias);
        }

        public static string LoadLayout(string Layout)
        {
            return Utility.ReadFile(Layout);

        }
    }
}