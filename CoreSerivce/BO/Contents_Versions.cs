using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class Contents_Versions
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Content_Id { get; set; }

        [DataMember]
        public string ShortTitle { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public string Introtext { get; set; }
        [DataMember]
        public string Fulltext { get; set; }
    }
}
