using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Users_Group
    {
        public static List<BO.Users_Group> SelectAll()
        {
            var UGList = new List<BO.Users_Group>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Users_Group order by title ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Up = new BO.Users_Group();
                    Up.Id = int.Parse(Dr["Id"].ToString());
                    Up.Title = Dr["Title"].ToString();
                    UGList.Add(Up);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }



            return UGList;
        }
        public static List<BO.Users_Group> SelectByUserId(string UserID)
        {
            var UGList = new List<BO.Users_Group>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Users_Group order by title ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Up = new BO.Users_Group();
                    Up.Id = int.Parse(Dr["Id"].ToString());
                    Up.Title = Dr["Title"].ToString();
                    UGList.Add(Up);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }



            return UGList;
        }
        public static List<BO.Users_Group> SelectGroupsForCurrentUser(string UserId)
        {
            var UGList = new List<BO.Users_Group>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT      *
                            FROM            Users_Group INNER JOIN
                            Users_Group_Map ON Users_Group.Id = Users_Group_Map.GroupId  where Users_Group_Map.UserId=" + UserId + "   order by  Users_Group.title ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Up = new BO.Users_Group();
                    Up.Id = int.Parse(Dr["Id"].ToString());
                    Up.Title = Dr["Title"].ToString();
                    UGList.Add(Up);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }



            return UGList;
        }
        public static BO.Users_Group SelectById(int Id)
        {
            var Up = new BO.Users_Group();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Users_Group where id=" + Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                Up.Id = int.Parse(Dr["Id"].ToString());
                Up.Title = Dr["Title"].ToString();
            }









            sqlCommand.Connection.Close();
            sqlCommand.Dispose();




            return Up;
        }
        public static void DeleteUserGroupByUserId(string userId)
        {

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"delete Users_Group_Map where UserId=" + userId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

        }

        public static void InsertUserGroupMap(BO.Users_Group usrGroupMap, BO.Users usrObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"insert into  Users_Group_Map (GroupId,UserId) values(" + usrGroupMap.Id + "," + usrObj.Id + ") ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

        }
    }
}
