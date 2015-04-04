using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CoreSerivce.PL
{
    [ServiceContract]
    public interface IContents
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "")]
        BO.Contents Insert(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/box/{StateId}")]
        List<BO.Contents> SelectByState(string StateId);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/{ContentId}")]
        BO.Contents SelectById(string ContentId);


        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "UpdatePriority")]
        List<BO.Contents_Categories> UpdatePriority(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/list/{Category}/?Published={Published}&Ordering={Order}")]
        List<BO.Contents> SelectByStatePublishedCategory(string Published, string Category, string  Order);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "?query={query}")]
        List<BO.Contents> Search(string query);


        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "{contentId}")]
        BO.Contents Update(Stream Data, string contentId);


        [OperationContract]
        [WebInvoke(Method = "DELETE",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/{Id}/{StateId}")]
        bool UpdateState(string Id,string StateId);
    }
}
