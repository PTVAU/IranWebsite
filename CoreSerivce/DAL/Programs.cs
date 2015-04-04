using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
namespace CoreSerivce.DAL
{
    public class Programs
    {
        public static List<BO.Programs> listPrograms(string kind,string status)
        {
            string Condition = "";
            if (kind !="-1")
            {
                Condition = "   kind=" + kind + " ";
            }
            else
            {
                Condition = "  id>0 ";
            }

            if (status != "-1")
            {
                Condition += "  and  status=" + status + "  ";
            }
            else
            {
                status += "  and id>0 ";
            }


            var progList = new List<BO.Programs>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Programs where  "+Condition+"  order by priority desc ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var prgObj = new BO.Programs();

                prgObj.Director = Dr["Director"].ToString();
                prgObj.EpisodesCount = short.Parse(Dr["Episodes"].ToString());
                prgObj.FullText = Dr["FullText"].ToString();
                prgObj.Genre = short.Parse(Dr["Genre"].ToString());
                prgObj.Id = int.Parse(Dr["Id"].ToString());
                prgObj.Image = Dr["Image"].ToString();
                prgObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                prgObj.Kind = short.Parse(Dr["Kind"].ToString());
                prgObj.Priority = int.Parse(Dr["Priority"].ToString());
                prgObj.Produced = Dr["Produced"].ToString();
                prgObj.Title = Dr["Title"].ToString();
                prgObj.Video = Dr["Video"].ToString();
                prgObj.Created = Dr["Created"].ToString();
                prgObj.ViewCount = Dr["ViewCount"].ToString();
                prgObj.Youtube = Dr["Youtube"].ToString();


                progList.Add(prgObj);
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

            return progList;
        }
        public static BO.Programs SelectbyId(int ProgId)
        {
            BO.Programs prgObj = new BO.Programs();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Programs where id=" + ProgId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                prgObj.Director = Dr["Director"].ToString();
                prgObj.EpisodesCount = short.Parse(Dr["Episodes"].ToString());
                prgObj.FullText = Dr["FullText"].ToString();
                prgObj.Genre = short.Parse(Dr["Genre"].ToString());
                prgObj.Id = int.Parse(Dr["Id"].ToString());
                prgObj.Image = Dr["Image"].ToString();
                prgObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                prgObj.Kind = short.Parse(Dr["Kind"].ToString());
                prgObj.Priority = int.Parse(Dr["Priority"].ToString());
                prgObj.Produced = Dr["Produced"].ToString();
                prgObj.Title = Dr["Title"].ToString();
                prgObj.Video = Dr["Video"].ToString();
                prgObj.Created = Dr["Created"].ToString();
                prgObj.Published = Dr["Published"].ToString();
                prgObj.IntroText = Dr["IntroText"].ToString();
                prgObj.FullText = Dr["FullText"].ToString();
                prgObj.Status = Dr["Status"].ToString();
                prgObj.ViewCount = Dr["ViewCount"].ToString();
                prgObj.Youtube = Dr["Youtube"].ToString();
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
            return prgObj;
        }
        public static BO.Programs Insert(BO.Programs Prog)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"INSERT INTO [dbo].[Programs]
           ([Title]
           ,[IntroText]
           ,[FullText]
           ,[Kind]
           ,[Episodes]
           ,[Director]
           ,[Genre]
           ,[Produced]
           ,[Priority]
           ,[Image]
           ,[Video]
           ,[Created_By]
           ,[IsPublished]
           ,[Status],[youtube],[viewcount]
            )
                VALUES
                (N'" + Prog.Title+@"'
                ,N'" + Prog.IntroText + @"'
                ,N'" + Prog.FullText + @"'
                ," + Prog.Kind +@"
                ,"+Prog.EpisodesCount+@"
                ,N'" + Prog.Director + @"'
                ," + Prog.Genre + @"
                ,N'" + Prog.Produced + @"'
                ," + Prog. Priority+ @"
                ,N'" + Prog.Image + @"'
                ,N'" + Prog.Video + @"',
                "+Prog.Created_By+","+Prog.IsPublished+@","+Prog.Status+",N'"+Prog.Youtube+"',0)";
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
            return Prog;
        }
        public static BO.Programs Update(BO.Programs Prog)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update [dbo].[Programs]
           set   
            [Title]=N'" + Prog.Title + @"'
           ,[IntroText]=N'" + Prog.IntroText + @"'
           ,[FullText]=N'" + Prog.FullText + @"'
           ,[Kind]=" + Prog.Kind + @"
           ,[Episodes]=" + Prog.EpisodesCount + @"
           ,[Director]=N'" + Prog.Director + @"'
           ,[Genre]=" + Prog.Genre + @"
           ,[Produced]=N'" + Prog.Produced + @"' 
           ,[Image]= N'" + Prog.Image + @"' 
           ,[IsPublished]= " + Prog.IsPublished + @" 
           ,[Video]= N'" + Prog.Video + @"' , [status]=" + Prog.Status + ", [youtube]=N'" + Prog.Youtube + "', [priority]=" + Prog.Priority + " where id=" + Prog.Id;
    
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
            return Prog;
        }
        public static List<BO.Programs> FrontendlistPrograms(string kind,string count,string order,string status)
        {
            var progList = new List<BO.Programs>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select top("+count+") * from Programs where ispublished=1 and kind=" + kind + " and status="+status+" order by "+order;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var prgObj = new BO.Programs();

                prgObj.Director = Dr["Director"].ToString();
                prgObj.EpisodesCount = short.Parse(Dr["Episodes"].ToString());
                prgObj.FullText = Dr["FullText"].ToString();
                prgObj.Genre = short.Parse(Dr["Genre"].ToString());
                prgObj.Id = int.Parse(Dr["Id"].ToString());
                prgObj.Image = Dr["Image"].ToString();
                prgObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                prgObj.Kind = short.Parse(Dr["Kind"].ToString());
                prgObj.Priority = int.Parse(Dr["Priority"].ToString());
                prgObj.Produced = Dr["Produced"].ToString();
                prgObj.Title = Dr["Title"].ToString();
                prgObj.Video = Dr["Video"].ToString();
                prgObj.Created = Dr["Created"].ToString();
                prgObj.IntroText = Dr["IntroText"].ToString();
                prgObj.Status = Dr["Status"].ToString();
                prgObj.ViewCount = Dr["ViewCount"].ToString();
                prgObj.Youtube = Dr["Youtube"].ToString();


                progList.Add(prgObj);
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

            return progList;
        }
        public static BO.Programs FrontendSelectbyId(int ProgId)
        {
            BO.Programs prgObj = new BO.Programs();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from Programs where id=" + ProgId + " and IsPublished=1";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                prgObj.Director = Dr["Director"].ToString();
                prgObj.EpisodesCount = short.Parse(Dr["Episodes"].ToString());
                prgObj.FullText = Dr["FullText"].ToString();
                prgObj.Genre = short.Parse(Dr["Genre"].ToString());
                prgObj.Id = int.Parse(Dr["Id"].ToString());
                prgObj.Image = Dr["Image"].ToString();
                prgObj.IsPublished = short.Parse(Dr["IsPublished"].ToString());
                prgObj.Kind = short.Parse(Dr["Kind"].ToString());
                prgObj.Priority = int.Parse(Dr["Priority"].ToString());
                prgObj.Produced = Dr["Produced"].ToString();
                prgObj.Title = Dr["Title"].ToString();
                prgObj.Video = Dr["Video"].ToString();
                prgObj.Created = Dr["Created"].ToString();
                prgObj.Published = Dr["Published"].ToString();
                prgObj.IntroText = Dr["IntroText"].ToString();
                prgObj.FullText = Dr["FullText"].ToString();
                prgObj.Status = Dr["Status"].ToString();
                prgObj.ViewCount = Dr["ViewCount"].ToString();
                prgObj.Youtube = Dr["Youtube"].ToString();
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
            return prgObj;
        }
        public static bool UpdateViewCount(int Id)
        {
            var Success = false;

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update Programs set Viewcount=Viewcount+1  where Id=" + Id;
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
     
    }
}