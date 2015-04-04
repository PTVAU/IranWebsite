using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Users
    {
        public static BO.Users User_Login(BO.Users  UserObj)
        {
            return DAL.Users.User_Login(UserObj);
        }
        public static List<BO.Users> SelectAll()
        {
            return DAL.Users.SelectAll();
        }
        public static List<BO.Users> SelectGroupList(string GroupId)
        {
            return DAL.Users.SelectGroupList(GroupId);
        }
        public static BO.Users SelectById(string UserId)
        {
            return DAL.Users.SelectById(UserId);
        }
        public static BO.Users SelectBySessionKey(string SessionKey)
        {
            return DAL.Users.SelectBySessionKey(SessionKey);
        }
        public static string GetLastLogin(string UserID)
        {
            return DAL.Users.GetLastLogin(UserID);
        }
        public static void ResetPassWord(int userId)
        {
            DAL.Users.ResetPassWord(userId);
        }
        public static void UpdateUser(BO.Users usr)
        {
            DAL.Users.UpdateUser(usr);
        }
        public static void Insert(string userName, string name)
        {
            DAL.Users.Insert(userName, name);
        }
        public static void updateUserpassword(int userId, string password)
        {
            DAL.Users.updateUserpassword(userId, password);
        }
    }
}
