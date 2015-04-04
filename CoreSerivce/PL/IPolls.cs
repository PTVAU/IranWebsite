using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CoreSerivce.PL
{
    [ServiceContract]
    public interface IPolls
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "")]
        List<BO.Polls> SelectAll();


        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "")]
        BO.Polls Insert(Stream Data);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/{ID}")]
        BO.Polls SelectById(string ID);

        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/{ID}")]
        BO.Polls Update(Stream Data, string ID);
    }
}
