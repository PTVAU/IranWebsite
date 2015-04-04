using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Polls
    {
        public static List<BO.Polls> SelectAll()
        {
            var PollsList = new List<BO.Polls>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from polls order by id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var PlObj = new BO.Polls();

                    PlObj.AllowNew = bool.Parse(Dr["AllowNew"].ToString());
                    PlObj.ShowValues = bool.Parse(Dr["ShowValues"].ToString());
                    PlObj.ShowTotal = bool.Parse(Dr["ShowTotal"].ToString());
                    PlObj.ShowResult = bool.Parse(Dr["ShowResult"].ToString());
                    PlObj.ShowComments = bool.Parse(Dr["ShowComments"].ToString());
                    PlObj.Title = Dr["Title"].ToString();
                    PlObj.Published = Dr["Published"].ToString();
                    PlObj.Published_By = short.Parse(Dr["Published_By"].ToString());
                    PlObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    PlObj.Kind = short.Parse(Dr["Kind"].ToString());
                    PlObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                    PlObj.Id = int.Parse(Dr["Id"].ToString());
                    PlObj.Description = Dr["Description"].ToString();
                    PlObj.Created = Dr["Created"].ToString();
                    PlObj.Created_By = short.Parse(Dr["Created_By"].ToString());
                    PlObj.Expired = Dr["Expired"].ToString();


                    PollsList.Add(PlObj);
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

            return PollsList;
        }

        public static BO.Polls Insert(BO.Polls PollsObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "Polls_Insert";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Title", PollsObj.Title);
            sqlCommand.Parameters.AddWithValue("@Created_By", PollsObj.Created_By);
            sqlCommand.Parameters.AddWithValue("@AllowNew", PollsObj.AllowNew);
            sqlCommand.Parameters.AddWithValue("@ShowResult", PollsObj.ShowResult);
            sqlCommand.Parameters.AddWithValue("@ShowValues", PollsObj.ShowValues);
            sqlCommand.Parameters.AddWithValue("@ShowTotal", PollsObj.ShowTotal);
            sqlCommand.Parameters.AddWithValue("@Expired", PollsObj.Expired);
            sqlCommand.Parameters.AddWithValue("@Description", PollsObj.Description);
            sqlCommand.Parameters.AddWithValue("@IsPublished", PollsObj.IsPublished);


            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                PollsObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }
            return PollsObj;
        }

        public static BO.Polls SelectById(int Id)
        {
            var PlObj = new BO.Polls();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from polls where id=" + Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    PlObj.AllowNew = bool.Parse(Dr["AllowNew"].ToString());
                    PlObj.ShowValues = bool.Parse(Dr["ShowValues"].ToString());
                    PlObj.ShowTotal = bool.Parse(Dr["ShowTotal"].ToString());
                    PlObj.ShowResult = bool.Parse(Dr["ShowResult"].ToString());
                    PlObj.ShowComments = bool.Parse(Dr["ShowComments"].ToString());
                    PlObj.Title = Dr["Title"].ToString();
                    PlObj.Published = Dr["Published"].ToString();
                    PlObj.Published_By = short.Parse(Dr["Published_By"].ToString());
                    PlObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    PlObj.Kind = short.Parse(Dr["Kind"].ToString());
                    PlObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                    PlObj.Id = int.Parse(Dr["Id"].ToString());
                    PlObj.Description = Dr["Description"].ToString();
                    PlObj.Created = Dr["Created"].ToString();
                    PlObj.Created_By = short.Parse(Dr["Created_By"].ToString());
                    PlObj.Expired = Dr["Expired"].ToString();
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

            return PlObj;
        }
        public static BO.Polls SelectActive()
        {
            var PlObj = new BO.Polls();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from polls where IsPublished=1 and Expired >= getdate()";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    PlObj.AllowNew = bool.Parse(Dr["AllowNew"].ToString());
                    PlObj.ShowValues = bool.Parse(Dr["ShowValues"].ToString());
                    PlObj.ShowTotal = bool.Parse(Dr["ShowTotal"].ToString());
                    PlObj.ShowResult = bool.Parse(Dr["ShowResult"].ToString());
                    PlObj.ShowComments = bool.Parse(Dr["ShowComments"].ToString());
                    PlObj.Title = Dr["Title"].ToString();
                    PlObj.Published = Dr["Published"].ToString();
                    PlObj.Published_By = short.Parse(Dr["Published_By"].ToString());
                    PlObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    PlObj.Kind = short.Parse(Dr["Kind"].ToString());
                    PlObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                    PlObj.Id = int.Parse(Dr["Id"].ToString());
                    PlObj.Description = Dr["Description"].ToString();
                    PlObj.Created = Dr["Created"].ToString();
                    PlObj.Created_By = short.Parse(Dr["Created_By"].ToString());
                    PlObj.Expired = Dr["Expired"].ToString();
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

            return PlObj;
        }

        public static BO.Polls Update(BO.Polls PollsObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update  polls set " +
                                     "Title=N'" + PollsObj.Title + "', " +
                                     "ShowValues=" + Convert.ToInt32(PollsObj.ShowValues) + ", " +
                                     "AllowNew=" + Convert.ToInt32(PollsObj.AllowNew) + ", " +
                                      "ShowTotal=" + Convert.ToInt32(PollsObj.ShowTotal) + ", " +
                                       "ShowResult=" + Convert.ToInt32(PollsObj.ShowResult) + ", " +
                                        "Published_By=" + PollsObj.Published_By + ", " +
                                         "Published='" + PollsObj.Published + "', " +
                                          "IsPublished=" + PollsObj.IsPublished + ", " +
                                          "Expired='" + PollsObj.Expired + "', " +
                                          "Description=N'" + PollsObj.Description + "' " +
                                          "where id=" + PollsObj.Id;
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

            return PollsObj;
        }
    }
}
