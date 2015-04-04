using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Tags
    {
        public static List<BO.Tags> SelectAll()
        {
            var TagList = new List<BO.Tags>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from tags  order by id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Tg = new BO.Tags();
                    Tg.Created = Dr["Created"].ToString();
                    Tg.Modified = Dr["Modified"].ToString();

                    Tg.Created_By = int.Parse(Dr["Created_By"].ToString());
                    Tg.Modified_By = int.Parse(Dr["Modified_By"].ToString());

                    Tg.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    Tg.id = int.Parse(Dr["id"].ToString());
                    Tg.name = Dr["Title"].ToString();

                    Tg.Published = short.Parse(Dr["Published"].ToString());

                    TagList.Add(Tg);
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


            return TagList;
        }
        public static BO.Tags Insert(BO.Tags TagObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "Tags_Insert";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Created_By", TagObj.Created_By);
            sqlCommand.Parameters.AddWithValue("@Modified_By", TagObj.Modified_By);
            sqlCommand.Parameters.AddWithValue("@Parent_Id", TagObj.Parent_Id);
            sqlCommand.Parameters.AddWithValue("@Published", TagObj.Published);
            sqlCommand.Parameters.AddWithValue("@Title", TagObj.name);

            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            TagObj.id = int.Parse(sqlCommand.ExecuteScalar().ToString());








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return TagObj;
        }
        public static BO.Tags Update(BO.Tags TagObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Update  tags  set  Title=N'" + TagObj.name + "' , Published=" + TagObj.Published + "  where id=" + TagObj.id;
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

            return TagObj;
        }
        public static List<BO.Tags> SelectSearch(string SearchText)
        {
            var TagList = new List<BO.Tags>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from tags where (title like N'" + SearchText + "%') or (title like N'% " + SearchText + "%') order by title ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Tg = new BO.Tags();
                    Tg.Created = Dr["Created"].ToString();
                    Tg.Modified = Dr["Modified"].ToString();

                    Tg.Created_By = int.Parse(Dr["Created_By"].ToString());
                    Tg.Modified_By = int.Parse(Dr["Modified_By"].ToString());

                    Tg.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    Tg.id = int.Parse(Dr["id"].ToString());
                    Tg.name = Dr["Title"].ToString();

                    Tg.Published = short.Parse(Dr["Published"].ToString());

                    TagList.Add(Tg);
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


            return TagList;
        }
    }
}
