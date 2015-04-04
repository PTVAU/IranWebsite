using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    public class Contents
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public string Introtext { get; set; }

        [DataMember]
        public string Fulltext { get; set; }

        [DataMember]
        public short State { get; set; }

        [DataMember]
        public string Created { get; set; }

        [DataMember]
        public int Created_By { get; set; }

        [DataMember]
        public string Modified { get; set; }

        [DataMember]
        public int Modified_By { get; set; }

        [DataMember]
        public int Owner { get; set; }

        [DataMember]
        public string Published { get; set; }

        [DataMember]
        public int Published_By { get; set; }


        [DataMember]
        public string Metadesc { get; set; }

        [DataMember]
        public int Viewcount { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ShortTitle { get; set; }


        [DataMember]
        public List<BO.Tags> Tags { get; set; }

        [DataMember]
        public List<BO.Categories> Categories { get; set; }

        [DataMember]
        public List<BO.Repositories> Repositories { get; set; }

        [DataMember]
        public int IsPublished { get; set; }

        [DataMember]
        public List<BO.Contents_Flow> Flow { get; set; }

        [DataMember]
        public string OwnerName { get; set; }
        [DataMember]
        public string Youtube { get; set; }
        [DataMember]
        public List<BO.Comments> Comments { get; set; }

        [DataMember]
        public short ItemPriority { get; set; }
        
    }
}
