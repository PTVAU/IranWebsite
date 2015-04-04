using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Contents_Flow
    {
        public static List<BO.Contents> Select(int UserId)
        {
            var OwnerCondition = "   ";
            if (UserId > 0)
            {
                OwnerCondition = " and  Contents.owner=" + UserId + " ";
            }
            else
            {
                OwnerCondition = " ";
            }


            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT      * 
                                     FROM            Contents INNER JOIN 
                                     Contents_Flow ON Contents.Id = Contents_Flow.Content_Id 
                                     where  User_To=" + UserId + OwnerCondition + "  and Contents.IsPublished=0  and Contents.state=0   order by Contents_Flow.SendDate desc ";
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
                    ContentObj.ItemPriority = short.Parse(Dr["ItemPriority"].ToString());

                    var ContentFlowList = new List<BO.Contents_Flow>();

                    var ContentFlowObj = new BO.Contents_Flow();
                    ContentFlowObj.SeenDate = Dr["SeenDate"].ToString();
                    ContentFlowObj.SendDate = Dr["SendDate"].ToString();
                    ContentFlowObj.User_FromId = int.Parse(Dr["User_From"].ToString());
                    ContentFlowObj.User_ToId = int.Parse(Dr["User_To"].ToString());
                    ContentFlowObj.Content_Id = ContentObj.Id;
                    ContentFlowList.Add(ContentFlowObj);


                    ContentObj.Flow = ContentFlowList;


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
        public static List<BO.Contents> SelectSent(int UserId)
        {
            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT      * 
                                     FROM            Contents INNER JOIN 
                                     Contents_Flow ON Contents.Id = Contents_Flow.Content_Id 
                                     where  User_from=" + UserId + "  order by Contents_Flow.SendDate  desc ";
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


                    var ContentFlowList = new List<BO.Contents_Flow>();

                    var ContentFlowObj = new BO.Contents_Flow();
                    ContentFlowObj.SeenDate = Dr["SeenDate"].ToString();
                    ContentFlowObj.SendDate = Dr["SendDate"].ToString();
                    ContentFlowObj.User_FromId = int.Parse(Dr["User_From"].ToString());
                    ContentFlowObj.User_ToId = int.Parse(Dr["User_To"].ToString());
                    ContentFlowObj.Content_Id = ContentObj.Id;
                    ContentFlowList.Add(ContentFlowObj);


                    ContentObj.Flow = ContentFlowList;


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
        public static List<BO.Contents_Flow> SelectByContentId(int Content_Id)
        {
            var ContentFlowList = new List<BO.Contents_Flow>();


            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT      * 
                                     FROM  Contents_Flow
                                     where  Content_Id=" + Content_Id + "  order by Contents_Flow.SendDate  ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var ContentFlowObj = new BO.Contents_Flow();
                    ContentFlowObj.SeenDate = Dr["SeenDate"].ToString();
                    ContentFlowObj.SendDate = Dr["SendDate"].ToString();
                    ContentFlowObj.User_FromId = int.Parse(Dr["User_From"].ToString());
                    ContentFlowObj.User_ToId = int.Parse(Dr["User_To"].ToString());

                    ContentFlowObj.Id = int.Parse(Dr["Id"].ToString());
                    ContentFlowObj.Version_Id = int.Parse(Dr["Version_Id"].ToString());


                    ContentFlowList.Add(ContentFlowObj);
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

            return ContentFlowList;
        }
        public static BO.Contents_Flow Insert(BO.Contents_Flow FlwObj)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"insert into Contents_flow
                                       ( Content_Id,User_From,User_To,Version_Id)
                                       values (" + FlwObj.Content_Id + "," + FlwObj.User_FromId +
                                                 "," + FlwObj.User_ToId + "," + FlwObj.Version_Id + " )  select @@IDENTITY ";
            sqlCommand.CommandType = CommandType.Text;



            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            FlwObj.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());







            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return FlwObj;
        }
        public static void UpdateSeen(int UserId, int Content_Id)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update    Contents_Flow  set seendate=getdate() where id 
                                    in (select top(1) id from Contents_Flow where Content_Id=" + Content_Id +
                                    " and seendate is null order by senddate desc )";

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
        }
        public static List<BO.Contents> SelectNotSeen(int UserId)
        {
            var OwnerCondition = "   ";
            if (UserId > 0)
            {
                OwnerCondition = " and  Contents.owner=" + UserId + " ";
            }
            else
            {
                OwnerCondition = " ";
            }


            var ContentList = new List<BO.Contents>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT      * 
                                     FROM            Contents INNER JOIN 
                                     Contents_Flow ON Contents.Id = Contents_Flow.Content_Id 
                                     where  User_To=" + UserId + OwnerCondition + "  and Contents.IsPublished=0 and Contents.state <> -200 and   Contents_Flow.seendate is null   order by Contents_Flow.SendDate desc ";
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

                    var ContentFlowList = new List<BO.Contents_Flow>();

                    var ContentFlowObj = new BO.Contents_Flow();
                    ContentFlowObj.SeenDate = Dr["SeenDate"].ToString();
                    ContentFlowObj.SendDate = Dr["SendDate"].ToString();
                    ContentFlowObj.User_FromId = int.Parse(Dr["User_From"].ToString());
                    ContentFlowObj.User_ToId = int.Parse(Dr["User_To"].ToString());
                    ContentFlowObj.Content_Id = ContentObj.Id;
                    ContentFlowList.Add(ContentFlowObj);


                    ContentObj.Flow = ContentFlowList;


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
           // }

            return ContentList;
        }
    }
}
