using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace CoreSerivce.PL
{
    [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Frontend : IFrontend
    {
        public List<BO.Contents> contentsSelectByCategory(string Categories, string count, string ordering)
        {
            var bLLContentsSelectByState = new List<BO.Contents>();

            bLLContentsSelectByState = BLL.Contents.frontendSelect(Categories, count,ordering);


            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Repositories = BLL.Contents_Repository.SelectByConetentsId(item.Id);
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);
            }


            return bLLContentsSelectByState;
        }
        public List<BO.siteModules> modulesSelectByPageAlias(string Alias)
        {
            return BLL.FrontendConfig.modulesSelectByAlias(Alias);
        }
        public List<BO.urlRouting> urlRoutingList()
        {
            return BLL.FrontendConfig.urlRoutingSelectAll();
        }
        public BO.Polls pollSelectById(string ID)
        {
            BO.Polls PollObj;
            if(ID=="-1")
            {
                PollObj = BLL.Polls.SelectActive();
            }
            else
            {
                PollObj = BLL.Polls.SelectById(int.Parse(ID));
            }            

            PollObj.Polls_Options = BLL.Polls_Options.SelectByPid(PollObj.Id);
            return PollObj;
        }
        public BO.Contents contentsSelectById(string ContentId)
        {
            var ContentObj = BLL.Contents.SelectById(int.Parse(ContentId));
            if (ContentObj.IsPublished == 1)
            {
                ContentObj.Tags = BLL.Contents_Tags.SelectByConetntsId(ContentObj.Id);
                ContentObj.Categories = BLL.Contents_Categories.SelectByConetentsId(ContentObj.Id);
                ContentObj.Repositories = BLL.Contents_Repository.SelectByConetentsId(ContentObj.Id);
                ContentObj.Comments = BLL.Comments.SelectByContentId(ContentId, "50", "0");
            }
            else
            {
                ContentObj = new BO.Contents();
            }
            return ContentObj;
        }
        public List<BO.Categories> subCategoryByPid(string pid)
        {
            return BLL.Categories.subCategorySelectByPid(pid);
        }
        public BO.Categories categorySelectById(string id)
        {
           return  DAL.Categories.selectById(id);
        }
        public List<BO.Contents> contentsSelectByTagId(string tagId,string count)
        {
            
            var bLLContentsSelectByState = new List<BO.Contents>();

            bLLContentsSelectByState = BLL.Contents.frontendSelectByTagId(tagId, count);


            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Repositories = BLL.Contents_Repository.SelectByConetentsId(item.Id);
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);
            }


            return bLLContentsSelectByState;
        }
        public BO.Polls pollUpdateOptionCount(string pollId,string optionId)
        {
            BLL.Polls_Options.UpdateCount(int.Parse(optionId));

            var PollObj = BLL.Polls.SelectById(int.Parse(pollId));

            PollObj.Polls_Options = BLL.Polls_Options.SelectByPid(PollObj.Id);

            return PollObj;
        }
        public  List<BO.Programs> programsSelectList(string kind,string count,string order,string status)
        {
            return BLL.Programs.FrontendlistPrograms(kind, count, order,status);
        }
        public List<BO.Episodes> episodesSelectListByPd(string programId, string count, string ordering)
        {
            return BLL.Episodes.listEpisodesByPid(programId, count, ordering);
        }
        public  BO.Episodes episodeFrontendSelectbyId(string EpisodeId)
        {
            return BLL.Episodes.FrontendSelectbyId(int.Parse(EpisodeId));
        }
        public  BO.Programs programsSelectbyId(string ProgId,string count,string ordering)
        {
            BO.Programs prog = BLL.Programs.FrontendSelectbyId(int.Parse(ProgId));
            prog.EpisodesList = BLL.Episodes.listEpisodesByPid(ProgId, count, ordering);
            return prog;
        }
        public List<BO.Contents> frontendSelectRelatedByTag(string itemId, string count)
        {
            var bLLContentsSelectByState = new List<BO.Contents>();
            bLLContentsSelectByState = BLL.Contents.frontendSelectRelatedByTag(itemId, count);
            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Repositories = BLL.Contents_Repository.SelectByConetentsId(item.Id);
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);
            }
            return bLLContentsSelectByState;
        }
        public  void UpdateViewCount(string Id)
        {
            BLL.Contents.UpdateViewCount(int.Parse(Id));
        }
        public void EpisodesUpdateViewCount(string Id,string Pid)
        {
            BLL.Episodes.UpdateViewCount(int.Parse(Id));
            BLL.Programs.UpdateViewCount(int.Parse(Pid));
        }
        public void ProgramsUpdateViewCount(string Id)
        {
            BLL.Programs.UpdateViewCount(int.Parse(Id));
        }
        public BO.Comments CommentsInsert(Stream Data)
        {
            BO.Comments cmnt = new BO.Comments();
            string body = new StreamReader(Data).ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(body);
            cmnt.Parent_Id = int.Parse(nvc["Parent_Id"].ToString());
            cmnt.Content_Id = int.Parse(nvc["Content_Id"].ToString());
            cmnt.Name = nvc["Name"].ToString();
            cmnt.Email = nvc["Email"].ToString();
            cmnt.Text = nvc["Text"].ToString();
            cmnt.IP = nvc["IP"].ToString();
            return BLL.Comments.Insert(cmnt);
        }
        public int CommentsVote(string commentId,string isUp)
        {
            if(isUp=="1")
            {
                BLL.Comments.VoteUp(commentId);
            }
            if(isUp=="0")
            {
                BLL.Comments.VoteDown(commentId);
            }
            BO.Comments cmnt= BLL.Comments.SelectById(commentId);
            return cmnt.Vote_Up - cmnt.Vote_Down;
        }
        public List<BO.Episodes> FrontendListEpisodes(string kinds, string count, string order)
        {
            return DAL.Episodes.FrontendListEpisodes(kinds, count, order);
        }
        public List<BO.Contents> frontendSelectMostViewed(string count, string hours)
        {
            var bLLContentsSelectByState = new List<BO.Contents>();
            bLLContentsSelectByState = BLL.Contents.frontendSelectMostViewed(count, hours);
            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Repositories = BLL.Contents_Repository.SelectByConetentsId(item.Id);
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);
            }
            return bLLContentsSelectByState;
        }
        public List<BO.Contents> frontendSearch(string Category, string count, string ordering, string SearchKey)
        {
            var bLLContentsSelectByState = new List<BO.Contents>();

            bLLContentsSelectByState =BLL.Contents.frontendSearch(Category, count, ordering, SearchKey);


            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Repositories = BLL.Contents_Repository.SelectByConetentsId(item.Id);
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);
            }


            return bLLContentsSelectByState;
        }
    }
}
