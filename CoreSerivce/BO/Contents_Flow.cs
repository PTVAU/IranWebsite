using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    public class Contents_Flow
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Content_Id { get; set; }


        public int User_FromId { get; set; }

        public int User_ToId { get; set; }

        [DataMember]
        public string SendDate { get; set; }
        [DataMember]
        public string SeenDate { get; set; }

        [DataMember]
        public string User_From { get; set; }

        [DataMember]
        public string User_To { get; set; }

        [DataMember]
        public int Version_Id { get; set; }
    }
}
