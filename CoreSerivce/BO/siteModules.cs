using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class siteModules
    {

        [DataMember]
        public string Site_Modules_Menu_Position { get; set; }
        [DataMember]
        public int Site_Modules_Menu_Priority { get; set; }

        [DataMember]
        public string Site_Modules_Title { get; set; }
        [DataMember]
        public string Site_Modules_Url { get; set; }
        [DataMember]
        public string Site_Modules_Body { get; set; }
        [DataMember]
        public string Site_Modules_Params { get; set; }
        [DataMember]
        public string Site_Modules_Id { get; set; }

        [DataMember]
        public string Site_Modules_List_Title { get; set; }
        [DataMember]
        public string Site_Modules_List_Builder { get; set; }
        [DataMember]
        public string Site_Modules_Generated_Html { get; set; }


        [DataMember]
        public string Site_Menu_Title { get; set; }

        [DataMember]
        public string Site_Menu_Alias { get; set; }

        [DataMember]
        public string Site_Menu_Params { get; set; }
        [DataMember]
        public string Site_Menu_Layout { get; set; }

        [DataMember]
        public int Site_Menu_Visible { get; set; }

        [DataMember]
        public int Site_Menu_Kind { get; set; }

        [DataMember]
        public int Site_Menu_Priority { get; set; }

        [DataMember]
        public int Site_Menu_Pid { get; set; }
        [DataMember]
        public string Site_Modules_ViewPath { get; set; }
    }
}