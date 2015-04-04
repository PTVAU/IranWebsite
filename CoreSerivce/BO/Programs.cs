using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class Programs
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string IntroText { get; set; }
        [DataMember]
        public string FullText { get; set; }
        [DataMember]
        public short Kind { get; set; }
        [DataMember]
        public short EpisodesCount { get; set; }
        [DataMember]
        public string Director { get; set; }
        [DataMember]
        public short Genre { get; set; }
        [DataMember]
        public string Produced { get; set; }
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
        [DataMember]
        public List<Episodes> EpisodesList { get; set; }
        [DataMember]
        public string Published { get; set; }
        public int Created_By { get; set; }
        [DataMember]
        public string VideoThumbnail { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ViewCount { get; set; }
        [DataMember]
        public string Youtube { get; set; }
        
    }
}
