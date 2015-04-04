using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    public class Tags
    {
        [DataMember]
        public int id { get; set; }


        public int Parent_Id { get; set; }


        [DataMember]
        public string name { get; set; }


        [DataMember]
        public short Published { get; set; }


        public string Created { get; set; }



        public int Created_By { get; set; }



        public string Modified { get; set; }


        public int Modified_By { get; set; }
    }
}
