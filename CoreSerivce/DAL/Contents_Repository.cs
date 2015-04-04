using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Contents_Repository
    {
        public static BO.Contents_Repository Insert(BO.Contents_Repository ContentsRepObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"insert into Contents_Repository
                                    (Contents_Id,IsPublished,Priority,Repository_Id)
                                    values
                                    (" + ContentsRepObj.Contents_Id + "," + ContentsRepObj.IsPublished + "," +
                                    ContentsRepObj.Priority + "," + ContentsRepObj.Repository_Id + ") select @@IDENTITY";
            sqlCommand.CommandType = CommandType.Text;


            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            ContentsRepObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());


            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return ContentsRepObj;
        }
        public static List<BO.Repositories> SelectByConetentsId(int Content_Id)
        {
            var RepList = new List<BO.Repositories>();

            var sqlCommand = new SqlCommand();


            sqlCommand.CommandText = @"SELECT        *,Contents_Repository.Repository_Id as RID
                                        FROM            Contents_Repository INNER JOIN
                                        Repository ON Contents_Repository.Repository_Id = Repository.Id  where Contents_Repository.Contents_Id=" + Content_Id + " order by Priority";


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

                Rep.Id = int.Parse(Dr["RID"].ToString());
                Rep.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                Rep.Kind = short.Parse(Dr["Kind"].ToString());

                Rep.Title = Dr["Title"].ToString();
                RepList.Add(Rep);
            }








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();



            return RepList;
        }

        public static void DeleteByConetentsId(int Content_Id)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Delete  Contents_Repository where  Contents_Repository.Contents_Id=" + Content_Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }
}
