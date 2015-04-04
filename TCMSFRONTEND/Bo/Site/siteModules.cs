using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCMSFRONTEND.Bo.Site
{
    public class siteModules
    {
        public string Site_Modules_Menu_Position { get; set; }
        public int Site_Modules_Menu_Priority { get; set; }


        public string Site_Modules_Title { get; set; }
        public string Site_Modules_Url { get; set; }
        public string Site_Modules_Body { get; set; }
        public string Site_Modules_Params { get; set; }
        public string Site_Modules_Id { get; set; }

        public string Site_Modules_List_Title { get; set; }
        public string Site_Modules_List_Builder { get; set; }
        public string Site_Modules_Generated_Html { get; set; }


        public string Site_Menu_Title { get; set; }
        public string Site_Menu_Alias { get; set; }
        public string Site_Menu_Params { get; set; }
        public string Site_Menu_Layout { get; set; }
        public int Site_Menu_Visible { get; set; }
        public int Site_Menu_Kind { get; set; }
        public int Site_Menu_Priority { get; set; }
        public int Site_Menu_Pid { get; set; }

        public string visibleCount { get; set; }
        public string htmlClass { get; set; }

        public string Site_Modules_ViewPath { get; set; }
        public Bo.Site.sitePageConfig pageConfig { get; set; }
        public string searchKey { get; set; }
      
        
    }
}