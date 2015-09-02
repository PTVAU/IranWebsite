using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace TCMSFRONTEND.Bll
{
    public class SiteData
    {
        public static List<Bo.Data.Contents> contentsList(string categories, string ordering, string count, string imagesIndex, string offset, string imageSuffix, string tagId, List<Bo.Service.Contents> origContentList)
        {
            string[] imgIndex = imagesIndex.Split(',');
            List<Bo.Data.Contents> contetList = new List<Bo.Data.Contents>();

            List<Bo.Service.Contents> serContentList = new List<Bo.Service.Contents>();

            if (origContentList == null)
            {
                if (string.IsNullOrEmpty(tagId))
                {
                    serContentList = Dal.SiteData.contentsList(categories, ordering, (int.Parse(count) + int.Parse(offset)).ToString());
                }
                else
                {
                    serContentList = Dal.SiteData.contentsSelectByTagId(tagId, (int.Parse(count) + int.Parse(offset)).ToString());
                }
            }
            else
            {
                serContentList = origContentList;
            }

            int Itmindx = 0;
            bool firstItem = true;
            foreach (Bo.Service.Contents item in serContentList)
            {
                Bo.Data.Contents cnt = new Bo.Data.Contents();
                cnt.Title = item.Title;
                cnt.Id = item.Id;

                cnt.Published = DateTime.Parse(item.Published).ToString("f",CultureInfo.CreateSpecificCulture("es-ES"));
                cnt.Fulltext = item.Fulltext;
                cnt.Introtext = item.Introtext;
                cnt.ShortTitle = item.ShortTitle;

                //check if first item:
                if (firstItem)
                {
                    cnt.first = true;
                    firstItem = false;
                }

                bool selected = false;
                foreach (Bo.Service.Repositories rp in item.Repositories)
                {
                    //Loop on repositories:
                    if (rp.Kind == 1)
                    {
                        for (int i = 0; i < imgIndex.Length; i++)
                        {
                            if (imgIndex[i] == Itmindx.ToString() || imgIndex[i] == "-1")
                            {
                                //check item image index to show/notshow
                                if (!selected)
                                {
                                    cnt.image = null;
                                    cnt.image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + rp.FilePath.Replace(".jpg", "_" + imageSuffix + ".jpg");
                                    selected = true;
                                }
                            }
                        }
                    }

                    if (rp.Kind == 2)
                    {
                        cnt.hasVideo = true;
                    }
                }

                //Generate Title Alias:
                cnt.Alias = Core.Utility.contentAliasGenerator(item);


                //Loop  on categories
                if (item.Categories.Count > 0)
                {
                    //Check if contents is breaking:
                    for (int i = 0; i < item.Categories.Count; i++)
                    {
                        if (item.Categories[i].Id.ToString() == "102")
                        {
                            cnt.isBreaking = true;
                        }
                    }

                    //    //Find Parent title:
                    //    Bo.Service.Categories parentCat = Dal.SiteData.categorySelectById(item.Categories[0].Parent_Id.ToString());
                    //    cnt.categoryTitle = item.Categories[0].Title;
                    //    cnt.categoryUrl = "/subcategory/" + parentCat.Title + "/" + item.Categories[0].Parent_Id + "/" + item.Categories[0].Title + "/" + item.Categories[0].Id;

                    //Category and Parent category Title and Url:
                    cnt.categoryTitle = item.Categories[0].Title;

                    Bo.Service.Categories parentCat = Dal.SiteData.categorySelectById(item.Categories[0].Parent_Id.ToString());

                    //If parent was selected for contents category 
                    if (item.Categories[0].Parent_Id == 0)
                    {
                        cnt.categoryTitleParent = item.Categories[0].Title;
                        cnt.categoryUrlParent = "/category/" + item.Categories[0].Title + "/" + item.Categories[0].Id;
                        cnt.categoryUrl = "/category/" + item.Categories[0].Title + "/" + item.Categories[0].Id;
                    }
                    else
                    {
                        cnt.categoryTitleParent = parentCat.Title;
                        cnt.categoryUrlParent = "/category/" + Core.Utility.ClearTitle(parentCat.Title) + "/" + parentCat.Id;
                        cnt.categoryUrl = "/subcategory/" + Core.Utility.ClearTitle(parentCat.Title) + "/" + parentCat.Id + "/" + item.Categories[0].Title + "/" + item.Categories[0].Id;
                    }

                }
                else
                {
                    cnt.categoryTitle = "NoCategory";
                }
                cnt.url = "/newsdetail/" + Core.Utility.ClearTitle(cnt.categoryTitle) + "/" + cnt.Id + "/" + Core.Utility.ClearTitle(cnt.Alias);

                Itmindx++;
                //Check item index to skip offset item
                if (Itmindx > int.Parse(offset))
                {
                    contetList.Add(cnt);
                }
            }
            return contetList;
        }
        public static Bo.Data.Polls pollsSeletById(string pollId)
        {
            Bo.Service.Polls pl = Dal.SiteData.pollsSeletById(pollId);
            Bo.Data.Polls plRet = new Bo.Data.Polls();

            plRet.AllowNew = pl.AllowNew;
            plRet.Description = pl.Description;
            plRet.Expired = pl.Expired;
            plRet.Id = pl.Id;
            //plRet.Polls_Options = pl.Polls_Options;
            plRet.ShowResult = pl.ShowResult;
            plRet.ShowTotal = pl.ShowTotal;
            plRet.ShowValues = pl.ShowValues;
            plRet.Title = pl.Title;
            plRet.TotalCount = "";



            List<Bo.Data.Polls_Options> Options = new List<Bo.Data.Polls_Options>();
            int TtCount = 0;
            foreach (Bo.Service.Polls_Options item in pl.Polls_Options)
            {
                TtCount += item.SelectedCount;
            }
            foreach (Bo.Service.Polls_Options item in pl.Polls_Options)
            {
                Bo.Data.Polls_Options op = new Bo.Data.Polls_Options();
                if (item.SelectedCount != 0 && TtCount > 0)
                {
                    op.Percent = Math.Round(double.Parse((item.SelectedCount * 100 / TtCount).ToString()), 2).ToString();
                }
                else
                {
                    op.Percent = "0";
                }
                op.Title = item.Title;
                op.SelectedCount = item.SelectedCount;
                op.Id = item.Id;

                Options.Add(op);
            }



            plRet.Polls_Options = Options;
            plRet.TotalCount = TtCount.ToString();
            return plRet;
        }
        public static Bo.Data.Contents contentsSelectById(string contentId)
        {
            Bo.Data.Contents cnt = new Bo.Data.Contents();
            Bo.Service.Contents servContent = Dal.SiteData.contentsSelectById(contentId);
            cnt.Title = servContent.Title;
            cnt.Published = DateTime.Parse(servContent.Published).ToString("f", CultureInfo.CreateSpecificCulture("es-ES"));
            cnt.Fulltext = servContent.Fulltext;
            //Remove word tags:
            cnt.Fulltext = Regex.Replace(cnt.Fulltext, "<!--.*?-->", "", RegexOptions.Singleline);
            //Replace original inline photo with thumbnails
            cnt.Fulltext = cnt.Fulltext.Replace(".jpg", "_xl.jpg").Replace("/Original/", "/thumbnail/");
            cnt.Introtext = servContent.Introtext;
            cnt.Id = servContent.Id;
            cnt.Youtube = servContent.Youtube;
            cnt.Viewcount = servContent.Viewcount;

            //Images
            List<Bo.Data.contentsImages> imgList = new List<Bo.Data.contentsImages>();

            //Videos
            List<Bo.Data.contentsVideo> videoList = new List<Bo.Data.contentsVideo>();
            bool selected = false;
            foreach (Bo.Service.Repositories rp in servContent.Repositories)
            {
                //Loop on repositories:
                if (rp.Kind == 1)
                {
                    Bo.Data.contentsImages img = new Bo.Data.contentsImages();
                    img.imagePath = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + rp.FilePath.Replace(".jpg", "_xl.jpg").Replace("Original", "thumbnail");
                    img.description = rp.Description;
                    imgList.Add(img);

                    if (!selected)
                    {
                        cnt.image = null;
                        cnt.image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + rp.FilePath.Replace(".jpg", "_xl.jpg");
                        selected = true;
                    }
                }
            }
                foreach (Bo.Service.Repositories rp in servContent.Repositories)
                {
                    if (rp.Kind == 2)
                {
                    Bo.Data.contentsVideo vdo = new Bo.Data.contentsVideo();
                    vdo.videoPath = rp.FilePath.ToLower().Replace(".mpg", ".mp4").Replace(".mov", ".mp4").Replace(".jpg", ".mp4");
                   
                        vdo.videoThumbail = cnt.image; //vdo.videoPath.ToLower().Replace(".mp4", ".jpg").Replace(".mpg", ".jpg");
                   
                    vdo.description = rp.Description;
                    videoList.Add(vdo);
                    cnt.hasVideo = true;
                }
            }
            cnt.Images = imgList;
            cnt.Videos = videoList;


            List<Bo.Data.Tags> tgList = new List<Bo.Data.Tags>();

            foreach (Bo.Service.Tags item in servContent.Tags)
            {
                Bo.Data.Tags tg = new Bo.Data.Tags();
                tg.url = "/tag/" + Core.Utility.ClearTitle(item.name) + "/" + item.id;
                tg.name = item.name;
                tgList.Add(tg);
            }

            cnt.Tags = tgList;

            if (servContent.Categories.Count > 0)
            {

                //Category and Parent category Title and Url:
                cnt.categoryTitle = servContent.Categories[0].Title;

                Bo.Service.Categories parentCat = Dal.SiteData.categorySelectById(servContent.Categories[0].Parent_Id.ToString());


                //If parent was selected for contents category 
                if (servContent.Categories[0].Parent_Id == 0)
                {
                    cnt.categoryTitleParent = null;
                    cnt.categoryUrlParent = null;
                    cnt.categoryUrl = "/category/" + Core.Utility.ClearTitle(servContent.Categories[0].Title) + "/" + servContent.Categories[0].Id;
                }
                else
                {
                    cnt.categoryTitleParent = parentCat.Title;
                    cnt.categoryUrlParent = "/category/" + Core.Utility.ClearTitle(parentCat.Title) + "/" + parentCat.Id;
                    cnt.categoryUrl = "/subcategory/" + Core.Utility.ClearTitle(parentCat.Title) + "/" + parentCat.Id + "/" + Core.Utility.ClearTitle(servContent.Categories[0].Title) + "/" + servContent.Categories[0].Id;
                }

                //Find same category news:
                List<Bo.Data.Contents> rtContents = new List<Bo.Data.Contents>();
                rtContents = Bll.SiteData.contentsList(servContent.Categories[0].Id.ToString(), "id desc", "11", "-1", "0", "m", null, null);
                
                //Remove current item if exist in related list    
                for (int i = 0; i < rtContents.Count; i++)
                {
                    if (rtContents[i].Id.ToString() == contentId)
                        rtContents.RemoveAt(i);
                }
                if (rtContents.Count > 3)
                    rtContents.RemoveAt(3);

                cnt.relatedByCategory = rtContents;
               
                //Find same category news:
                List<Bo.Data.Contents> rtContentsByTag = new List<Bo.Data.Contents>();
                rtContentsByTag = Bll.SiteData.contentsListRelatedByTag(cnt.Id.ToString(), "3");
                cnt.relatedByTag = rtContentsByTag;
            }
            else
            {
                cnt.categoryTitle = "NoCategory";
            }
            //Generate Title Alias:
            cnt.Alias = Core.Utility.contentAliasGenerator(servContent);
            cnt.url = "/newsdetail/" + Core.Utility.ClearTitle(cnt.categoryTitle) + "/" + cnt.Id + "/" + Core.Utility.ClearTitle(cnt.Alias);
            


            
            //Comments:
            List<Bo.Data.Comments> cmntsList = new List<Bo.Data.Comments>();
            int CommentsCount = 0;
            foreach (Bo.Service.Comments cm in servContent.Comments)
            {
                CommentsCount++;

                Bo.Data.Comments cmnt = new Bo.Data.Comments();
                cmnt.Content_Id = cm.Content_Id;
                cmnt.Datetime_Insert = cm.Datetime_Insert;
                cmnt.Email = cm.Email;
                cmnt.Id = cm.Id;
                cmnt.IP = cm.IP;
                cmnt.Name = cm.Name;
                cmnt.Parent_Id = cm.Parent_Id;
                cmnt.Text = cm.Text;
                cmnt.Vote_Down = cm.Vote_Down;
                cmnt.Vote_Up = cm.Vote_Up;
                cmnt.Vote = cm.Vote_Up - cm.Vote_Down;



                //Get Replies:
                List<Bo.Data.Comments> cmntsListRep = new List<Bo.Data.Comments>();
                foreach (Bo.Service.Comments cmRep in cm.Reply)
                {
                    CommentsCount++;

                    Bo.Data.Comments cmntRep = new Bo.Data.Comments();
                    cmntRep.Content_Id = cmRep.Content_Id;
                    cmntRep.Datetime_Insert = cmRep.Datetime_Insert;
                    cmntRep.Email = cmRep.Email;
                    cmntRep.Id = cmRep.Id;
                    cmntRep.IP = cmRep.IP;
                    cmntRep.Name = cmRep.Name;
                    cmntRep.Parent_Id = cmRep.Parent_Id;
                    cmntRep.Text = cmRep.Text;
                    cmntRep.Vote_Down = cmRep.Vote_Down;
                    cmntRep.Vote_Up = cmRep.Vote_Up;
                    cmntRep.Vote = cmRep.Vote_Up - cmRep.Vote_Down;

                    cmntsListRep.Add(cmntRep);
                }
                cmnt.Reply = cmntsListRep;


                cmntsList.Add(cmnt);
            }
            cnt.Comments = cmntsList;
            cnt.CommentsCount = CommentsCount.ToString();
            return cnt;
        }
        public static List<Bo.Data.Categories> subCategoryByPid(string pid, string parentTitle, string subId, bool subCategoryExist)
        {
            List<Bo.Data.Categories> ctgList = new List<Bo.Data.Categories>();

            List<Bo.Service.Categories> ctgListSrv = Dal.SiteData.subCategoryByPid(pid);

            //Add parent Category to list:
            Bo.Data.Categories ctgParent = new Bo.Data.Categories();
            ctgParent.parentTitle = parentTitle;
            ctgParent.Parent_Id = int.Parse(pid);
            ctgParent.Id = int.Parse(pid);
            ctgParent.Title = "TODAS";
            ctgParent.url = "/category/" + Core.Utility.ClearTitle(parentTitle) + "/" + pid;
            if (!subCategoryExist)
                ctgParent.active = "true";           
                ctgList.Add(ctgParent);

            //Add subcategories to list:
            foreach (Bo.Service.Categories item in ctgListSrv)
            {
                Bo.Data.Categories ctg = new Bo.Data.Categories();
                ctg.parentTitle = parentTitle;
                ctg.Parent_Id = int.Parse(pid);
                ctg.Id = item.Id;
                ctg.Title = item.Title;
                ctg.url = "/subcategory/" + Core.Utility.ClearTitle(parentTitle) + "/" + pid + "/" + Core.Utility.ClearTitle(item.Title) + "/" + item.Id;
                if (!string.IsNullOrEmpty(subId))
                {
                    if (item.Id.ToString() == subId)
                    {
                        ctg.active = "true";
                    }
                }

                ctgList.Add(ctg);
            }



            return ctgList;
        }
        public static List<Bo.Data.Programs> programsSelectList(string kind, string count, string order, string episodeCount, string episodesOrdering, string status, string imageSuffix,string offset)
        {
            List<Bo.Data.Programs> Lst = Dal.SiteData.programsSelectList(kind,(int.Parse(count) + int.Parse(offset)).ToString(), order, status);

            List<Bo.Data.Programs> RetList = new List<Bo.Data.Programs>();
            int indx = 0;
            int addedIndx = 0;
            foreach (Bo.Data.Programs item in Lst)
            {
                item.ProducedShort = DateTime.Parse(item.Produced).ToString("yyyy/MM/dd");
                item.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
                item.Url = "/showprogram/" + Core.Utility.ClearTitle(item.Title) + "/" + item.Id;
                if (indx == 0)
                {
                    item.first = (indx == 0) ? true : false;
                }
                if (int.Parse(episodeCount) != 0)
                {
                    item.EpisodesList = episodesSelectByPid(item.Id.ToString(), episodeCount, episodesOrdering, item.Title, "xl");
                }
                indx++;

                //Check item index to skip offset item
                if (indx > int.Parse(offset))
                {

                    //Add bool property to list of progs for menu list:
                    addedIndx++;
                    if (addedIndx < 9)
                    {
                        item.MenuFirstColumn = true;
                    }
                    else
                    {
                        item.MenuFirstColumn = false;
                    }

                    RetList.Add(item);
                }
            }

            return RetList;

        }
        public static List<Bo.Data.Episodes> episodesSelectByPid(string pid, string count, string order, string programTile, string imageSuffix)
        {
            List<Bo.Data.Episodes> Lst = Dal.SiteData.episodesSelectByPid(pid, count, order);

            int indx = 0;
            foreach (Bo.Data.Episodes item in Lst)
            {
                item.ProducedShort = DateTime.Parse(item.Produced).ToString("yyyy/MM/dd");
                item.first = (indx == 0) ? true : false;
                item.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
                item.Url = "/showepisode/" + Core.Utility.ClearTitle(programTile) + "/" +Core.Utility.ClearTitle(item.Title) + "/" + item.Id;
                indx++;
            }

            return Lst;
        }
        public static Bo.Data.Episodes episodeSelectById(string episodeId, string imageSuffix)
        {
            Bo.Data.Episodes eps = Dal.SiteData.episodeSelectById(episodeId);
            eps.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + eps.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
            eps.VideoThumbnail = eps.Video.ToLower().Replace(".mp4", ".jpg").Replace(".mpg", ".jpg");
            eps.Video = eps.Video.ToLower().Replace(".mpg", ".mp4");


            //Load Episdode list
            Bo.Data.Programs prog = Dal.SiteData.programsSelectById(eps.Pid.ToString(), "100", "id desc");
            prog.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + prog.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
            prog.Url = "/showprogram/" + Core.Utility.ClearTitle(prog.Title) + "/" + prog.Id;

            List<Bo.Data.Episodes> Lst = prog.EpisodesList;

            int indx = 0;
            foreach (Bo.Data.Episodes item in Lst)
            {
                item.ProducedShort = DateTime.Parse(item.Produced).ToString("yyyy/MM/dd");
                item.first = (indx == 0) ? true : false;
                item.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
                item.Url = "/showepisode/" + Core.Utility.ClearTitle(prog.Title) + "/" + item.Id + Core.Utility.ClearTitle(item.Title) + "/" + item.Id;
                indx++;
            }
            eps.EpisodesList = Lst;


            return eps;
        }
        public static Bo.Data.Programs programsSelectById(string progId, string count, string order, string imageSuffix)
        {
            Bo.Data.Programs prog = Dal.SiteData.programsSelectById(progId, count, order);
            prog.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + prog.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
            prog.Url = "/showprogram/" + Core.Utility.ClearTitle(prog.Title) + "/" + prog.Id;


            List<Bo.Data.Episodes> Lst = prog.EpisodesList;

            int indx = 0;
            foreach (Bo.Data.Episodes item in Lst)
            {
                item.ProducedShort = DateTime.Parse(item.Produced).ToString("yyyy/MM/dd");
                item.first = (indx == 0) ? true : false;
                item.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
                item.Url = "/showepisode/" + Core.Utility.ClearTitle(prog.Title) + "/" + item.Id + Core.Utility.ClearTitle(item.Title) + "/" + item.Id;
                indx++;
            }

            return prog;
        }
        public static List<Bo.Data.Contents> contentsListRelatedByTag(string itemId, string count)
        {

            List<Bo.Data.Contents> contetList = new List<Bo.Data.Contents>();

            List<Bo.Service.Contents> serContentList = new List<Bo.Service.Contents>();


            serContentList = Dal.SiteData.contentsListRelatedByTag(itemId, count);

            int Itmindx = 0;
            bool firstItem = true;
            foreach (Bo.Service.Contents item in serContentList)
            {
                Bo.Data.Contents cnt = new Bo.Data.Contents();
                cnt.Title = item.Title;
                cnt.Id = item.Id;


                cnt.Published = DateTime.Parse(item.Published).ToString("f");
                cnt.Fulltext = item.Fulltext;
                cnt.Introtext = item.Introtext;

                //check if first item:
                if (firstItem)
                {
                    cnt.first = true;
                    firstItem = false;
                }

                bool selected = false;
                foreach (Bo.Service.Repositories rp in item.Repositories)
                {
                    //Loop on repositories:
                    if (rp.Kind == 1)
                    {

                        //check item image index to show/notshow
                        if (!selected)
                        {
                            cnt.image = null;
                            cnt.image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + rp.FilePath.Replace(".jpg", "_m.jpg");
                            selected = true;
                        }

                    }

                    if (rp.Kind == 2)
                    {
                        cnt.hasVideo = true;
                    }
                }

                //Generate Title Alias:
                cnt.Alias = Core.Utility.contentAliasGenerator(item);


                //Loop  on categories
                if (item.Categories.Count > 0)
                {
                    //Check if contents is breaking:
                    for (int i = 0; i < item.Categories.Count; i++)
                    {
                        if (item.Categories[i].Id.ToString() == "102")
                        {
                            cnt.isBreaking = true;
                        }
                    }

                    //Find Parent title:
                    Bo.Service.Categories parentCat = Dal.SiteData.categorySelectById(item.Categories[0].Parent_Id.ToString());
                    cnt.categoryTitle = item.Categories[0].Title;
                    cnt.categoryUrl = "/subcategory/" + parentCat.Title + "/" + item.Categories[0].Parent_Id + "/" + item.Categories[0].Title + "/" + item.Categories[0].Id;
                }
                else
                {
                    cnt.categoryTitle = "NoCategory";
                }
                cnt.url = "/newsdetail/" + Core.Utility.ClearTitle(cnt.categoryTitle) + "/" + cnt.Id + "/" + cnt.Alias;

                Itmindx++;

                contetList.Add(cnt);

            }
            return contetList;
        }
        public static void contentsUpdateViewCount(string Id)
        {
            Dal.SiteData.contentsUpdateViewCount(Id);
        }
        public static void PrgramsUpdateViewCount(string Id)
        {
            Dal.SiteData.PrgramsUpdateViewCount(Id);
        }
        public static void EpisodesUpdateViewCount(string Id, string Pid)
        {
            Dal.SiteData.EpisodesUpdateViewCount(Id,Pid);
        }
        public static List<Bo.Data.Episodes> episodesList(string kind, string count, string order, string programTile, string imageSuffix,string hours)
        {
            List<Bo.Data.Episodes> Lst = Dal.SiteData.episodesList(kind, count, order,hours);

            int indx = 0;
            foreach (Bo.Data.Episodes item in Lst)
            {
                item.ProducedShort = DateTime.Parse(item.Produced).ToString("yyyy/MM/dd");
                item.first = (indx == 0) ? true : false;
                item.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.Image.Replace(".jpg", "_" + imageSuffix + ".jpg");
                item.Url = "/showepisode/" + Core.Utility.ClearTitle(programTile) + "/" +Core.Utility.ClearTitle(item.Title) + "/" + item.Id;
                indx++;
            }

            return Lst;
        }
        public static List<Bo.Data.Weather> weatherList()
        {
            List<Bo.Data.Weather> Lst=Dal.SiteData.weatherList();
            for (int i = 0; i < Lst.Count; i++)
            {
               if(i==0)
               {
                   Lst[i].first = true;
               }
                else
               {
                   Lst[i].first = false;
               }
            }
            return Lst;
        }
    }
}