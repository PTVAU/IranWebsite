using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Episodes
    {
        public static List<BO.Episodes> listEpisodes(string programId)
        {
            var episodesList = new List<BO.Episodes>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from episodes where pid="+programId+" order by number desc ";            

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var epsObj = new BO.Episodes();

                epsObj.Id = int.Parse(Dr["Id"].ToString());
                epsObj.Image = Dr["Image"].ToString();
                epsObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                epsObj.Produced = Dr["Produced"].ToString();
                epsObj.Title = Dr["Title"].ToString();
                epsObj.Video = Dr["Video"].ToString();
                epsObj.Created = Dr["Created"].ToString();
                epsObj.Pid = int.Parse(Dr["Pid"].ToString());
                epsObj.Published = Dr["Published"].ToString();
                epsObj.Number = int.Parse(Dr["Number"].ToString());
                epsObj.ViewCount = Dr["ViewCount"].ToString();
                epsObj.Youtube = Dr["Youtube"].ToString();

                episodesList.Add(epsObj);
            }
            // }
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            // }

            return episodesList;
        }
        public static  BO.Episodes SelectbyId(int  EpisodeId)
        {
            BO.Episodes epsObj = new BO.Episodes();


            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from episodes where id=" + EpisodeId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {


                epsObj.Id = int.Parse(Dr["Id"].ToString());
                epsObj.Image = Dr["Image"].ToString();
                epsObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                epsObj.Produced = Dr["Produced"].ToString();
                epsObj.Title = Dr["Title"].ToString();
                epsObj.Video = Dr["Video"].ToString();
                epsObj.Created = Dr["Created"].ToString();
                epsObj.Pid = int.Parse(Dr["Pid"].ToString());
                epsObj.Published = Dr["Published"].ToString();
                epsObj.Number = int.Parse(Dr["Number"].ToString());
                epsObj.Introtext = Dr["Introtext"].ToString();
                epsObj.Fulltext = Dr["Fulltext"].ToString();
                epsObj.ViewCount = Dr["ViewCount"].ToString();
                epsObj.Youtube = Dr["Youtube"].ToString();


            }
            // }
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            // }

            return epsObj;
        }
        public static BO.Episodes Insert(BO.Episodes Eps)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"INSERT INTO [dbo].[Episodes]
           ([Pid]
           ,[Title]
           ,[Number]
           ,[Video]
           ,[Image]
           ,[Created_By]
           ,[Produced]
           ,[Introtext]
           ,[Fulltext],[ispublished],[youtube])
                VALUES
                ("+Eps.Pid+@"
                 ,N'" + Eps.Title + @"'
                ," + Eps.Number + @"
                ,N'" + Eps.Video + @"'
                ,N'" + Eps.Image + @"'
                ," + Eps.Created_By + @"
                ,N'" + Eps.Produced + @"'
                ,N'" + Eps.Introtext + @"'
                ,N'" + Eps.Fulltext + @"',"+Eps.IsPublished+@",N'"+Eps.Youtube+"')";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            //}
            return Eps;
        }
        public static BO.Episodes Update(BO.Episodes Eps)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update [dbo].[Episodes] 
           set 
           [Title]=N'" + Eps.Title + @"'
           ,[Number]=" + Eps.Number + @"
           ,[Video]=N'" + Eps.Video + @"'
           ,[Image]=N'" + Eps.Image + @"'
            ,[IsPublished]=" + Eps.IsPublished + @"
           ,[Produced]=N'" + Eps.Produced + @"'
           ,[Introtext]=N'" + Eps.Introtext + @"'
           ,[Fulltext]=N'" + Eps.Fulltext + @"',[youtube]=N'"+Eps.Youtube+"' where id="+Eps.Id;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            //}
            return Eps;
        }
        public static List<BO.Episodes> listEpisodesByPid(string programId, string count, string ordering)
        {
            var episodesList = new List<BO.Episodes>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select top(" + count + ") * from episodes where pid=" + programId + " and ispublished=1  order by " + ordering;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var epsObj = new BO.Episodes();

                epsObj.Id = int.Parse(Dr["Id"].ToString());
                epsObj.Image = Dr["Image"].ToString();
                epsObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                epsObj.Produced = Dr["Produced"].ToString();
                epsObj.Title = Dr["Title"].ToString();
                epsObj.Video = Dr["Video"].ToString();
                epsObj.Created = Dr["Created"].ToString();
                epsObj.Pid = int.Parse(Dr["Pid"].ToString());
                epsObj.Published = Dr["Published"].ToString();
                epsObj.Number = int.Parse(Dr["Number"].ToString());
                epsObj.Introtext = Dr["Introtext"].ToString();
                epsObj.ViewCount = Dr["ViewCount"].ToString();
                epsObj.Youtube = Dr["Youtube"].ToString();

                episodesList.Add(epsObj);
            }
            // }
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            // }

            return episodesList;
        }
        public static BO.Episodes FrontendSelectbyId(int EpisodeId)
        {
            BO.Episodes epsObj = new BO.Episodes();


            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from episodes where id=" + EpisodeId+" and ispublished=1";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {


                epsObj.Id = int.Parse(Dr["Id"].ToString());
                epsObj.Image = Dr["Image"].ToString();
                epsObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                epsObj.Produced = Dr["Produced"].ToString();
                epsObj.Title = Dr["Title"].ToString();
                epsObj.Video = Dr["Video"].ToString();
                epsObj.Created = Dr["Created"].ToString();
                epsObj.Pid = int.Parse(Dr["Pid"].ToString());
                epsObj.Published = Dr["Published"].ToString();
                epsObj.Number = int.Parse(Dr["Number"].ToString());
                epsObj.Introtext = Dr["Introtext"].ToString();
                epsObj.Fulltext = Dr["Fulltext"].ToString();
                epsObj.ViewCount = Dr["ViewCount"].ToString();
                epsObj.Youtube = Dr["Youtube"].ToString();


            }
            // }
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            // }

            return epsObj;
        }
        public static bool UpdateViewCount(int Id)
        {
            var Success = false;

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update episodes set Viewcount=Viewcount+1  where Id=" + Id;
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
        public static List<BO.Episodes> FrontendListEpisodes(string  kinds,string count,string order,string hours)
        {
            var episodesList = new List<BO.Episodes>();
            if (string.IsNullOrEmpty(hours))
                hours = "10000";

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT     top("+count+@") *
                                        FROM  Episodes INNER JOIN
                                        Programs ON Episodes.Pid = Programs.Id
						                where Programs.Kind in (" + kinds + @") and Episodes.IsPublished=1 and Programs.IsPublished=1 and ( Episodes.Published between DATEADD(HOUR,-" + hours + @", GETDATE()) and GETDATE()) order by " + order;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var epsObj = new BO.Episodes();

                epsObj.Id = int.Parse(Dr["Id"].ToString());
                epsObj.Image = Dr["Image"].ToString();
                epsObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                epsObj.Produced = Dr["Produced"].ToString();
                epsObj.Title = Dr["Title"].ToString();
                epsObj.Video = Dr["Video"].ToString();
                epsObj.Created = Dr["Created"].ToString();
                epsObj.Pid = int.Parse(Dr["Pid"].ToString());
                epsObj.Published = Dr["Published"].ToString();
                epsObj.Number = int.Parse(Dr["Number"].ToString());
                epsObj.ViewCount = Dr["ViewCount"].ToString();
                epsObj.Youtube = Dr["Youtube"].ToString();
                epsObj.Introtext = Dr["Introtext"].ToString();

                episodesList.Add(epsObj);
            }
            // }
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
            // }

            return episodesList;
        }
    }
}