
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using Nustache.Core;
using System.IO;

namespace TCMSFRONTEND
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(HttpContext.Current.Request.Url.AbsolutePath.Replace("index.aspx", ""));
            string pgUrl = HttpContext.Current.Request.Url.AbsolutePath;
            //Handle old Website:
            if (pgUrl.ToLower().StartsWith("/detail"))
            {
                Response.Redirect("http://93.190.24.12" + pgUrl);
            }
            string cacheResult = "";
            string cacheKey = HttpContext.Current.Request.Url.ToString();
            cacheResult = (string)Core.Utility.cacheControl(cacheKey, null, "R", 0);
            if (string.IsNullOrEmpty(cacheResult))
            {
                string[] pgUrlSplitted = pgUrl.Split('/');
                string pgAlias = pgUrlSplitted[1];
                if (pgAlias.ToLower() == "default.aspx")
                {
                    pgAlias = "home";
                }
                //Load all Modules for this Page:
                List<Bo.Site.siteModules> modulesList = Core.Builder.SelectMenuModules(pgAlias);
                //Load Page Layout:
                String PageLayout = "";
                if (modulesList.Count > 0)
                {
                    PageLayout = Core.Builder.LoadLayout(modulesList[0].Site_Menu_Layout);
                }
                //Get new item of site positions class:
                Bo.Site.sitePositions Pos = new Bo.Site.sitePositions();
                Bo.Site.sitePageConfig cnfg = new Bo.Site.sitePageConfig();
                cnfg.title = string.Empty;
                foreach (Bo.Site.siteModules Md in modulesList)
                {
                    //Call Method to generate html:
                    Bo.Site.siteModules newMd = Core.ModuleBuilder.findBuilder(Md);
                    if (newMd.pageConfig != null)
                    {
                        cnfg = newMd.pageConfig;
                    }
                    //Add generated html to position
                    switch (Md.Site_Modules_Menu_Position)
                    {
                        case "showcase":
                            Pos.showcase += newMd.Site_Modules_Generated_Html;
                            break;
                        case "main":
                            Pos.main += newMd.Site_Modules_Generated_Html;
                            break;
                        case "masthead":
                            Pos.masthead += newMd.Site_Modules_Generated_Html;
                            break;
                        case "menu":
                            Pos.menu += newMd.Site_Modules_Generated_Html;
                            break;
                        case "copyright":
                            Pos.copyright += newMd.Site_Modules_Generated_Html;
                            break;
                        case "top":
                            Pos.top += newMd.Site_Modules_Generated_Html;
                            break;
                        case "bot":
                            Pos.bot += newMd.Site_Modules_Generated_Html;
                            break;
                        case "green":
                            Pos.green += newMd.Site_Modules_Generated_Html;
                            break;
                        case "gray":
                            Pos.gray += newMd.Site_Modules_Generated_Html;
                            break;
                        case "lightgray":
                            Pos.lightgray += newMd.Site_Modules_Generated_Html;
                            break;
                        default:
                            break;
                    }
                }
                Pos.pageConfig = cnfg;               

                string result=Nustache.Core.Render.StringToString(PageLayout,Pos);


                //var template = new Template();
                //template.Load(new StringReader(PageLayout));
                //string result = "";
                //template.Render(Pos, new TextWriter(result),);
              


                //Template template = new Template();
                //template.Load(new StringReader(PageLayout));
                //string result = template.Render()
                ////Render template with data:
                //FormatCompiler compiler = new FormatCompiler();
                //Template generator = compiler.Compile(PageLayout);
                //string result = generator.Render(Pos);
                ////result = Regex.Replace(result, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);
                ////result = Regex.Replace(result, "<!--.*?-->", "", RegexOptions.Singleline);
                cacheResult = Core.Utility.cacheControl(cacheKey, result, "W", 10).ToString();
            }
            //Send output html to client:
            Response.Write(cacheResult);
        }
    }
}