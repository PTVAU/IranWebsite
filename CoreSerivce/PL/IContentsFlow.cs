using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CoreSerivce.PL
{
    [ServiceContract]
    public interface IContentsFlow
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "inbox/{GroupId}")]
        List<BO.Contents> Select(string GroupId);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "sent")]
        List<BO.Contents> SelectSent();


        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "sendflow")]
        BO.Contents_Flow SendFlow(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "inbox/notseen")]
        int SelectNotSeen();
    }
}
