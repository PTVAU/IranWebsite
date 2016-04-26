using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace TCMSFRONTEND.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class News : System.Web.Services.WebService
    {
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsByCategory(string category, string count, string imageSize)
        {
            string cacheKey = "MOBILE-APP-" + category + "-" + count + "-" + imageSize;
            string cacheResult = (string)Core.Utility.cacheControl(cacheKey, null, "R", 0);
            if (string.IsNullOrEmpty(cacheResult))
            {
                List<Bo.Service.Contents> CntList = Dal.SiteData.contentsList(category, "Contents.Published desc", count);
                List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, count, "-1", "0", imageSize, null, CntList);
                foreach (var item in CntListData)
                {
                    item.Fulltext = Regex.Replace(item.Fulltext, @"(?:<blockquote>)(.*?)(?:</blockquote>)", "");
                }
                cacheResult = JsonConvert.SerializeObject(CntListData);
                Core.Utility.cacheControl(cacheKey, cacheResult, "W", 120).ToString();
            }

            HttpContext.Current.Response.Write(cacheResult);


        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsByCategoryNoHtml(string category, string count, string imageSize)
        {
            string cacheKey = "MOBILE-APP-" + category + "-" + count + "-" + imageSize;
            string cacheResult = (string)Core.Utility.cacheControl(cacheKey, null, "R", 0);
            if (string.IsNullOrEmpty(cacheResult))
            {
                List<Bo.Service.Contents> CntList = Dal.SiteData.contentsList(category, "Contents_Categories.Priority, Contents.Published desc", count);
                List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, count, "-1", "0", imageSize, null, CntList);

                foreach (Bo.Data.Contents item in CntListData)
                {
                    item.Fulltext = item.Fulltext;

                }
                cacheResult = JsonConvert.SerializeObject(CntListData);
                Core.Utility.cacheControl(cacheKey, cacheResult, "W", 120).ToString();
            }

            HttpContext.Current.Response.Write(cacheResult);
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsSelectByTag(string count, string tags, string imageSize)
        {
            string cacheKey = "MOBILE-APP-" + tags + "-" + count + "-" + imageSize;
            string cacheResult = (string)Core.Utility.cacheControl(cacheKey, null, "R", 0);
            if (string.IsNullOrEmpty(cacheResult))
            {
                List<Bo.Service.Contents> CntList = Dal.SiteData.frontendContentsSelectByTag(count, tags);
                List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, count, "-1", "0", imageSize, null, CntList);
                foreach (var item in CntListData)
                {
                    item.Fulltext = Regex.Replace(item.Fulltext, @"(?:<blockquote>)(.*?)(?:</blockquote>)", "<br/>");
                }
                cacheResult = JsonConvert.SerializeObject(CntListData);
                Core.Utility.cacheControl(cacheKey, cacheResult, "W", 120).ToString();
            }
            HttpContext.Current.Response.Write(cacheResult);
        }
    
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void categoriesSelectAll()
        {
            List<Bo.Service.Categories> CntList = Dal.SiteData.categoriesSelectAll();
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(CntList));
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void tagsSelectMostUsed(string count)
        {
            List<Bo.Service.Tags> CntList = Dal.SiteData.tagsSelectMostUsed(count);
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(CntList));
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void live()
        {
            HttpContext.Current.Response.Write("http://ptv04.leaseweb.hls.live.cdn.overon.es/04.m3u8");
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod(CacheDuration = 60)]
        public void Rss(string category)
        {
            List<Bo.Service.Contents> CntList = Dal.SiteData.contentsList(category, "Contents_Categories.Priority, Contents.Published desc", "100");
            List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, "100", "-1", "0", "xl", null, CntList);

            StringBuilder rss = new StringBuilder();
            rss.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            rss.AppendLine("<rss xmlns:media=\"http://search.yahoo.com/mrss/\" xmlns:atom=\"http://www.w3.org/2005/Atom\"  version=\"2.0\">");
            rss.AppendLine("<channel>");
            rss.AppendLine("<image>");
            rss.AppendLine("<url>http://www.hispantv.com/Views/Assets/img/logo.png</url>");
            rss.AppendLine("<link>http://www.hispantv.com/rss</link>");
            rss.AppendLine("<title>HispanTV, Nexo Latino</title>");
            rss.AppendLine("</image>");
            rss.AppendLine("<title>HispanTV, Nexo Latino</title>");
            rss.AppendLine("<link>http://www.hispantv.com</link>");
            rss.AppendLine(" <description>HispanTV, Nexo Latino</description>");

            foreach (Bo.Data.Contents item in CntListData)
            {
                item.Fulltext = item.Fulltext;
                rss.AppendLine("<item>");
                rss.AppendLine("<title>" + item.Title + "</title>");
                rss.AppendLine("<link>http://www.hispantv.com" + item.url + "</link>");
                rss.AppendLine("<description>" + item.Introtext + "</description>");
                rss.AppendLine("<guid isPermaLink=\"true\">");
                rss.AppendLine("http://www.hispantv.com" + item.url);
                rss.AppendLine("</guid>");
                rss.AppendLine("<pubDate>");
                rss.AppendLine(item.Published);
                rss.AppendLine("</pubDate>");


                rss.AppendLine("<media:thumbnail url=\"" + item.image + "\"/>");
                if (item.hasVideo)
                {
                    rss.AppendLine("<media:title>" + item.Title + "</media:title>");
                    rss.AppendLine("<media:content url=\"" + item.Videos[0].videoPath + "\"/>");
                }



                rss.AppendLine("</item>");

            }
            rss.AppendLine("</channel>");
            rss.AppendLine("</rss>");
            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.Write(rss);
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsMostViewed(string range)
        {
            List<Bo.Service.Contents> CntList = Dal.SiteData.contentsListMostViewed("7", range);
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(CntList));
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsListSearch(string SearchKey)
        {
            List<Bo.Service.Contents> CntList = Dal.SiteData.contentsListSearch("-1", "30", " Published desc", SearchKey);
            List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, "30", "-1", "0", "l", null, CntList);
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(CntListData));
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void Comments(string cid)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(@"<html>");
            str.AppendLine(@"<head></head>");
            str.AppendLine(@"<body>");
            str.AppendLine("<div id=\"comment\" class=\"row\"><div class=\"col-md-24 col-xs-24\"><div id = \"hypercomments_widget\" ></div></div></div>");
            str.AppendLine("<script type=\"text/javascript\">");
            str.AppendLine("var extid=\"c"+cid+"\";");
            str.AppendLine("_hcwp = window._hcwp || [];");
            str.AppendLine("_hcwp.push({ widget: \"Stream\", widget_id: 26167, xid: extid, hc_disable: 1, quote_disable: 1, social: \"google, facebook, twitter, openid\", comments_level: 2 });");
            str.AppendLine(" (function () {");
            str.AppendLine(" if (\"HC_LOAD_INIT\" in window) return;");
            str.AppendLine("HC_LOAD_INIT = true;");
            str.AppendLine("var lang = (navigator.language || navigator.systemLanguage || navigator.userLanguage || \"en\").substr(0, 2).toLowerCase();");
            str.AppendLine("  var hcc = document.createElement(\"script\"); hcc.type = \"text/javascript\"; hcc.async = true;");
            str.AppendLine("hcc.src = (\"https:\" == document.location.protocol ? \"https\" : \"http\") + \"://w.hypercomments.com/widget/hc/26215/\" + lang + \"/widget.js\";");
            str.AppendLine(" var s = document.getElementsByTagName(\"script\")[0];");
            str.AppendLine(" s.parentNode.insertBefore(hcc, s.nextSibling);");
            str.AppendLine(" })(); </script> </body></html>");

            HttpContext.Current.Response.Write(str.ToString());
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsByCategoryDatetime(string category, string count, string imageSize, string datetime)
        {
            //string cacheKey = "MOBILE-APP-" + category + "-" + count + "-" + imageSize;
            //string cacheResult = (string)Core.Utility.cacheControl(cacheKey, null, "R", 0);
            //if (string.IsNullOrEmpty(cacheResult))
            //{
            //    List<Bo.Service.Contents> CntList = Dal.SiteData.contentsListByDateTime(category, "Contents.Published desc", count, datetime);
            //    List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, count, "-1", "0", imageSize, null, CntList);
            //    foreach (var item in CntListData)
            //    {
            //        item.Fulltext = Regex.Replace(item.Fulltext, @"(?:<blockquote>)(.*?)(?:</blockquote>)", "");
            //    }
            //    cacheResult = JsonConvert.SerializeObject(CntListData);
            //    Core.Utility.cacheControl(cacheKey, cacheResult, "W", 120).ToString();
            //}

            //HttpContext.Current.Response.Write(cacheResult);
        }
    }
}
