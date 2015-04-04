using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CoreSerivce.PL
{
    [ServiceContract]
    public interface IUsers
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "login")]
        BO.Users User_Login(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "groups")]
        List<BO.Users_Group> SelectAllUsersGroup();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "")]
        List<BO.Users> SelectAllUsers();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "/manage/")]
        List<BO.Users> SelectAllUsersManage();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "groups/{GroupId}")]
        List<BO.Users> SelectGroupList(string GroupId);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "mygroups")]
        List<BO.Users_Group> SelectGroupsForCurrentUser();


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Current")]
        BO.Users SelectUserById();


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "messages/inbox?limit={limit}")]
        List<BO.Message> MessageUserInbox(string limit);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "messages/sent")]
        List<BO.Message> MessageUserSent();


        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "messages")]
        BO.Message InsertMessage(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "messages/seen/{messageId}")]
        string MessageUpdateSeen(string messageId);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "resetpassword/{userId}")]
        string ResetPassWord(string userId);


        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "{userId}")]
        BO.Users UpdateUser(Stream Data,string userId);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "")]
        BO.Users Insert(Stream Data);

        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Current/password")]
        BO.Users UpdateUserPassword(Stream Data);
    }
}
