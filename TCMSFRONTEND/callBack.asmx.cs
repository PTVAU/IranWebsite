using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Services;
using System.Web.Services;


namespace TCMSFRONTEND
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class callBack : System.Web.Services.WebService
    {
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void HelloWorld(int id)
        {
            Bo.Data.Contents d = new Bo.Data.Contents();
            d.Id = id;
            Context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(d));
        }

        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public void insertPoll(string pollId, string optionId)
        {
            HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Dal.SiteData.pollsOptionUpdateCount(pollId, optionId)));
        }

        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsList(string categories, string ordering, string count, string imagesIndex, string offset, string imagesSuffix, string viewPath)
        {
            Bo.Site.moduleTypeContents c = new Bo.Site.moduleTypeContents();
            c.categories = categories;
            c.count = count;
            c.imagesIndex = imagesIndex;
            c.imagesSuffix = imagesSuffix;
            c.offset = offset;
            c.ordering = ordering;

            Bo.Wrapper Wr = new Bo.Wrapper();
            Bo.Site.siteModules md = new Bo.Site.siteModules();
            md.Site_Modules_Params = JsonConvert.SerializeObject(c);// prms;// @"{'categories': '2,3,7,8,9,10,11,12','ordering': 'Ordering',count:'5','imagesIndex':'0','offset':'0','imagesSuffix':'l'}";
            Wr.Config = md;

            Wr.Config.Site_Modules_Body = Core.Utility.ReadFile(@"\Views\Modules\" + viewPath + "-CallBack.html");
            string result = Core.ModuleBuilder.ContentsList(Wr).Site_Modules_Generated_Html;
            result = Regex.Replace(result, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);

            HttpContext.Current.Response.Write(result);
        }

        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ProgramsList(string kind, string count, string ordering, string episodesCount, string episodesOrdering, string status, string imagesSuffix, string viewPath, string visibleCount, string htmlClass)
        {
            Bo.Site.moduleTypePrograms c = new Bo.Site.moduleTypePrograms();
            c.kind = kind;
            c.count = count;
            c.episodesCount = episodesCount;
            c.episodesOrdering = episodesOrdering;
            c.status = status;
            c.imagesSuffix = imagesSuffix;
            c.visibleCount = visibleCount;
            c.htmlClass = htmlClass;
            c.ordering = ordering;

            Bo.Wrapper Wr = new Bo.Wrapper();
            Bo.Site.siteModules md = new Bo.Site.siteModules();
            md.Site_Modules_Params = JsonConvert.SerializeObject(c);//  {  "kind": "3",  "ordering": "id desc",count:"6","imagesIndex":"-1","offset":"0","imagesSuffix":"l","episodesCount":"0","episodesOrdering":"Number desc","status":"1","visibleCount":"3"}
            Wr.Config = md;

            Wr.Config.Site_Modules_Body = Core.Utility.ReadFile(@"\Views\Modules\" + viewPath + "-CallBack.html");
            string result = Core.ModuleBuilder.programsList(Wr).Site_Modules_Generated_Html;
            result = Regex.Replace(result, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);

            HttpContext.Current.Response.Write(result);
        }

        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsViewCount(string id)
        {

            if (HttpContext.Current.Request.Cookies["HTV-Content-" + id] == null)
            {
                Bll.SiteData.contentsUpdateViewCount(id);
                HttpCookie aCookie = new HttpCookie("HTV-Content-" + id);
                aCookie.Expires = DateTime.Now.AddMinutes(10);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }

        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void PrgramsViewCount(string id)
        {
            Bll.SiteData.PrgramsUpdateViewCount(id);
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void EpisodesViewCount(string id, string Pid)
        {
            Bll.SiteData.EpisodesUpdateViewCount(id, Pid);
        }
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public void GetPoll(string pollId)
        {
            HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Bll.SiteData.pollsSeletById(pollId)));
        }

        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public void InsertComment(string Parent_Id, string Content_Id, string Name, string Email, string Text)
        {
            Bo.Service.Comments cmnt = new Bo.Service.Comments();
            cmnt.IP = HttpContext.Current.Request.UserHostAddress;
            cmnt.Parent_Id = int.Parse(Parent_Id);
            cmnt.Content_Id = int.Parse(Content_Id);
            cmnt.Email = Email;
            cmnt.Name = Name;
            cmnt.Text = Text;
            HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Dal.SiteData.CommentsInsert(cmnt)));
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void CommentsVote(string CommentId, string Up)
        {
            HttpContext.Current.Response.Write(Dal.SiteData.CommentsVote(CommentId, Up));
        }
        [ScriptMethod(UseHttpGet = true)]
        [WebMethod]
        public void ContentsMostViewed(string range)
        {
            List<Bo.Service.Contents> CntList = Dal.SiteData.contentsListMostViewed("7", range);
            Bo.Wrapper Wr = new Bo.Wrapper();
            Bo.Site.siteModules md = new Bo.Site.siteModules();
            Wr.Config = md;

            Wr.Config.Site_Modules_Body = Core.Utility.ReadFile(@"\Views\Modules\ContentsList\ItemlistMostViewed-CallBack.html");
            List<Bo.Data.Contents> CntListData = Bll.SiteData.contentsList("", null, "7", "-1", "0", "s", null, CntList);

            Wr.Data = CntListData;
            string result = Core.Utility.templateDataMerger(Wr.Config.Site_Modules_Body, Wr);

            result = Regex.Replace(result, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);

            HttpContext.Current.Response.Write(result);
        }
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public void SendEmail(string name, string from, string to, string newsId)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential = new NetworkCredential("noreply@hispantv.com", "%123456%");
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress("noreply@hispantv.com");

                smtpClient.Host = "mail.hispantv.com";
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;
                // smtpClient.Timeout = (60 * 5 * 1000);

                message.From = fromAddress;
                message.Subject = "Hispantv Title";
                message.IsBodyHtml = false;
                string url = "http://hispantv.ir/newsdetail/email/" + newsId + "/emailnews";
                message.Body = "Test text email from:" + from + " and name: "+name+"\r\nPlease click on link :" + "\r\n" + url;
                message.To.Add(to);
                smtpClient.Send(message);
            }
            catch { }
        }
    }
}