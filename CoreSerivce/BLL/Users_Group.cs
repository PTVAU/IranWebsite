using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Users_Group
    {
        public static List<BO.Users_Group> SelectAll()
        {
            return DAL.Users_Group.SelectAll();
        }

        public static List<BO.Users_Group> SelectGroupsForCurrentUser(string UserId)
        {
            return DAL.Users_Group.SelectGroupsForCurrentUser(UserId);
        }
        public static bool UserIsInRole(int UserId,string RoleList)
        {
            bool retValue = false;
            List<BO.Users_Group> lst = DAL.Users_Group.SelectGroupsForCurrentUser(UserId.ToString());
            string[] rls=RoleList.Split('#');
            foreach (BO.Users_Group item in lst)
            {
                foreach (string rl in rls)
                {
                    if (item.Title.ToLower() == rl.ToLower())
                        retValue = true;
                }                
            }
            return retValue;
        }
        public static BO.Users_Group SelectById(int Id)
        {
            return DAL.Users_Group.SelectById(Id);
        }
        public static void DeleteUserGroupByUserId(string userId)
        {
            DAL.Users_Group.DeleteUserGroupByUserId(userId);
        }
        public static void InsertUserGroupMap(BO.Users_Group usrGroupMap, BO.Users usrObj)
        {
            DAL.Users_Group.InsertUserGroupMap(usrGroupMap,usrObj);
        }
    }
}
