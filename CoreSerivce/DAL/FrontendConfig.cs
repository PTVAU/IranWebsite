using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CoreSerivce.DAL
{
    public class FrontendConfig
    {
        public static List<BO.siteModules> modulesSelectByAlias(string Alias)
        {
            List<BO.siteModules> siteModulesList = new List<BO.siteModules>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT  Site_Modules_Menu.Site_Modules_Menu_Position, Site_Modules_Menu.Site_Modules_Menu_Priority, Site_Modules.Site_Modules_Title, Site_Modules.Site_Modules_Url,Site_modules.Site_Modules_Params, 
                         Site_Modules_List.Site_Modules_List_Title, Site_Modules_List.Site_Modules_List_Builder, Site_Menu.Site_Menu_Title, Site_Menu.Site_Menu_Alias, Site_Menu.Site_Menu_Params, 
                         Site_Menu.Site_Menu_Layout, Site_Menu.Site_Menu_Visible, Site_Menu.Site_Menu_Kind, Site_Menu.Site_Menu_Priority, Site_Menu.Site_Menu_Pid,Site_Modules.Site_Modules_body,Site_Modules.Site_Modules_Id
                         FROM   Site_Modules_Menu INNER JOIN
                         Site_Modules ON Site_Modules_Menu.Site_Modules_Menu_Smid = Site_Modules.Site_Modules_Id INNER JOIN
                         Site_Menu ON Site_Modules_Menu.Site_Modules_Menu_MenueId = Site_Menu.Site_Menu_Id INNER JOIN
                         Site_Modules_List ON Site_Modules.Site_Modules_ModuleId = Site_Modules_List.Site_Modules_List_Id  where  Site_Menu.Site_Menu_Alias=N'" + Alias + "'  order by   Site_Modules_Menu.Site_Modules_Menu_Priority";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    BO.siteModules siteModule = new BO.siteModules();

                    siteModule.Site_Menu_Alias = Dr["Site_Menu_Alias"].ToString();
                    siteModule.Site_Menu_Kind = int.Parse(Dr["Site_Menu_Kind"].ToString());
                    siteModule.Site_Menu_Layout = Dr["Site_Menu_Layout"].ToString();
                    siteModule.Site_Menu_Params = Dr["Site_Menu_Params"].ToString();
                    siteModule.Site_Menu_Pid = int.Parse(Dr["Site_Menu_Pid"].ToString());
                    siteModule.Site_Menu_Priority = int.Parse(Dr["Site_Menu_Priority"].ToString());
                    siteModule.Site_Menu_Title = Dr["Site_Menu_Title"].ToString();
                    siteModule.Site_Menu_Visible = int.Parse(Dr["Site_Menu_Visible"].ToString());
                    siteModule.Site_Modules_Url = Dr["Site_Modules_Url"].ToString();
                    siteModule.Site_Modules_List_Builder = Dr["Site_Modules_List_Builder"].ToString();
                    siteModule.Site_Modules_List_Title = Dr["Site_Modules_List_Title"].ToString();
                    siteModule.Site_Modules_Menu_Position = Dr["Site_Modules_Menu_Position"].ToString();
                    siteModule.Site_Modules_Menu_Priority = int.Parse(Dr["Site_Modules_Menu_Priority"].ToString());
                    siteModule.Site_Modules_Title = Dr["Site_Modules_Title"].ToString();
                    siteModule.Site_Modules_Params = Dr["Site_Modules_Params"].ToString();
                    siteModule.Site_Modules_Body = Dr["Site_Modules_Body"].ToString();
                    siteModule.Site_Modules_ViewPath = Dr["Site_Modules_Body"].ToString();
                    siteModule.Site_Modules_Id = Dr["Site_Modules_Id"].ToString();

                    siteModulesList.Add(siteModule);
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

            return siteModulesList;
        }

        public static List<BO.urlRouting> urlRoutingSelectAll()
        {
            List<BO.urlRouting> urlRoutingList = new List<BO.urlRouting>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"SELECT   [Site_Menu_Alias]   FROM [iran].[dbo].[Site_Menu] ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            //try
            //{
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    BO.urlRouting urlRoutingObject = new BO.urlRouting();

                    urlRoutingObject.pageAlias = Dr["Site_Menu_Alias"].ToString();


                    urlRoutingList.Add(urlRoutingObject);
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

            return urlRoutingList;
        }
    }
}