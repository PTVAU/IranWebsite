using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CoreSerivce.PL
{
    [ServiceContract]
    public interface IComments
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "")]
        List<BO.Comments> SelectAll();


        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/{ID}")]
        BO.Comments Update(Stream Data, string ID);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "?query={query}")]
        List<BO.Comments> SelectSearch(string query);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/{id}")]
        BO.Comments SelectById(string id);
    }
}
