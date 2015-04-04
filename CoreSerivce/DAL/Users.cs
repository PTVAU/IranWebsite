using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace CoreSerivce.DAL
{
    public class Users
    {
        public static BO.Users User_Login(BO.Users UserObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Users Where Username=N'" + UserObj.UserName + "' and Password=N'" + UserObj.Password + "'";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    UserObj.Id = int.Parse(Dr["id"].ToString());
                    UserObj.Name = Dr["Name"].ToString();
                }


                if (UserObj.Id > 0)
                {
                    UserObj = User_InsertSession(UserObj);
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



            return UserObj;
        }
        public static BO.Users User_InsertSession(BO.Users UserObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"declare @NID nvarchar(128);  set @NID=NEWID(); insert into Users_Session (Id,UserId) values(@NID," + UserObj.Id + ") SELECT @NID";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                UserObj.SessionKey = sqlCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }
            return UserObj;
        }
        public static List<BO.Users> SelectAll()
        {
            var UsersList = new List<BO.Users>();
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Users  order by name ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Usr = new BO.Users();
                    Usr.AccessSecurityKey = double.Parse(Dr["AccessSecurityKey"].ToString());
                    Usr.ActiveState = short.Parse(Dr["ActiveState"].ToString());
                    Usr.CellPhone = Dr["CellPhone"].ToString();
                    Usr.Email = Dr["Email"].ToString();
                    Usr.Id = int.Parse(Dr["Id"].ToString());
                    //Usr.LastLogin = Dr["LastLogin"].ToString();
                    Usr.MenuSecurityKey = double.Parse(Dr["MenuSecurityKey"].ToString());
                    Usr.Name = Dr["Name"].ToString();

                    //Usr.Password = Dr["Password"].ToString();
                    Usr.PasswordChanged = Dr["PasswordChanged"].ToString();
                    Usr.Registered = Dr["Registered"].ToString();
                    Usr.UserName = Dr["UserName"].ToString();

                    UsersList.Add(Usr);
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



            return UsersList;
        }
        public static List<BO.Users> SelectGroupList(string GroupId)
        {
            var UsersList = new List<BO.Users>();
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT    Users.*
                                        FROM            Users INNER JOIN
                                        Users_Group_Map ON Users.Id = Users_Group_Map.UserId where Users_Group_Map.GroupId=" + GroupId + " order by  Users.name";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Usr = new BO.Users();
                    Usr.AccessSecurityKey = double.Parse(Dr["AccessSecurityKey"].ToString());
                    Usr.ActiveState = short.Parse(Dr["ActiveState"].ToString());
                    Usr.CellPhone = Dr["CellPhone"].ToString();
                    Usr.Email = Dr["Email"].ToString();
                    Usr.Id = int.Parse(Dr["Id"].ToString());
                    //Usr.LastLogin = Dr["LastLogin"].ToString();
                    Usr.MenuSecurityKey = double.Parse(Dr["MenuSecurityKey"].ToString());
                    Usr.Name = Dr["Name"].ToString();

                   // Usr.Password = Dr["Password"].ToString();
                    Usr.PasswordChanged = Dr["PasswordChanged"].ToString();
                    Usr.Registered = Dr["Registered"].ToString();
                    Usr.UserName = Dr["UserName"].ToString();

                    UsersList.Add(Usr);
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



            return UsersList;
        }
        public static BO.Users SelectById(string UserId)
        {
            var Usr = new BO.Users();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Users where id=" + UserId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    Usr.AccessSecurityKey = double.Parse(Dr["AccessSecurityKey"].ToString());
                    Usr.ActiveState = short.Parse(Dr["ActiveState"].ToString());
                    Usr.CellPhone = Dr["CellPhone"].ToString();
                    Usr.Email = Dr["Email"].ToString();
                    Usr.Id = int.Parse(Dr["Id"].ToString());
                   // Usr.LastLogin = Dr["LastLogin"].ToString();
                    Usr.MenuSecurityKey = double.Parse(Dr["MenuSecurityKey"].ToString());
                    Usr.Name = Dr["Name"].ToString();

                //    Usr.Password = Dr["Password"].ToString();
                    Usr.PasswordChanged = Dr["PasswordChanged"].ToString();
                    Usr.Registered = Dr["Registered"].ToString();
                    Usr.UserName = Dr["UserName"].ToString();
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



            return Usr;
        }
        public static BO.Users SelectBySessionKey(string SessionKey)
        {
            var Usr = new BO.Users();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @" SELECT top(1)       * FROM            Users INNER JOIN
                                        Users_Session ON Users.Id = Users_Session.UserId
                                        where Users_Session.id=N'" + SessionKey + @"'
                                        order by Users_Session.datetime desc";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                Usr.AccessSecurityKey = double.Parse(Dr["AccessSecurityKey"].ToString());
                Usr.ActiveState = short.Parse(Dr["ActiveState"].ToString());
                Usr.CellPhone = Dr["CellPhone"].ToString();
                Usr.Email = Dr["Email"].ToString();
                Usr.Id = int.Parse(Dr["Id"].ToString());
                Usr.LastLogin = Dr["datetime"].ToString();
                Usr.MenuSecurityKey = double.Parse(Dr["MenuSecurityKey"].ToString());
                Usr.Name = Dr["Name"].ToString();

               // Usr.Password = Dr["Password"].ToString();
                Usr.PasswordChanged = Dr["PasswordChanged"].ToString();
                Usr.Registered = Dr["Registered"].ToString();
                Usr.UserName = Dr["UserName"].ToString();
            }


            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return Usr;
        }
        public static string GetLastLogin(string UserID)
        {
            string RetVal = "";

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @" SELECT top(1)  datetime from  Users_Session where UserId=" + UserID + "  order by datetime desc";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            try
            {
                RetVal = sqlCommand.ExecuteScalar().ToString();
            }
            catch
            {
                RetVal = "Never";
            }

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return RetVal;
        }
        public static void ResetPassWord(int userId)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update Users set Password=N'123456' Where id=" + userId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());


            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

        }
        public static void UpdateUser(BO.Users usr)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update Users set Name=N'" + usr.Name + "' , ActiveState=" + usr.ActiveState + "   Where id=" + usr.Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());


            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
        public static void Insert(string userName, string name)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @" insert into users (Username,Name) values(N'" + userName + "',N'" + name + "') ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());


            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
        public static void updateUserpassword(int userId,string password)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update Users set Password=N'"+password+"' Where id=" + userId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());


            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

        }
    }
}
