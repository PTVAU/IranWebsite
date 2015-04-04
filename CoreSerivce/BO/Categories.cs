using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    public class Categories
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Parent_Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Metadesc { get; set; }

        [DataMember]
        public int Published { get; set; }

        [DataMember]
        public int Sort { get; set; }
    }
}
