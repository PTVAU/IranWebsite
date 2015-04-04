using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    public class Image_Tmp
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FileHost { get; set; }


        [DataMember]
        public string FullFileName { get; set; }


        [DataMember]
        public int WidthOriginal { get; set; }


        [DataMember]
        public int HeightOriginal { get; set; }


        [DataMember]
        public int HeightCrop { get; set; }


        [DataMember]
        public int WidthCrop { get; set; }


        [DataMember]
        public int X { get; set; }


        [DataMember]
        public int Y { get; set; }


        [DataMember]
        public string error { get; set; }


        [DataMember]
        public bool success { get; set; }

        [DataMember]
        public string ContentType { get; set; }



        [DataMember]
        public string Title { get; set; }


        [DataMember]
        public string Description { get; set; }


        [DataMember]
        public List<Tags> Tags { get; set; }

        [DataMember]
        public short Kind { get; set; }

        [DataMember]
        public string Src { get; set; }
    }
}
