using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Repository_Tags
    {
        public static BO.Repository_Tags Insert(BO.Repository_Tags TgObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"insert into Repository_Tags
                                    (Repository_Id,Tag_Id)
                                    values
                                    (" + TgObj.Repository_Id + "," + TgObj.Tag_Id + " ) select @@IDENTITY";
            sqlCommand.CommandType = CommandType.Text;


            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());


            sqlCommand.Connection.Open();
            TgObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return TgObj;
        }

        public static List<BO.Tags> SelectByRepositoryId(int RepositoryId)
        {
            var TagList = new List<BO.Tags>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT   Tags.Id,Tags.Title,tags.Published
                                        FROM            Repository_Tags INNER JOIN
                                        Tags ON Repository_Tags.Tag_Id = Tags.Id
                                         where tags.Published=1 and  Repository_Tags.Repository_Id=" + RepositoryId + " order by id desc ";
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
        public static void DeleteByRepositoryId(int RepositoryId)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"delete repository_tags where Repository_Id=" + RepositoryId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
    }
}
