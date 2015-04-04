using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;

namespace CoreSerivce.PL
{
    [MessageContract(IsWrapped = false)]
    [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Users : IUsers
    {
        public BO.Users User_Login(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();
            var RqstBody = HttpUtility.ParseQueryString(body);

            var UserObj = new BO.Users();
            UserObj.UserName = RqstBody["username"].ToString();
            UserObj.Password = RqstBody["password"].ToString();
            UserObj = BLL.Users.User_Login(UserObj);
            UserObj.SessionKeyExpire = DateTime.Now.AddMinutes(100).ToString();

            if(UserObj.Id==0)
            {
                var customError = new BO.ErrorHandling("ERROR", "Unauthorized User");
                throw new WebFaultException<BO.ErrorHandling>(customError, HttpStatusCode.Unauthorized);
            }


            return UserObj;
        }
        public List<BO.Users_Group> SelectAllUsersGroup()
        {
            return BLL.Users_Group.SelectAll();
        }
        public List<BO.Users> SelectAllUsers()
        {

            List<BO.Users> UsersList = new List<BO.Users>();
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
           
                UsersList = BLL.Users.SelectAll();
                foreach (BO.Users item in UsersList)
                {
                    item.Groups = BLL.Users_Group.SelectGroupsForCurrentUser(item.Id.ToString());
                    item.LastLogin = BLL.Users.GetLastLogin(item.Id.ToString());
                }
            
            return UsersList;
        }
        public List<BO.Users> SelectAllUsersManage()
        {

            List<BO.Users> UsersList = new List<BO.Users>();
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            if (BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins"))
            {
                UsersList = BLL.Users.SelectAll();
                foreach (BO.Users item in UsersList)
                {
                    item.Groups = BLL.Users_Group.SelectGroupsForCurrentUser(item.Id.ToString());
                    item.LastLogin = BLL.Users.GetLastLogin(item.Id.ToString());
                }
            }
            return UsersList;
        }
        public List<BO.Users> SelectGroupList(string GroupId)
        {
            return BLL.Users.SelectGroupList(GroupId);
        }
        public List<BO.Users_Group> SelectGroupsForCurrentUser()
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            return BLL.Users_Group.SelectGroupsForCurrentUser(CurUsr.Id.ToString());
        }
        public BO.Users SelectUserById()
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            BO.Users Usr = BLL.Users.SelectById(CurUsr.Id.ToString());
            Usr.ProfilePicture = "/files/images/users/0.png";
            Usr.LastLogin = CurUsr.LastLogin;
            return Usr;
        }
        public List<BO.Message> MessageUserInbox(string limit)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());


            var MsgList = BLL.Message.SelectAllInboxForCurrentUser(CurUsr.Id, limit);

            var unReadCount = BLL.Message.SelectCountUnread(CurUsr.Id);

            foreach (BO.Message msg in MsgList)
            {
                msg.MessageFrom = BLL.Users.SelectById(msg.MessageFromId.ToString()).Name;
                msg.TotalUnread = unReadCount;
                msg.MessageFromPicture = "/files/images/users/0.png";
            }


            return MsgList;
        }
        public List<BO.Message> MessageUserSent()
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());


            var MsgList = BLL.Message.SelectAllSentForCurrentUser(CurUsr.Id);



            foreach (BO.Message msg in MsgList)
            {
                msg.MessageTo = BLL.Users.SelectById(msg.MessageToId.ToString()).Name;
            }

            return MsgList;
        }
        public BO.Message InsertMessage(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();


            var MsgObj = new BO.Message();
            MsgObj = JsonConvert.DeserializeObject<BO.Message>(body);


            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());


            MsgObj.MessageFromId = CurUsr.Id;


            BLL.Message.Insert(MsgObj);

            return MsgObj;
        }
        public string MessageUpdateSeen(string messageId)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            BLL.Message.UpdateSeen(int.Parse(messageId.Trim()), CurUsr.Id);
            return "OK";
        }
        public string ResetPassWord(string userId)
        {
            DAL.Users.ResetPassWord(int.Parse(userId));
            return "OK";
        }
        public BO.Users UpdateUser(Stream Data, string userId)
        {
            var body = new StreamReader(Data).ReadToEnd();


            var userObj = new BO.Users();
            userObj = JsonConvert.DeserializeObject<BO.Users>(body);
            userObj.Id = int.Parse(userId);

            //Update Main Fields
            BLL.Users.UpdateUser(userObj);

            //Delete User's Group:
            BLL.Users_Group.DeleteUserGroupByUserId(userId);


            //Insert User's Group:
            foreach (BO.Users_Group item in userObj.Groups)
            {
                BLL.Users_Group.InsertUserGroupMap(item, userObj);
            }


            return userObj;
        }
        public BO.Users Insert(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();
            var userObj = new BO.Users();
            userObj = JsonConvert.DeserializeObject<BO.Users>(body);
            BLL.Users.Insert(userObj.UserName, userObj.Name);
            return userObj;
        }
        public BO.Users UpdateUserPassword(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();
            var userObj = new BO.Users();
            userObj = JsonConvert.DeserializeObject<BO.Users>(body);
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());            
            BLL.Users.updateUserpassword(CurUsr.Id, userObj.Password);           
            return userObj;
        }
    }
}
