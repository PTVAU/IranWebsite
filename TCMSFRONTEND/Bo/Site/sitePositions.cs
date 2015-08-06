using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCMSFRONTEND.Bo.Site
{
    public class sitePositions
    {
        // header
        public string masthead { get; set; }
        public string menu { get; set; }

        // top
        public string showcase { get; set; }
        public string top { get; set; }

        // programs


        // mainbody
        public string main { get; set; }
        public string item { get; set; }

        // bottom
        public string bot { get; set; }

        // footer
        public string copyright { get; set; }

        // Colors
        public string green { get; set; }
        public string gray { get; set; }
        public string lightgray{ get; set; }
        public string sidebar { get; set; }

        //Global Config:
        public sitePageConfig pageConfig { get; set; }

    }
}