using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class Polls
    {
        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public string Title { get; set; }



        public short Kind { get; set; }


        public int Parent_Id { get; set; }

        [DataMember]
        public string Created { get; set; }

        public int Created_By { get; set; }

        [DataMember]
        public string Published { get; set; }

        public int Published_By { get; set; }

        [DataMember]
        public short IsPublished { get; set; }

        [DataMember]
        public bool AllowNew { get; set; }

        [DataMember]
        public bool ShowResult { get; set; }

        [DataMember]
        public bool ShowValues { get; set; }

        [DataMember]
        public bool ShowTotal { get; set; }

        public bool ShowComments { get; set; }

        [DataMember]
        public string Expired { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<Polls_Options> Polls_Options { get; set; }
    }
}
