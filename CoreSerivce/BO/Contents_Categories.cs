using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class Contents_Categories
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Contents_Id { get; set; }

        [DataMember]
        public int Categories_Id { get; set; }

        [DataMember]
        public int Priority { get; set; }
    }
}
