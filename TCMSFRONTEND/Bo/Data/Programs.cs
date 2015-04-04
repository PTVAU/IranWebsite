using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Data
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
        [DataMember]
        public string VideoThumbnail { get; set; }
        [DataMember]
        public string Url { get; set; }
        public string VisibleCount { get; set; }
        public bool first { get; set; }

        public string ProducedShort { get; set; }
        [DataMember]
        public string ViewCount { get; set; }
        [DataMember]
        public string Youtube { get; set; }
        public bool MenuFirstColumn { get; set; }

        
    }
}
