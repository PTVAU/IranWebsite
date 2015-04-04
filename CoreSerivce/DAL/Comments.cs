using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Comments
    {
        public static List<BO.Comments> SelectAll()
        {
            var CommentsList = new List<BO.Comments>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select top(500) * from Comments where Published <> -1   order by id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ComntObj = new BO.Comments();

                    ComntObj.Content_Id = int.Parse(Dr["Content_Id"].ToString());
                    ComntObj.Datetime_Insert = Dr["Datetime_Insert"].ToString();
                    ComntObj.Datetime_Published = Dr["Datetime_Published"].ToString();
                    ComntObj.Email = Dr["Email"].ToString();
                    ComntObj.Id = int.Parse(Dr["Id"].ToString());
                    ComntObj.IP = Dr["IP"].ToString();
                    ComntObj.Kind = short.Parse(Dr["Kind"].ToString());
                    ComntObj.Name = Dr["Name"].ToString();
                    ComntObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    ComntObj.Published = short.Parse(Dr["Published"].ToString());
                    ComntObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ComntObj.Subscribe = short.Parse(Dr["Subscribe"].ToString());
                    ComntObj.Text = Dr["Text"].ToString();
                    ComntObj.User_Id = int.Parse(Dr["User_Id"].ToString());
                    ComntObj.Vote_Down = int.Parse(Dr["Vote_Down"].ToString());
                    ComntObj.Vote_Up = int.Parse(Dr["Vote_Up"].ToString());

                    CommentsList.Add(ComntObj);
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

            return CommentsList;
        }

        public static BO.Comments Update(BO.Comments CommenetObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update  Comments set " +
                                     "Text=N'" + CommenetObj.Text + "', " +
                                        "Published_By=" + CommenetObj.Published_By + ", " +
                                         "Published=" + CommenetObj.Published + ", " +
                                         "Datetime_Published= getdate(), " +
                                          "Name=N'" + CommenetObj.Name + "' " +
                                          "where id=" + CommenetObj.Id;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

            return CommenetObj;
        }
        public static List<BO.Comments> SelectSearch(string SearchText)
        {
            var CommentsList = new List<BO.Comments>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select TOP(500) * from Comments where Text like N'%" + SearchText + "%' Or Name like N'%" + SearchText + "%' and  Published <> -1   order by id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ComntObj = new BO.Comments();

                    ComntObj.Content_Id = int.Parse(Dr["Content_Id"].ToString());
                    ComntObj.Datetime_Insert = Dr["Datetime_Insert"].ToString();
                    ComntObj.Datetime_Published = Dr["Datetime_Published"].ToString();
                    ComntObj.Email = Dr["Email"].ToString();
                    ComntObj.Id = int.Parse(Dr["Id"].ToString());
                    ComntObj.IP = Dr["IP"].ToString();
                    ComntObj.Kind = short.Parse(Dr["Kind"].ToString());
                    ComntObj.Name = Dr["Name"].ToString();
                    ComntObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    ComntObj.Published = short.Parse(Dr["Published"].ToString());
                    ComntObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ComntObj.Subscribe = short.Parse(Dr["Subscribe"].ToString());
                    ComntObj.Text = Dr["Text"].ToString();
                    ComntObj.User_Id = int.Parse(Dr["User_Id"].ToString());
                    ComntObj.Vote_Down = int.Parse(Dr["Vote_Down"].ToString());
                    ComntObj.Vote_Up = int.Parse(Dr["Vote_Up"].ToString());

                    CommentsList.Add(ComntObj);
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

            return CommentsList;
        }
        public static BO.Comments Insert(BO.Comments CommenetObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"INSERT INTO [dbo].[comments]
               ([Parent_Id]
               ,[Content_Id]
               ,[Kind]
               ,[Name]
               ,[Email]
               ,[Text]
               ,[User_Id]
               ,[IP])
                VALUES
                (" + CommenetObj.Parent_Id + @"
                 ," + CommenetObj.Content_Id + @"
                ," + CommenetObj.Kind + @"
                ,N'" + CommenetObj.Name + @"'
                ,N'" + CommenetObj.Email + @"'
                ,N'" + CommenetObj.Text + @"'
                ," + CommenetObj.User_Id + @"
                ,N'" + CommenetObj.IP + @"')";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            return CommenetObj;
        }
        public static List<BO.Comments> SelectByContentId(string contentId, string count, string lastId)
        {
            var CommentsList = new List<BO.Comments>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select top(" + count + ") * from Comments where Published=1 and id>=" +
                lastId + " and Content_Id=" + contentId + " and Parent_Id=0   order by id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ComntObj = new BO.Comments();

                    ComntObj.Content_Id = int.Parse(Dr["Content_Id"].ToString());
                    ComntObj.Datetime_Insert = Dr["Datetime_Insert"].ToString();
                    ComntObj.Datetime_Published = Dr["Datetime_Published"].ToString();
                    ComntObj.Email = Dr["Email"].ToString();
                    ComntObj.Id = int.Parse(Dr["Id"].ToString());
                    ComntObj.IP = Dr["IP"].ToString();
                    ComntObj.Kind = short.Parse(Dr["Kind"].ToString());
                    ComntObj.Name = Dr["Name"].ToString();
                    ComntObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    ComntObj.Published = short.Parse(Dr["Published"].ToString());
                    ComntObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ComntObj.Subscribe = short.Parse(Dr["Subscribe"].ToString());
                    ComntObj.Text = Dr["Text"].ToString();
                    ComntObj.User_Id = int.Parse(Dr["User_Id"].ToString());
                    ComntObj.Vote_Down = int.Parse(Dr["Vote_Down"].ToString());
                    ComntObj.Vote_Up = int.Parse(Dr["Vote_Up"].ToString());

                    CommentsList.Add(ComntObj);
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

            return CommentsList;
        }
        public static List<BO.Comments> SelectByReply(string Parent_Id)
        {
            var CommentsList = new List<BO.Comments>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select top(1000) * from Comments where Published=1 and  Parent_Id=" + Parent_Id + "  order by id";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ComntObj = new BO.Comments();

                    ComntObj.Content_Id = int.Parse(Dr["Content_Id"].ToString());
                    ComntObj.Datetime_Insert = Dr["Datetime_Insert"].ToString();
                    ComntObj.Datetime_Published = Dr["Datetime_Published"].ToString();
                    ComntObj.Email = Dr["Email"].ToString();
                    ComntObj.Id = int.Parse(Dr["Id"].ToString());
                    ComntObj.IP = Dr["IP"].ToString();
                    ComntObj.Kind = short.Parse(Dr["Kind"].ToString());
                    ComntObj.Name = Dr["Name"].ToString();
                    ComntObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    ComntObj.Published = short.Parse(Dr["Published"].ToString());
                    ComntObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ComntObj.Subscribe = short.Parse(Dr["Subscribe"].ToString());
                    ComntObj.Text = Dr["Text"].ToString();
                    ComntObj.User_Id = int.Parse(Dr["User_Id"].ToString());
                    ComntObj.Vote_Down = int.Parse(Dr["Vote_Down"].ToString());
                    ComntObj.Vote_Up = int.Parse(Dr["Vote_Up"].ToString());

                    CommentsList.Add(ComntObj);
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

            return CommentsList;
        }
        public static void VoteUp(string CommentId)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update  Comments set vote_up=vote_up+1  where id=" + CommentId;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

        }
        public static void VoteDown(string CommentId)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update  Comments set vote_down=vote_down+1  where id=" + CommentId;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

        }
        public static BO.Comments SelectById(string commentId)
        {
            BO.Comments ComntObj = new BO.Comments();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Comments where id=" + commentId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {


                    ComntObj.Content_Id = int.Parse(Dr["Content_Id"].ToString());
                    ComntObj.Datetime_Insert = Dr["Datetime_Insert"].ToString();
                    ComntObj.Datetime_Published = Dr["Datetime_Published"].ToString();
                    ComntObj.Email = Dr["Email"].ToString();
                    ComntObj.Id = int.Parse(Dr["Id"].ToString());
                    ComntObj.IP = Dr["IP"].ToString();
                    ComntObj.Kind = short.Parse(Dr["Kind"].ToString());
                    ComntObj.Name = Dr["Name"].ToString();
                    ComntObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    ComntObj.Published = short.Parse(Dr["Published"].ToString());
                    ComntObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ComntObj.Subscribe = short.Parse(Dr["Subscribe"].ToString());
                    ComntObj.Text = Dr["Text"].ToString();
                    ComntObj.User_Id = int.Parse(Dr["User_Id"].ToString());
                    ComntObj.Vote_Down = int.Parse(Dr["Vote_Down"].ToString());
                    ComntObj.Vote_Up = int.Parse(Dr["Vote_Up"].ToString());

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

            return ComntObj;
        }
    }
}
