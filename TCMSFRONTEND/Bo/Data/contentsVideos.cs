using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCMSFRONTEND.Bo.Data
{
    [Serializable]
    public class contentsVideo
    {
            public string videoPath { get; set; }
            public string description { get; set; }
            public string videoThumbail { get; set; }
    }
}