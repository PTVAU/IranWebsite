using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CoreSerivce.BO
{
     [DataContract]
    public class Newsletter
    {
         [DataMember]
         public int Id{ get; set; }
         [DataMember]
         public string name { get; set; }
         [DataMember]
         public string email { get; set; }
         [DataMember]
         public string sections { get; set; }
         [DataMember]
         public string datetime { get; set; }
         [DataMember]
         public int kind { get; set; }
    }
}