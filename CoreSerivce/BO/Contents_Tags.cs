using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class Contents_Tags
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Content_Id { get; set; }

        [DataMember]
        public int Tag_Id { get; set; }
    }
}
