using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    public class Polls_Options
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        public int Pid { get; set; }

        public string Created { get; set; }

        public int Created_By { get; set; }

        [DataMember]
        public short Priority { get; set; }

        [DataMember]
        public int SelectedCount { get; set; }
        [DataMember]
        public string Percent { get; set; }
    }
}
