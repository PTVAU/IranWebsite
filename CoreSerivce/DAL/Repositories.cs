using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Repositories
    {
        public static BO.Repositories Insert(BO.Repositories RepObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"insert into Repository
                                    (Created_By,Description,FilePath,Kind,Title)
                                    values
                                    (" + RepObj.Created_By + ",N'" + RepObj.Description + "'  , N'" + RepObj.FilePath + "' ," +
                                      RepObj.Kind + ", N'" + RepObj.Title + "' ) select @@IDENTITY";
            sqlCommand.CommandType = CommandType.Text;








            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            RepObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return RepObj;
        }
        public static List<BO.Repositories> SelectAll()
        {
            var RepList = new List<BO.Repositories>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select  Top(100) * from Repository where ( FilePath not like N'%.mp4%' and FilePath not like N'%.mpg%') order by id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var Rep = new BO.Repositories();
                Rep.Created = Dr["Created"].ToString();
                Rep.Created_By = int.Parse(Dr["Created_By"].ToString());

                Rep.Description = Dr["Description"].ToString();
                Rep.FilePath = Dr["FilePath"].ToString();

                Rep.Id = int.Parse(Dr["Id"].ToString());
                Rep.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                Rep.Kind = short.Parse(Dr["Kind"].ToString());

                Rep.Title = Dr["Title"].ToString();

                RepList.Add(Rep);
            }

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();



            return RepList;
        }
        public static List<BO.Repositories> Search(string Keyword)
        {
            var RepList = new List<BO.Repositories>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select  Top(100) * from Repository where (title like N'%" + Keyword + "%' or description like N'%" + Keyword + "%') and (FilePath not like N'%.mp4%' and FilePath not like N'%.mpg%' ) order by id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var Rep = new BO.Repositories();
                Rep.Created = Dr["Created"].ToString();
                Rep.Created_By = int.Parse(Dr["Created_By"].ToString());

                Rep.Description = Dr["Description"].ToString();
                Rep.FilePath = Dr["FilePath"].ToString();

                Rep.Id = int.Parse(Dr["Id"].ToString());
                Rep.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                Rep.Kind = short.Parse(Dr["Kind"].ToString());

                Rep.Title = Dr["Title"].ToString();

                RepList.Add(Rep);
            }








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();



            return RepList;
        }
        public static BO.Repositories Update(BO.Repositories RepObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update Repository
                                     set Description=N'" + RepObj.Description + "' , Title= N'" + RepObj.Title + "' , Ispublished=" + RepObj.IsPublished+ " where id=" + RepObj.Id;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();

            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            return RepObj;
        }
        public static List<BO.Repositories> SelectRepByFilePath(string FilePath)
        {
            var RepList = new List<BO.Repositories>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select  Top(10) * from Repository where FilePath=N'"+FilePath+"'";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var Rep = new BO.Repositories();
                Rep.Created = Dr["Created"].ToString();
                Rep.Created_By = int.Parse(Dr["Created_By"].ToString());

                Rep.Description = Dr["Description"].ToString();
                Rep.FilePath = Dr["FilePath"].ToString();

                Rep.Id = int.Parse(Dr["Id"].ToString());
                Rep.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                Rep.Kind = short.Parse(Dr["Kind"].ToString());

                Rep.Title = Dr["Title"].ToString();

                RepList.Add(Rep);
            }

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();



            return RepList;
        }
    }
}
