using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace TCMSFRONTEND
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterCustomRoutes(RouteTable.Routes);
        }
        void RegisterCustomRoutes(RouteCollection routes)
        {

            List<Bo.Site.urlRouting> urlRoutingList = Dal.SiteConfig.urlRoutingSelect();
            foreach (Bo.Site.urlRouting rt in urlRoutingList)
            {
                if(rt.pageAlias.StartsWith("/"))
                {
                    rt.pageAlias = rt.pageAlias.Remove(0,1);
                }
                routes.MapPageRoute(rt.pageAlias, rt.pageAlias, "~/Default.aspx");
            }

            //Static routes:
            routes.MapPageRoute("staticnews", "newsdetail/{CategoryTitle}/{NewsID}/{NewsTitle}", "~/Default.aspx");
            routes.MapPageRoute("staticsections", "category/{CategoryTitle}/{CategoryID}", "~/Default.aspx");
            routes.MapPageRoute("staticsubsections", "subcategory/{CategoryTitle}/{CategoryID}/{subCategoryTitle}/{subCategoryID}", "~/Default.aspx");
            routes.MapPageRoute("statictag", "tag/{TagTitle}/{TagID}", "~/Default.aspx");
            routes.MapPageRoute("staticprogram", "program/{ProgramTitle}/{ProgramId}", "~/Default.aspx");
            routes.MapPageRoute("staticepisode", "showepisode/{ProgramTitle}/{episodeTitle}/{EpisodeId}", "~/Default.aspx");
            routes.MapPageRoute("staticshowprogram", "showprogram/{ProgramTitle}/{ProgId}", "~/Default.aspx");
            routes.MapPageRoute("staticdetailoldwebsite", "detail/{year}/{month}/{day}/{id}/{title}", "~/Default.aspx");

        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}