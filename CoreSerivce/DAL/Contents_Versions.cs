using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace CoreSerivce.DAL
{
    public class Contents_Versions
    {
        public static BO.Contents_Versions Insert(BO.Contents ContentObj)
        {
            var CntVr = new BO.Contents_Versions();
            CntVr.Alias = ContentObj.Alias;
            CntVr.Content_Id = ContentObj.Id;
            CntVr.Fulltext = ContentObj.Fulltext;
            CntVr.Introtext = ContentObj.Introtext;
            CntVr.ShortTitle = ContentObj.ShortTitle;
            CntVr.Title = ContentObj.Title;

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"insert into Contents_Versions 
                                       ( Content_Id,ShortTitle,Title,Alias,Introtext,Fulltext)
                                       values (N'" + ContentObj.Id + "',N'" + ContentObj.ShortTitle.Replace("'", "\'") + "',N'" + ContentObj.Title.Replace("'", "\'") + "',N'" + ContentObj.Alias.Replace("'", "\'") + "',N'" + ContentObj.Introtext.Replace("'", "\'") + "',N'" + ContentObj.Fulltext.Replace("'", "\'") + "' )  select @@IDENTITY ";
            sqlCommand.CommandType = CommandType.Text;



            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());



            sqlCommand.Connection.Open();
            CntVr.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());







            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return CntVr;
        }
    }
}
