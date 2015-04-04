using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Contents_Categories
    {
        public static BO.Contents_Categories Insert(BO.Contents_Categories ContentsCatObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "[dbo].[Contents_Categories_Insert]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Contents_Id", ContentsCatObj.Contents_Id);
            sqlCommand.Parameters.AddWithValue("@Categories_Id", ContentsCatObj.Categories_Id);


            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            ContentsCatObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return ContentsCatObj;
        }
        public static void DeleteByConetentId(int Content_Id)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "delete from contents_categories where Contents_Id=" + Content_Id;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());
            
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();


            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }
        public static List<BO.Categories> SelectByConetentsId(int Content_Id)
        {
            var CategoryList = new List<BO.Categories>();

            var sqlCommand = new SqlCommand();


            sqlCommand.CommandText = @"SELECT       *
                                       FROM            Categories INNER JOIN
                                       Contents_Categories ON Categories.Id = Contents_Categories.Categories_Id
                                       where Categories.published=1 and contents_categories.contents_Id=" + Content_Id + " order by Contents_Categories.Id ";


            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var CatgObj = new BO.Categories();

                CatgObj.Id = int.Parse(Dr["Id"].ToString());
                CatgObj.Metadesc = Dr["Metadesc"].ToString();
                CatgObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                CatgObj.Published = int.Parse(Dr["Published"].ToString());
                CatgObj.Sort = int.Parse(Dr["Sort"].ToString());
                CatgObj.Title = Dr["Title"].ToString();

                CategoryList.Add(CatgObj);
            }








            sqlCommand.Connection.Close();
            sqlCommand.Dispose();



            return CategoryList;
        }
        public static BO.Contents_Categories UpdatePriority(BO.Contents_Categories ContentsCatObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "Update  Contents_Categories set priority=" + ContentsCatObj.Priority + " where Contents_Id=" + ContentsCatObj.Contents_Id + " and Categories_Id=" + ContentsCatObj.Categories_Id;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());
            
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return ContentsCatObj;
        }
        public static void DeleteById(int Contents_Id,int Categories_Id)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "delete from contents_categories where Contents_Id=" + Contents_Id + " and Categories_Id=" + Categories_Id;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();


            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
        }


    }
}
