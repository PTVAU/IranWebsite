using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Service
{
    [DataContract]
    public class Comments
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Parent_Id { get; set; }

        [DataMember]
        public int Content_Id { get; set; }

        [DataMember]
        public short Kind { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public int User_Id { get; set; }

        [DataMember]
        public string IP { get; set; }

        [DataMember]
        public short Published { get; set; }

        [DataMember]
        public string Datetime_Published { get; set; }

        [DataMember]
        public int Published_By { get; set; }

        [DataMember]
        public string Datetime_Insert { get; set; }

        [DataMember]
        public int Vote_Up { get; set; }

        [DataMember]
        public int Vote_Down { get; set; }

        [DataMember]
        public short Subscribe { get; set; }
        [DataMember]
        public List<Bo.Service.Comments> Reply { get; set; }
    }
}
