using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace TCMSFRONTEND.Dal
{
    public class SiteConfig
    {
        //Select Pages Modules with config...
        public static List<Bo.Site.siteModules> modulesSelectByPageAlias(string Alias)
        {
            List<Bo.Site.siteModules> RvLst = new List<Bo.Site.siteModules>();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/Config/modules/List/?Alias=" + Alias);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                var result = reader.ReadToEnd();
                stream.Dispose();
                reader.Dispose();


                RvLst = JsonConvert.DeserializeObject<List<Bo.Site.siteModules>>(result);
            }
            catch
            {
               
            }

            return RvLst;
            
        }
        public static List<Bo.Site.urlRouting> urlRoutingSelect()
        {
            List<Bo.Site.urlRouting> RvLst = new List<Bo.Site.urlRouting>();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/Config/urlrouting/List");


                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                var result = reader.ReadToEnd();
                stream.Dispose();
                reader.Dispose();


                RvLst = JsonConvert.DeserializeObject<List<Bo.Site.urlRouting>>(result);

            }
            catch 
            {
            }
            return RvLst;

        }
    }
}