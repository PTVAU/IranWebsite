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
        public string ticker { get; set; }
        public string ads { get; set; }

        // top
        public string showcase { get; set; }
        public string top { get; set; }

        // programs
        public string programs_1 { get; set; }
        public string programs_2 { get; set; }


        // mainbody
        public string main { get; set; }
        public string main_1_1 { get; set; }
        public string main_1_2 { get; set; }
        public string main_2_1 { get; set; }
        public string main_2_2 { get; set; }
        public string main_2_3 { get; set; }
        public string main_2_4 { get; set; }
        public string main_2_5 { get; set; }
        public string main_2_6 { get; set; }

        // media & utility
        public string media { get; set; }
        public string utility { get; set; }

        // sidebar
        public string aside { get; set; }

        // bottom
        public string bot { get; set; }

        // footer
        public string newsletter { get; set; }
        public string copyright { get; set; }
        public string weather { get; set; }

        //between media and bot:
        public string main_3_1 { get; set; }
        public string main_3_2 { get; set; }

        //Global Config:
        public sitePageConfig pageConfig { get; set; }

    }
}