using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreSerivce.BLL
{
    public class FrontendConfig
    {
        public static List<BO.siteModules> modulesSelectByAlias(string Alias)
        {
            return DAL.FrontendConfig.modulesSelectByAlias(Alias);
        }
        public static List<BO.urlRouting> urlRoutingSelectAll()
        {
            //Select dynamic route from databse
            List<BO.urlRouting> rtList = DAL.FrontendConfig.urlRoutingSelectAll();

            //Add default route(static) to list:
            //................


            return rtList;
        }
    }
}