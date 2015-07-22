using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Contents
    {
        public static BO.Contents Insert(BO.Contents ContentObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "Contents_Insert";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Title", ContentObj.Title.Replace("'", "\'"));
            sqlCommand.Parameters.AddWithValue("@Created_By", ContentObj.Created_By);
            sqlCommand.Parameters.AddWithValue("@Fulltext", ContentObj.Fulltext.Replace("'", "\'"));
            sqlCommand.Parameters.AddWithValue("@Introtext", ContentObj.Introtext.Replace("'", "\'"));
            sqlCommand.Parameters.AddWithValue("@Metadesc", ContentObj.Metadesc.Replace("'", "\'"));
            sqlCommand.Parameters.AddWithValue("@Alias", ContentObj.Alias.Replace("'", "\'"));
            sqlCommand.Parameters.AddWithValue("@Owner", ContentObj.Owner);
            sqlCommand.Parameters.AddWithValue("@ShortTitle", ContentObj.ShortTitle.Replace("'", "\'"));

            sqlCommand.Parameters.AddWithValue("@Published", ContentObj.Published);
            sqlCommand.Parameters.AddWithValue("@IsPublished", ContentObj.IsPublished);
            sqlCommand.Parameters.AddWithValue("@Published_By", ContentObj.Published_By);
            sqlCommand.Parameters.AddWithValue("@State", ContentObj.State);
            sqlCommand.Parameters.AddWithValue("@Youtube", ContentObj.Youtube);
            sqlCommand.Parameters.AddWithValue("@ItemPriority", ContentObj.ItemPriority);


            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            ContentObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());


            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return ContentObj;
        }
        public static List<BO.Contents> SelectByState(int StateId, string Published, string Owner)
        {
            var Condition = "   ";
            if (Published == "1" || Published == "0")
            {
                Condition = " and  IsPublished=" + Published + " ";
            }
            if (Owner != "0")
            {
                Condition = " and  Owner=" + Owner + " ";
            }


            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select top(200) * from contents where State=" + StateId + Condition +"   order by Id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ContentObj = new BO.Contents();

                    ContentObj.Alias = Dr["Alias"].ToString();
                    ContentObj.Created = Dr["Created"].ToString();
                    ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                    ContentObj.Fulltext = Dr["Fulltext"].ToString();
                    ContentObj.Id = int.Parse(Dr["Id"].ToString());
                    ContentObj.Introtext = Dr["Introtext"].ToString();
                    ContentObj.Metadesc = Dr["Metadesc"].ToString();
                    ContentObj.Modified = Dr["Modified"].ToString();
                    ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                    ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                    ContentObj.Published = Dr["Published"].ToString();
                    ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                    ContentObj.State = short.Parse(Dr["State"].ToString());
                    ContentObj.Title = Dr["Title"].ToString();
                    ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                    ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                    ContentObj.Youtube = Dr["Youtube"].ToString();
                    ContentObj.ItemPriority = short.Parse(Dr["ItemPriority"].ToString());

                    ContentList.Add(ContentObj);
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

            return ContentList;
        }
        public static BO.Contents SelectById(int Id)
        {
            var ContentObj = new BO.Contents();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from contents where Id=" + Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    ContentObj.Alias = Dr["Alias"].ToString();
                    ContentObj.Created = Dr["Created"].ToString();
                    ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                    ContentObj.Fulltext = Dr["Fulltext"].ToString();
                    ContentObj.Id = int.Parse(Dr["Id"].ToString());
                    ContentObj.Introtext = Dr["Introtext"].ToString();
                    ContentObj.Metadesc = Dr["Metadesc"].ToString();
                    ContentObj.Modified = Dr["Modified"].ToString();
                    ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                    ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                    ContentObj.Published = Dr["Published"].ToString();
                    ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                    ContentObj.State = short.Parse(Dr["State"].ToString());
                    ContentObj.Title = Dr["Title"].ToString();
                    ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                    ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                    ContentObj.Youtube = Dr["Youtube"].ToString();
                    ContentObj.ItemPriority = short.Parse(Dr["ItemPriority"].ToString());

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

            return ContentObj;
        }
        public static bool UpdateOwner(int Id, int UserId)
        {
            var Success = false;

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update contents set owner=" + UserId + " where Id=" + Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

            return Success;
        }
        public static bool UpdateState(int Id, int StateId)
        {
            var Success = false;

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update contents set state=" + StateId + " where Id=" + Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

            return Success;
        }
        public static List<BO.Contents> SelectByStatePublishedCategory(string Published, string Category, string Order)
        {
            var Condition = "   ";

            if (Published == "1" || Published == "0")
            {
                Condition = " and  Contents.IsPublished=" + Published + " ";
            }

            var OrderState = "   ";
            if (Order == "List")
            {
                OrderState = " order by Contents.id desc ";
            }


            if (Order == "Ordering")
            {
                OrderState = " order by Contents_Categories.Priority, Contents.Published desc ";
            }


            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT   top(200)   *,Contents.Id as Cid
                                        FROM            Contents_Categories inner  JOIN
                                        Contents ON Contents.Id = Contents_Categories.Contents_Id
						                where Contents_Categories.Categories_Id=" + Category + " and  Contents.State=0  " + Condition + " " + OrderState;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ContentObj = new BO.Contents();

                    ContentObj.Alias = Dr["Alias"].ToString();
                    ContentObj.Created = Dr["Created"].ToString();
                    ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                    ContentObj.Fulltext = Dr["Fulltext"].ToString();
                    ContentObj.Id = int.Parse(Dr["Cid"].ToString());
                    ContentObj.Introtext = Dr["Introtext"].ToString();
                    ContentObj.Metadesc = Dr["Metadesc"].ToString();
                    ContentObj.Modified = Dr["Modified"].ToString();
                    ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                    ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                    ContentObj.Published = Dr["Published"].ToString();
                    ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                    ContentObj.State = short.Parse(Dr["State"].ToString());
                    ContentObj.Title = Dr["Title"].ToString();
                    ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                    ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                    ContentObj.Youtube = Dr["Youtube"].ToString();
                    ContentObj.ItemPriority = short.Parse(Dr["ItemPriority"].ToString());

                    ContentList.Add(ContentObj);
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

            return ContentList;
        }
        public static List<BO.Contents> SelectSearch(string SearchKey)
        {
            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from contents where ShortTitle like N'%" + SearchKey + "%' or Title like N'%" + SearchKey + "%' or  Alias like N'%" + SearchKey + "%' or Introtext like N'%" + SearchKey + "%' or Id=" + SearchKey + " order by Id desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ContentObj = new BO.Contents();

                    ContentObj.Alias = Dr["Alias"].ToString();
                    ContentObj.Created = Dr["Created"].ToString();
                    ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                    ContentObj.Fulltext = Dr["Fulltext"].ToString();
                    ContentObj.Id = int.Parse(Dr["Id"].ToString());
                    ContentObj.Introtext = Dr["Introtext"].ToString();
                    ContentObj.Metadesc = Dr["Metadesc"].ToString();
                    ContentObj.Modified = Dr["Modified"].ToString();
                    ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                    ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                    ContentObj.Published = Dr["Published"].ToString();
                    ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                    ContentObj.State = short.Parse(Dr["State"].ToString());
                    ContentObj.Title = Dr["Title"].ToString();
                    ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                    ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                    ContentObj.Youtube = Dr["Youtube"].ToString();
                    ContentObj.ItemPriority = short.Parse(Dr["ItemPriority"].ToString());

                    ContentList.Add(ContentObj);
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

            return ContentList;
        }
        public static void Update(BO.Contents ContentObj)
        {
            //ContentObj.Alias
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @" update contents set Alias=N'" + ContentObj.Alias+
                                    "',Fulltext=N'" + ContentObj.Fulltext.Replace("'", "''") + "'," +
                                    " Introtext=N'" + ContentObj.Introtext.Replace("'", "''") + "'," +
                                    " Metadesc=N'" + ContentObj.Metadesc.Replace("'", "''") + "'," +
                                    " Modified=getdate()," +
                                    " Modified_By=" + ContentObj.Modified_By + "," +
                                    " ShortTitle=N'" + ContentObj.ShortTitle.Replace("'", "''") + "'," +
                                    " Title=N'" + ContentObj.Title.Replace("'","''") + "'," +
                                    " IsPublished=" + ContentObj.IsPublished + "," +
                                    " Published= CONVERT(DATETIME, '" + ContentObj.Published + "'),"+
                                    " youtube=N'" + ContentObj.Youtube + "' , state="+ContentObj.State +
                                    " , ItemPriority="+ContentObj.ItemPriority+
                                    " where Id=" + ContentObj.Id;


            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

        }
        public static List<BO.Contents> frontendSelect(string Category, string count, string ordering)
        {
            var ContentList = new List<BO.Contents>();
            // Contents_Categories.Priority, Contents.Published desc
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT distinct top(" + count + @") Contents.*,
                                        Contents_Categories.Priority
                                        FROM  Contents_Categories right  JOIN 
                                        Contents ON Contents.Id = Contents_Categories.Contents_Id
						                where Contents_Categories.Categories_Id in (" + Category + @") and  Contents.State=0 and  Contents.IsPublished=1 group by contents.Id,Contents.ShortTitle,Contents.Title,Contents.Alias,Contents.Introtext,
										Contents.Fulltext,Contents.State,Contents.Created,Contents.Created_By,
										Contents.Modified,Contents.Modified_By,Contents.Owner,
										Contents.Published,Contents.Published_By,Contents.IsPublished,
										Contents.Metadesc,contents.Viewcount,contents.youtube,contents.ItemPriority,Contents_Categories.Priority
									     order by " + ordering;


            //object result = Utilities.CacheContoller.cacheControl(sqlCommand.CommandText, null, "R", 0);
            //if (result == null)
            //{

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

                try
                {
                    sqlCommand.Connection.Open();
                    var Dr = sqlCommand.ExecuteReader();
                    while (Dr.Read())
                    {
                        var ContentObj = new BO.Contents();

                        ContentObj.Alias = Dr["Alias"].ToString();
                        ContentObj.Created = Dr["Created"].ToString();
                        ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                        ContentObj.Fulltext = Dr["Fulltext"].ToString();
                        ContentObj.Id = int.Parse(Dr["id"].ToString());
                        ContentObj.Introtext = Dr["Introtext"].ToString();
                        ContentObj.Metadesc = Dr["Metadesc"].ToString();
                        ContentObj.Modified = Dr["Modified"].ToString();
                        ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                        ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                        ContentObj.Published = Dr["Published"].ToString();
                        ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                        ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                        ContentObj.State = short.Parse(Dr["State"].ToString());
                        ContentObj.Title = Dr["Title"].ToString();
                        ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                        ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                       // ContentObj.Youtube = Dr["Youtube"].ToString();

                        ContentList.Add(ContentObj);
                    }

                   // result = Utilities.CacheContoller.cacheControl(sqlCommand.CommandText, ContentList, "W", 60);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    sqlCommand.Connection.Close();
                    sqlCommand.Dispose();
                }
            //}
            //else
            //{
            //    ContentList = (List<BO.Contents>)result;
            //}
            return ContentList;
        }
        public static List<BO.Contents> frontendSelectLatest(string count)
        {

            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
          //  sqlCommand.CommandText = @"SELECT top(" + count + ") * from contents where IsPublished=1  order by published desc";
            sqlCommand.CommandText = @"SELECT distinct  top(" + count + @")    Contents.*
                                        FROM    Contents_Categories right   JOIN
                                        Contents ON Contents.Id = Contents_Categories.Contents_Id
						                where Contents_Categories.Categories_Id not in (113,102,129,125) 
										and  Contents.State=0 and  Contents.IsPublished=1 										
										group by contents.Id,Contents.ShortTitle,Contents.Title,Contents.Alias,Contents.Introtext,
										Contents.Fulltext,Contents.State,Contents.Created,Contents.Created_By,
										Contents.Modified,Contents.Modified_By,Contents.Owner,
										Contents.Published,Contents.Published_By,Contents.IsPublished,
										Contents.Metadesc,contents.Viewcount,contents.youtube,contents.ItemPriority
										order by Contents.Published desc";

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var ContentObj = new BO.Contents();

                ContentObj.Alias = Dr["Alias"].ToString();
                ContentObj.Created = Dr["Created"].ToString();
                ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                ContentObj.Fulltext = Dr["Fulltext"].ToString();
                ContentObj.Id = int.Parse(Dr["id"].ToString());
                ContentObj.Introtext = Dr["Introtext"].ToString();
                ContentObj.Metadesc = Dr["Metadesc"].ToString();
                ContentObj.Modified = Dr["Modified"].ToString();
                ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                ContentObj.Published = Dr["Published"].ToString();
                ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                ContentObj.State = short.Parse(Dr["State"].ToString());
                ContentObj.Title = Dr["Title"].ToString();
                ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                //ContentObj.Youtube = Dr["Youtube"].ToString();

                ContentList.Add(ContentObj);
            }
            // }
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            //}

            return ContentList;
        }
        public static List<BO.Contents> frontendSelectByTagId(string tagId, string count)
        {

            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT    top(" + count + @") *,contents.id as cid FROM            Contents INNER JOIN
                                        Contents_Tags ON Contents.Id = Contents_Tags.Content_Id
                                        WHERE        (Contents_Tags.Tag_Id = " + tagId + ") and  Contents.ispublished=1 order by contents.published desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var ContentObj = new BO.Contents();

                ContentObj.Alias = Dr["Alias"].ToString();
                ContentObj.Created = Dr["Created"].ToString();
                ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                ContentObj.Fulltext = Dr["Fulltext"].ToString();
                ContentObj.Id = int.Parse(Dr["Cid"].ToString());
                ContentObj.Introtext = Dr["Introtext"].ToString();
                ContentObj.Metadesc = Dr["Metadesc"].ToString();
                ContentObj.Modified = Dr["Modified"].ToString();
                ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                ContentObj.Published = Dr["Published"].ToString();
                ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                ContentObj.State = short.Parse(Dr["State"].ToString());
                ContentObj.Title = Dr["Title"].ToString();
                ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
               // ContentObj.Youtube = Dr["Youtube"].ToString();

                ContentList.Add(ContentObj);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            //}

            return ContentList;
        }
        public static List<BO.Contents> frontendSelectRelatedByTag(string itemId, string count)
        {
            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"select *,contents.id as cid from Contents where Id in 
                                         (
	                                        select top " +count+@" Content_Id from Contents_Tags
			                                        where Tag_Id in 
			                                        (
				                                        select top 10 Tag_Id from Contents_Tags nolock
				                                        where Content_Id = " + itemId + @"
				                                        order by Tag_Id
			                                        )
	                                        group by Content_Id
	                                        having Content_Id <> " + itemId + @"
                                            order by count(*)
                                        )
                                    and IsPublished=1 and State=0
                                    order by Published desc";

           
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ContentObj = new BO.Contents();

                    ContentObj.Alias = Dr["Alias"].ToString();
                    ContentObj.Created = Dr["Created"].ToString();
                    ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                    ContentObj.Fulltext = Dr["Fulltext"].ToString();
                    ContentObj.Id = int.Parse(Dr["Cid"].ToString());
                    ContentObj.Introtext = Dr["Introtext"].ToString();
                    ContentObj.Metadesc = Dr["Metadesc"].ToString();
                    ContentObj.Modified = Dr["Modified"].ToString();
                    ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                    ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                    ContentObj.Published = Dr["Published"].ToString();
                    ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                    ContentObj.State = short.Parse(Dr["State"].ToString());
                    ContentObj.Title = Dr["Title"].ToString();
                    ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                    ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                   // ContentObj.Youtube = Dr["Youtube"].ToString();

                    ContentList.Add(ContentObj);
                }

            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            //}
          
            return ContentList;
        }
        public static bool UpdateViewCount(int Id)
        {
            var Success = false;

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update contents set Viewcount=Viewcount+1  where Id=" + Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Success = true;
            }
            catch (Exception ex)
            {
                Success = false;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }

            return Success;
        }
        public static List<BO.Contents> frontendSelectMostViewed(string count,string hours)
        {

            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            //  sqlCommand.CommandText = @"SELECT top(" + count + ") * from contents where IsPublished=1  order by published desc";
            sqlCommand.CommandText = @"Select top("+count+@") * from Contents
                                        where Published between DATEADD(HOUR,-"+hours+@", GETDATE()) and GETDATE()  and IsPublished=1
                                        order by Viewcount desc";

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var ContentObj = new BO.Contents();

                ContentObj.Alias = Dr["Alias"].ToString();
                ContentObj.Created = Dr["Created"].ToString();
                ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                ContentObj.Fulltext = Dr["Fulltext"].ToString();
                ContentObj.Id = int.Parse(Dr["id"].ToString());
                ContentObj.Introtext = Dr["Introtext"].ToString();
                ContentObj.Metadesc = Dr["Metadesc"].ToString();
                ContentObj.Modified = Dr["Modified"].ToString();
                ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                ContentObj.Published = Dr["Published"].ToString();
                ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                ContentObj.State = short.Parse(Dr["State"].ToString());
                ContentObj.Title = Dr["Title"].ToString();
                ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                //ContentObj.Youtube = Dr["Youtube"].ToString();

                ContentList.Add(ContentObj);
            }
            // }
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            //}

            return ContentList;
        }
        public static List<BO.Contents> frontendSearch(string Category, string count, string ordering,string SearchKey)
        {
            //Contents_Categories.Categories_Id in (" + Category + ") and 
            string[] keys = SearchKey.Split(' ');
            string Condition = "";

            foreach (string item in keys)
            {
                Condition += "(  Contents.ShortTitle like N'%" + item + "%' or Contents.Title like N'%" +
                                          item + "%' or  Contents.Alias like N'%" + item +
                                          "%' or Contents.Introtext like N'%" + item +
                                          "%' or Contents.Fulltext like N'%" + item + @"%' ) and";
            }

            var ContentList = new List<BO.Contents>();
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT  distinct top(" + count + @")    Contents.*
                                        FROM    Contents_Categories right   JOIN
                                        Contents ON Contents.Id = Contents_Categories.Contents_Id
						                where  Contents_Categories.Categories_Id not in (102) and 
                                        (Contents.State=0 and  Contents.IsPublished=1 ) and " +Condition.Remove(Condition.Length-4,4)+
										@" group by contents.Id,Contents.ShortTitle,Contents.Title,Contents.Alias,Contents.Introtext,
										Contents.Fulltext,Contents.State,Contents.Created,Contents.Created_By,
										Contents.Modified,Contents.Modified_By,Contents.Owner,
										Contents.Published,Contents.Published_By,Contents.IsPublished,
										Contents.Metadesc,contents.Viewcount,contents.youtube,contents.ItemPriority
										order by Contents.Published desc";
                                        

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ContentObj = new BO.Contents();

                    ContentObj.Alias = Dr["Alias"].ToString();
                    ContentObj.Created = Dr["Created"].ToString();
                    ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                    ContentObj.Fulltext = Dr["Fulltext"].ToString();
                    ContentObj.Id = int.Parse(Dr["id"].ToString());
                    ContentObj.Introtext = Dr["Introtext"].ToString();
                    ContentObj.Metadesc = Dr["Metadesc"].ToString();
                    ContentObj.Modified = Dr["Modified"].ToString();
                    ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                    ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                    ContentObj.Published = Dr["Published"].ToString();
                    ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                    ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                    ContentObj.State = short.Parse(Dr["State"].ToString());
                    ContentObj.Title = Dr["Title"].ToString();
                    ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                    ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());

                    ContentList.Add(ContentObj);
                }

            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            //}            
            return ContentList;
        }
        public static List<BO.Contents> frontendSelectByTag(string count,string tags)
        {
            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"select top " + count + @" *  from Contents where id in (
		                                select Content_Id from Contents_Tags where Tag_Id in(" + tags + @"))
                                        and IsPublished=1 and State=0
                                        order by Published desc";


            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var ContentObj = new BO.Contents();

                ContentObj.Alias = Dr["Alias"].ToString();
                ContentObj.Created = Dr["Created"].ToString();
                ContentObj.Created_By = int.Parse(Dr["Created_By"].ToString());
                ContentObj.Fulltext = Dr["Fulltext"].ToString();
                ContentObj.Id = int.Parse(Dr["id"].ToString());
                ContentObj.Introtext = Dr["Introtext"].ToString();
                ContentObj.Metadesc = Dr["Metadesc"].ToString();
                ContentObj.Modified = Dr["Modified"].ToString();
                ContentObj.Modified_By = int.Parse(Dr["Modified_By"].ToString());
                ContentObj.Owner = int.Parse(Dr["Owner"].ToString());
                ContentObj.Published = Dr["Published"].ToString();
                ContentObj.Published_By = int.Parse(Dr["Published_By"].ToString());
                ContentObj.ShortTitle = Dr["ShortTitle"].ToString();
                ContentObj.State = short.Parse(Dr["State"].ToString());
                ContentObj.Title = Dr["Title"].ToString();
                ContentObj.Viewcount = int.Parse(Dr["Viewcount"].ToString());
                ContentObj.IsPublished = int.Parse(Dr["IsPublished"].ToString());
                // ContentObj.Youtube = Dr["Youtube"].ToString();

                ContentList.Add(ContentObj);
            }

            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            //}

            return ContentList;
        }
    }
}
