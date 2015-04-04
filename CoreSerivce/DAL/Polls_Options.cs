using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace CoreSerivce.DAL
{
    public class Polls_Options
    {
        public static BO.Polls_Options Insert(BO.Polls_Options PollsOpObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "[dbo].[Polls_Options_Insert]";
            sqlCommand.CommandType = CommandType.StoredProcedure;


            sqlCommand.Parameters.AddWithValue("@Created_By", PollsOpObj.Created_By);
            sqlCommand.Parameters.AddWithValue("@Pid", PollsOpObj.Pid);
            sqlCommand.Parameters.AddWithValue("@Priority", PollsOpObj.Priority);
            sqlCommand.Parameters.AddWithValue("@Title", PollsOpObj.Title);

            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                PollsOpObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }
            return PollsOpObj;
        }

        public static List<BO.Polls_Options> SelectByPid(int Pid)
        {
            var PollsOptionsList = new List<BO.Polls_Options>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Polls_Options  where pid=" + Pid + " order by priority";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var PlOPObj = new BO.Polls_Options();

                    PlOPObj.SelectedCount = int.Parse(Dr["SelectedCount"].ToString());
                    PlOPObj.Title = Dr["Title"].ToString();
                    PlOPObj.Priority = short.Parse(Dr["Priority"].ToString());
                    PlOPObj.Pid = Pid;
                    PlOPObj.Id = int.Parse(Dr["Id"].ToString());
                    PlOPObj.Created = Dr["Created"].ToString();
                    PlOPObj.Created_By = int.Parse(Dr["Created_By"].ToString());

                    PollsOptionsList.Add(PlOPObj);
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

            return PollsOptionsList;
        }

        public static BO.Polls_Options Update(BO.Polls_Options PollsOpObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update Polls_Options set Title=N'" + PollsOpObj.Title + "' , Priority=" + PollsOpObj.Priority + "  where id=" + PollsOpObj.Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();






            sqlCommand.Connection.Close();
            sqlCommand.Dispose();


            return PollsOpObj;
        }

        public static bool Delete(int ID)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"delete Polls_Options  where id=" + ID;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

            return true;
        }

        public static void UpdateCount(int optionId)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update Polls_Options set SelectedCount=SelectedCount+1  where id=" + optionId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());
            
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }
}
