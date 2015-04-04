using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class Repositories
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public short Kind { get; set; }

        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int IsPublished { get; set; }

        [DataMember]
        public List<Tags> Tags { get; set; }


        [DataMember]
        public int Created_By { get; set; }


        [DataMember]
        public string Created { get; set; }

        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public string Thumbnail { get; set; }
    }
}
