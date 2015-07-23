using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace TCMSFRONTEND.Dal
{
    public class SiteData
    {
        public static List<Bo.Service.Contents> contentsList(string categories, string ordering, string count)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "contents/list/?" + "Categories=" + categories + "&count=" + count + "&ordering=" + ordering);

            //  request.Method = "GET";
            // request.ContentType = "application/x-www-form-urlencoded";
            //  string postData = "Categories=" + categories + "&Ordering=" + ordering;
            //  byte[] bytes = Encoding.UTF8.GetBytes(postData);
            //  request.ContentLength = bytes.Length;

            //  Stream requestStream = request.GetRequestStream();
            // requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();


            List<Bo.Service.Contents> RvLst = JsonConvert.DeserializeObject<List<Bo.Service.Contents>>(result);



            return RvLst;
        }
        public static List<Bo.Service.Contents> contentsListRelatedByTag(string itemId, string count)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/contents/relatedbytag/" + itemId + "/" + count);

            //  request.Method = "GET";
            // request.ContentType = "application/x-www-form-urlencoded";
            //  string postData = "Categories=" + categories + "&Ordering=" + ordering;
            //  byte[] bytes = Encoding.UTF8.GetBytes(postData);
            //  request.ContentLength = bytes.Length;

            //  Stream requestStream = request.GetRequestStream();
            // requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();


            List<Bo.Service.Contents> RvLst = JsonConvert.DeserializeObject<List<Bo.Service.Contents>>(result);



            return RvLst;
        }
        public static Bo.Service.Polls pollsSeletById(string pollId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "polls/" + pollId);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            Bo.Service.Polls RvLst = JsonConvert.DeserializeObject<Bo.Service.Polls>(result);
            return RvLst;
        }
        public static Bo.Service.Contents contentsSelectById(string contentId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "contents/" + contentId);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            Bo.Service.Contents Rv = JsonConvert.DeserializeObject<Bo.Service.Contents>(result);
            return Rv;
        }
        public static List<Bo.Service.Categories> subCategoryByPid(string pid)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "categories/sub/" + pid);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Service.Categories> Rv = JsonConvert.DeserializeObject<List<Bo.Service.Categories>>(result);
            return Rv;
        }
        public static Bo.Service.Categories categorySelectById(string id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "categories/byid/" + id);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            Bo.Service.Categories Rv = JsonConvert.DeserializeObject<Bo.Service.Categories>(result);
            return Rv;
        }
        public static List<Bo.Service.Contents> contentsSelectByTagId(string tagId, string count)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "contents/bytag/" + tagId + "/" + count);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Service.Contents> Rv = JsonConvert.DeserializeObject<List<Bo.Service.Contents>>(result);
            return Rv;
        }
        public static Bo.Service.Polls pollsOptionUpdateCount(string pollId, string optionId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "polls/option/" + pollId + "/" + optionId);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            Bo.Service.Polls Rv = JsonConvert.DeserializeObject<Bo.Service.Polls>(result);
            return Rv;
        }
        public static List<Bo.Data.Programs> programsSelectList(string kind, string count, string order, string status)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "programs/list/" + kind + "/" + count + "/" + order + "/" + status);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Data.Programs> RvLst = JsonConvert.DeserializeObject<List<Bo.Data.Programs>>(result);
            return RvLst;
        }
        public static List<Bo.Data.Episodes> episodesSelectByPid(string pid, string count, string order)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "programs/episodes/" + pid + "/" + count + "/" + order);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Data.Episodes> RvLst = JsonConvert.DeserializeObject<List<Bo.Data.Episodes>>(result);
            return RvLst;
        }
        public static Bo.Data.Episodes episodeSelectById(string episodeId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "programs/episode/" + episodeId);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            Bo.Data.Episodes RvLst = JsonConvert.DeserializeObject<Bo.Data.Episodes>(result);
            return RvLst;
        }
        public static Bo.Data.Programs programsSelectById(string progId, string count, string order)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim()
                + "programs/" + progId + "/" + count + "/" + order);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            Bo.Data.Programs Rv = JsonConvert.DeserializeObject<Bo.Data.Programs>(result);
            return Rv;
        }
        public static void contentsUpdateViewCount(string Id)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim()
                + "/contents/viewcount/" + Id);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
        }
        public static void PrgramsUpdateViewCount(string Id)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim()
                + "/programs/viewcount/" + Id);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
        }
        public static void EpisodesUpdateViewCount(string Id, string Pid)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim()
                + "/programs/episode/viewcount/" + Id + "/" + Pid);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
        }
        public static Bo.Service.Comments CommentsInsert(Bo.Service.Comments Comment)
        {
            WebRequest request = WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/comments");
            request.Method = "POST";
            string postData = "Parent_Id=" + Comment.Parent_Id + "&Content_Id=" + Comment.Content_Id
                + "&Name=" + Comment.Name + "&Email=" + Comment.Email
                + "&Text=" + Comment.Text + "&IP=" + Comment.IP;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();

            return Comment;
        }
        public static string CommentsVote(string commentId, string isUp)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim()
                + "/comments/Vote/" + commentId + "/" + isUp);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;
        }
        public static List<Bo.Data.Episodes> episodesList(string kind, string count, string order,string hours)
        {
            //"/programs/episode/list/{kinds}/{count}/{order}"
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim()
                + "programs/episode/list/" + kind + "/" + count + "/" + order+"/"+hours);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Data.Episodes> RvLst = JsonConvert.DeserializeObject<List<Bo.Data.Episodes>>(result);
            return RvLst;
        }
        public static List<Bo.Service.Contents> contentsListMostViewed(string count, string hours)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/contents/mostviewed/?" + "count=" + count + "&hours=" + hours);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Service.Contents> RvLst = JsonConvert.DeserializeObject<List<Bo.Service.Contents>>(result);
            return RvLst;
        }
        public static List<Bo.Service.Contents> contentsListSearch(string Category, string count, string ordering, string SearchKey)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() +
                "/contents/search/?Category=" +Category+"&count="+count+"&ordering="+ordering+"&SearchKey="+SearchKey);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Service.Contents> RvLst = JsonConvert.DeserializeObject<List<Bo.Service.Contents>>(result);
            return RvLst;
        }
        public static List<Bo.Data.Weather> weatherList()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/share/weather");

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Data.Weather> RvLst = JsonConvert.DeserializeObject<List<Bo.Data.Weather>>(result);
            return RvLst;
        }
        public static List<Bo.Service.Contents> frontendContentsSelectByTag(string count, string tags)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/contents/list/bytag/?" + "count=" + count + "&tags=" + tags);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Service.Contents> RvLst = JsonConvert.DeserializeObject<List<Bo.Service.Contents>>(result);
            return RvLst;
        }
        public static List<Bo.Service.Categories> categoriesSelectAll()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/categories/list");

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Service.Categories> RvLst = JsonConvert.DeserializeObject<List<Bo.Service.Categories>>(result);
            return RvLst;
        }
        public static List<Bo.Service.Tags> tagsSelectMostUsed(string count)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["DbService"].Trim() + "/tags/mostused/"+count);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            List<Bo.Service.Tags> RvLst = JsonConvert.DeserializeObject<List<Bo.Service.Tags>>(result);
            return RvLst;
        }

        
    }
}