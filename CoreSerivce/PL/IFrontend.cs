using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoreSerivce.PL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFrontend" in both code and config file together.
    [ServiceContract]
    public interface IFrontend
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/contents/list/?Categories={Categories}&count={count}&ordering={ordering}")]
        List<BO.Contents> contentsSelectByCategory(string Categories, string count,string ordering);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/contents/{Id}")]
        BO.Contents contentsSelectById(string Id);




        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/Config/modules/List/?Alias={Alias}")]
        List<BO.siteModules> modulesSelectByPageAlias(string Alias);



        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/Config/urlrouting/List")]
        List<BO.urlRouting> urlRoutingList();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/polls/{ID}")]
        BO.Polls pollSelectById(string ID);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/categories/sub/{pid}")]
        List<BO.Categories> subCategoryByPid(string pid);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/categories/byid/{id}")]
        BO.Categories categorySelectById(string id);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/contents/bytag/{tagId}/{count}")]
        List<BO.Contents> contentsSelectByTagId(string tagId, string count);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/contents/relatedbytag/{itemId}/{count}")]
        List<BO.Contents> frontendSelectRelatedByTag(string itemId, string count);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/polls/option/{pollId}/{optionId}")]
        BO.Polls pollUpdateOptionCount(string pollId, string optionId);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/list/{kind}/{count}/{order}/{status}")]
        List<BO.Programs> programsSelectList(string kind, string count, string order,string status);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/episodes/{programId}/{count}/{ordering}")]
        List<BO.Episodes> episodesSelectListByPd(string programId, string count, string ordering);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/episode/{EpisodeId}")]
        BO.Episodes episodeFrontendSelectbyId(string EpisodeId);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/{ProgId}/{count}/{ordering}")]
        BO.Programs programsSelectbyId(string ProgId, string count, string ordering);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/contents/viewcount/{Id}")]
        void UpdateViewCount(string Id);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/episode/viewcount/{Id}/{Pid}")]
        void EpisodesUpdateViewCount(string Id, string Pid);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/viewcount/{Id}")]
        void ProgramsUpdateViewCount(string Id);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/comments")]
        BO.Comments CommentsInsert(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/comments/Vote/{commentId}/{isUp}")]
        int CommentsVote(string commentId, string isUp);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/episode/list/{kinds}/{count}/{order}")]
        List<BO.Episodes> FrontendListEpisodes(string kinds, string count, string order);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/contents/mostviewed/?count={count}&hours={hours}")]
        List<BO.Contents> frontendSelectMostViewed(string count, string hours);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/contents/search/?Category={Category}&count={count}&ordering={ordering}&SearchKey={SearchKey}")]
        List<BO.Contents> frontendSearch(string Category, string count, string ordering, string SearchKey);
        
       
    }
}
