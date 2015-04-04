using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Contents_Tags
    {
        public static BO.Contents_Tags Insert(BO.Contents_Tags ContentsTagObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "[dbo].[Contents_Tags_Insert]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Contents_Id", ContentsTagObj.Content_Id);
            sqlCommand.Parameters.AddWithValue("@Tag_Id", ContentsTagObj.Tag_Id);


            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            ContentsTagObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return ContentsTagObj;
        }
        public static void DeleteByConetntId(int Content_Id)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "delete  contents_tags where Content_Id=" + Content_Id;
            sqlCommand.CommandType = CommandType.Text;




            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
        public static List<BO.Tags> SelectByConetntsId(int Content_Id)
        {
            var TagList = new List<BO.Tags>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT  Tags.Id, Tags.Title,Tags.Published  FROM    Contents_Tags INNER JOIN   Tags ON Contents_Tags.Tag_Id = Tags.Id  where Contents_Tags.content_Id=" + Content_Id + " and Tags.Published=1  order by Contents_Tags.id ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());


            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var Tg = new BO.Tags();

                Tg.name = Dr["Title"].ToString();
                Tg.id = int.Parse(Dr["Id"].ToString());
                Tg.Published = short.Parse(Dr["Published"].ToString());

                TagList.Add(Tg);
            }

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            return TagList;
        }
    }
}
