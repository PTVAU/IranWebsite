using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TCMSFRONTEND.Bo.Data
{
    [DataContract]
    public class Weather
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string cssClass { get; set; }
        [DataMember]
        public string temp { get; set; }
        [DataMember]
        public bool first { get; set; }
    }
}