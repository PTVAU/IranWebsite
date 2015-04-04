using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    [Serializable]
    public class Message
    {
        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public string MessageBody { get; set; }

        [DataMember]
        public int MessageFromId { get; set; }

        [DataMember]
        public int MessageToId { get; set; }

        [DataMember]
        public string MessageSendDate { get; set; }

        [DataMember]
        public string MessageSeenDate { get; set; }

        [DataMember]
        public string MessageFrom { get; set; }

        [DataMember]
        public string MessageTo { get; set; }

        [DataMember]
        public int TotalUnread { get; set; }
        
        [DataMember]
        public string MessageFromPicture { get; set; }
        
    }
}
