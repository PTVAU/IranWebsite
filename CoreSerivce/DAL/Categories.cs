using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace CoreSerivce.DAL
{
    public class Categories
    {
        public static List<BO.Categories> SelectAll()
        {
            var CategoryList = new List<BO.Categories>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @" Select top(500) * from categories where Published <> -1  order by sort ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
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
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

            return CategoryList;
        }
        public static List<BO.Categories> subCategorySelectByPid(string pid)
        {
            var CategoryList = new List<BO.Categories>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @" Select top(500) * from categories where Published <> -1 and parent_id="+pid+" order by sort ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
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
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

            return CategoryList;
        }
        public static BO.Categories selectById(string id)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @" Select  * from categories where id="+id+"  order by sort ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());
          
            var CatgObj = new BO.Categories();
            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                 

                    CatgObj.Id = int.Parse(Dr["Id"].ToString());
                    CatgObj.Metadesc = Dr["Metadesc"].ToString();
                    CatgObj.Parent_Id = int.Parse(Dr["Parent_Id"].ToString());
                    CatgObj.Published = int.Parse(Dr["Published"].ToString());
                    CatgObj.Sort = int.Parse(Dr["Sort"].ToString());
                    CatgObj.Title = Dr["Title"].ToString();

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

            return CatgObj;
        }
    }
}
