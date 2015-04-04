using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace CoreSerivce.PL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPrograms" in both code and config file together.
    [ServiceContract]
    public interface IPrograms
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/programs/?kind={kind}&status={status}")]
        List<BO.Programs> SelectAll(string kind,string status);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/program/{Id}")]
        BO.Programs SelectById(string Id);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/episodes/{Pid}")]
        List<BO.Episodes> episodesSelectByPid(string Pid);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/episode/{Id}")]
        BO.Episodes episodesSelectById(string Id);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/program/")]
        BO.Programs ProgramsInsert(Stream Data);


        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/episode/")]
        BO.Episodes EpisodesInsert(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/program/{id}")]
        BO.Programs ProgramsUpdate(Stream Data,string id);


        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "/episode/{id}")]
        BO.Episodes EpisodesUpdate(Stream Data, string id);
    }
}
