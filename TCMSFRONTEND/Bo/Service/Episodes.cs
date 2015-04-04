using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Service
{
    [DataContract]
    [Serializable]
    public class Episodes
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Pid { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public short IsPublished { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public string Image { get; set; }
        [DataMember]
        public string Video { get; set; }
         [DataMember]
        public string Created { get; set; }

        public int Created_By { get; set; }
         [DataMember]
        public string Published { get; set; }

        public string Published_By { get; set; }
        [DataMember]
        public string Produced { get; set; }
        [DataMember]
        public string Introtext { get; set; }
        [DataMember]
        public string Fulltext { get; set; }
        [DataMember]
        public string VideoThumbnail { get; set; }
        [DataMember]
        public string ViewCount { get; set; }
        [DataMember]
        public string Youtube { get; set; }
    }
}
