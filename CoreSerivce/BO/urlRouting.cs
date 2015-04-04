using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CoreSerivce.BO
{
    [DataContract]
    public class urlRouting
    {
        [DataMember]
        public string pageAlias { get; set; }       

    }
}