using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;

namespace TCMSFRONTEND.Core
{
    public class ModuleBuilder
    {
        public static Bo.Site.siteModules findBuilder(Bo.Site.siteModules md)
        {
            //Generate wrapper:
            Bo.Wrapper Wr = new Bo.Wrapper();
            Wr.Config = md;
          
            //Read template from file:
            Wr.Config.Site_Modules_Body = Core.Utility.ReadFile(Wr.Config.Site_Modules_Body);

            //Generate Body Html:
            switch (Wr.Config.Site_Modules_List_Builder)
            {
                case "contentsList":
                    return ContentsList(Wr);
                case "staticHtml":
                    return staticHtml(Wr);
                case "poll":
                    return pollViewer(Wr);
                case "contentShow":
                    return contentShow(Wr);
                case "subcategorymenu":
                    return subCategoryMenu(Wr);
                case "programslist":
                    return programsList(Wr);
                case "episodeshow":
                    return episodeShow(Wr);
                case "programshow":
                    return programShow(Wr);
                case "episodeslist":
                    return episodesList(Wr);
                case "contentSearch":
                    return contentSearch(Wr);
                default:
                    md.Site_Modules_Generated_Html = "No builder found.";
                    return md;
            }       

        }
        public static Bo.Site.siteModules ContentsList(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypeContents Params = new Bo.Site.moduleTypeContents();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypeContents>(Wr.Config.Site_Modules_Params);


            //Skip "
            // Wr.Config.Site_Modules_Params = Wr.Config.Site_Modules_Params.Replace("\"", "\\\"");

            if (!string.IsNullOrEmpty(Wr.Config.Site_Modules_ViewPath))
            {
                //Substring View template
                Wr.Config.Site_Modules_ViewPath = Wr.Config.Site_Modules_ViewPath.Replace("\\Views\\Modules\\", "").Replace(".html", "");
            }
            //get data from DAL layer:
            List<Bo.Data.Contents> contentsList = new List<Bo.Data.Contents>();
            RouteData routeDt = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            if (Params.categories == "0")
            {
                //get category from url:
                //Load items for one subcategory               
                Params.categories = routeDt.Values["subCategoryID"].ToString();
            }

            if (Params.categories == "-2")
            {
                //Get subcategories list and load items for all sub categories:
                List<Bo.Data.Categories> ctgList = Bll.SiteData.subCategoryByPid(routeDt.Values["CategoryID"].ToString(), "", "", false);
                Params.categories = "";
                foreach (Bo.Data.Categories ctgItm in ctgList)
                {
                    Params.categories += ctgItm.Id + ",";
                }
                if (Params.categories.Length > 2)
                {
                    //Remove latest ","
                    Params.categories = Params.categories.Remove(Params.categories.Length - 1, 1);
                }
            }

            //Load Contents for tag:
            //If tagid exist in route data load contents for tag:
            string tagId = null;
            if (Params.categories == "-3")
            {
                try
                {
                    tagId = routeDt.Values["TagID"].ToString();
                    Wr.Config.Site_Modules_Title = routeDt.Values["TagTitle"].ToString().Replace("-", " ");
                    Params.categories = "tag"+routeDt.Values["TagID"].ToString();
                }
                catch
                {
                    tagId = null;
                }
            }
            //Find tag Id for Callback tag:
            if (Params.categories.Contains("tag"))
            {
                try
                {
                    tagId = Params.categories.Replace("tag", "");
                }
                catch
                {
                    tagId = null;
                }
            }

            Wr.Config.Site_Modules_Params= JsonConvert.SerializeObject(Params);

            contentsList = Bll.SiteData.contentsList(Params.categories, Params.ordering, Params.count, Params.imagesIndex, Params.offset, Params.imagesSuffix, tagId,null);
            Wr.Data = contentsList;

            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        private static Bo.Site.siteModules staticHtml(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypeContents Params = new Bo.Site.moduleTypeContents();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypeContents>(Wr.Config.Site_Modules_Params);



            ////get data from DAL layer:
            //List<Bo.Data.Contents> contentsList = new List<Bo.Data.Contents>();
            //contentsList = Bll.SiteData.contentsList(Params.categories, Params.ordering);
            //Wr.Data = contentsList;



            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        private static Bo.Site.siteModules pollViewer(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypePolls Params = new Bo.Site.moduleTypePolls();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypePolls>(Wr.Config.Site_Modules_Params);



            //get data from DAL layer:
            Bo.Data.Polls pl = new Bo.Data.Polls();
            pl = Bll.SiteData.pollsSeletById(Params.pollId);
            Wr.Data = pl;



            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        private static Bo.Site.siteModules contentShow(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypeContents Params = new Bo.Site.moduleTypeContents();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypeContents>(Wr.Config.Site_Modules_Params);

            RouteData routeDt = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            //get data from DAL layer:
            Bo.Data.Contents cnt = Bll.SiteData.contentsSelectById(routeDt.Values["NewsID"].ToString());
            Wr.Data = cnt;

            //Page Config:
            Bo.Site.sitePageConfig cnfg = new Bo.Site.sitePageConfig();
            cnfg.title = cnt.Title;
            cnfg.description = cnt.Introtext;
            cnfg.image = cnt.image;
            cnfg.datetime = cnt.Published;
            Wr.Config.pageConfig = cnfg;

            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        private static Bo.Site.siteModules subCategoryMenu(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypeContents Params = new Bo.Site.moduleTypeContents();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypeContents>(Wr.Config.Site_Modules_Params);

            RouteData routeDt = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            //get data from DAL layer:
            string subCatId = null;
            bool SubCategory = false;
            try
            {
                subCatId = routeDt.Values["subCategoryID"].ToString();
                SubCategory = true;
            }
            catch
            {
                subCatId = null;
                SubCategory = false;
            }
            List<Bo.Data.Categories> ctg = Bll.SiteData.subCategoryByPid(routeDt.Values["CategoryID"].ToString(), routeDt.Values["CategoryTitle"].ToString(), subCatId, SubCategory);
            if (ctg.Count>1)
            Wr.Data = ctg;

            //Override module  title:

            //if url contanins CategoryTitle:
            Wr.Config.Site_Modules_Title = routeDt.Values["CategoryTitle"].ToString().Replace("-", " ");


            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        public static Bo.Site.siteModules programsList(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypePrograms Params = new Bo.Site.moduleTypePrograms();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypePrograms>(Wr.Config.Site_Modules_Params);


            //get data from DAL layer:
            List<Bo.Data.Programs> programsList = new List<Bo.Data.Programs>();
            // RouteData routeDt = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            if (!string.IsNullOrEmpty(Wr.Config.Site_Modules_ViewPath))
            {
                //Substring View template
                Wr.Config.Site_Modules_ViewPath = Wr.Config.Site_Modules_ViewPath.Replace("\\Views\\Modules\\", "").Replace(".html", "");
            }


            Wr.Config.visibleCount = Params.visibleCount;
            Wr.Config.htmlClass = Params.htmlClass;

            programsList = Bll.SiteData.programsSelectList(Params.kind, Params.count, Params.ordering, Params.episodesCount, Params.episodesOrdering, Params.status, Params.imagesSuffix,Params.offset);
            Wr.Data = programsList;

            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        private static Bo.Site.siteModules episodeShow(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypeContents Params = new Bo.Site.moduleTypeContents();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypeContents>(Wr.Config.Site_Modules_Params);

            RouteData routeDt = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            //get data from DAL layer:
            Bo.Data.Episodes cnt = Bll.SiteData.episodeSelectById(routeDt.Values["EpisodeId"].ToString(), "xl");
            Wr.Data = cnt;


            //Page Config:
            Bo.Site.sitePageConfig cnfg = new Bo.Site.sitePageConfig();
            cnfg.title = cnt.Title;
            cnfg.description = cnt.Introtext;
            cnfg.image = cnt.Image;
            cnfg.datetime = cnt.Published;
            Wr.Config.pageConfig = cnfg;

            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        private static Bo.Site.siteModules programShow(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypePrograms Params = new Bo.Site.moduleTypePrograms();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypePrograms>(Wr.Config.Site_Modules_Params);


            //get data from DAL layer:
            Bo.Data.Programs prog = new Bo.Data.Programs();
            RouteData routeDt = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            Wr.Config.visibleCount = Params.visibleCount;
            Wr.Config.htmlClass = Params.htmlClass;

            prog = Bll.SiteData.programsSelectById(routeDt.Values["ProgId"].ToString(), Params.episodesCount, Params.episodesOrdering,Params.imagesSuffix);
            Wr.Data = prog;

            //Page Config:
            Bo.Site.sitePageConfig cnfg = new Bo.Site.sitePageConfig();
            cnfg.title = prog.Title;
            cnfg.description = prog.IntroText;
            cnfg.image = prog.Image;
            cnfg.datetime = prog.Published;
            Wr.Config.pageConfig = cnfg;

            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }
        public static Bo.Site.siteModules episodesList(Bo.Wrapper Wr)
        {
            //Get module params:
            Bo.Site.moduleTypeEpisodes Params = new Bo.Site.moduleTypeEpisodes();
            Params = JsonConvert.DeserializeObject<Bo.Site.moduleTypeEpisodes>(Wr.Config.Site_Modules_Params);


            //get data from DAL layer:
            List<Bo.Data.Episodes> episodesList = new List<Bo.Data.Episodes>();
            // RouteData routeDt = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            if (!string.IsNullOrEmpty(Wr.Config.Site_Modules_ViewPath))
            {
                //Substring View template
                Wr.Config.Site_Modules_ViewPath = Wr.Config.Site_Modules_ViewPath.Replace("\\Views\\Modules\\", "").Replace(".html", "");
            }

            Wr.Config.visibleCount = Params.visibleCount;
            Wr.Config.htmlClass = Params.htmlClass;

            episodesList = Bll.SiteData.episodesList(Params.kind, Params.count,Params.ordering,"episode", Params.imagesSuffix);
            Wr.Data = episodesList;

            //Call merger method to fill template with data and config:
            Wr.Config.Site_Modules_Generated_Html = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            return Wr.Config;
        }

        public static Bo.Site.siteModules contentSearch(Bo.Wrapper Wr)
        {
            string searchkey = "";
            try
            {
                searchkey=HttpContext.Current.Request.QueryString["q"].Trim();
            }
            catch
            {}
            List<Bo.Service.Contents> CntList = Dal.SiteData.contentsListSearch("-1", "100", " Published desc", searchkey);           
            Bo.Site.siteModules md = new Bo.Site.siteModules();
            md.searchKey = searchkey;
            Wr.Config = md;       

            Wr.Config.Site_Modules_Body = Core.Utility.ReadFile(@"\Views\Modules\ContentsList\SearchResults.html");
            List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, "100", "-1", "0", "s", null, CntList);

            Wr.Data = CntListData;
            string result = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);
            result = Regex.Replace(result, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);
            Wr.Config.Site_Modules_Generated_Html = result;
            return Wr.Config;
        }
    }
}