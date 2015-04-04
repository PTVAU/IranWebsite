using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Service
{
    public class Tags
    {
      
        public int id { get; set; }


        public int Parent_Id { get; set; }


        public string name { get; set; }


    
        public short Published { get; set; }


        public string Created { get; set; }



        public int Created_By { get; set; }



        public string Modified { get; set; }


        public int Modified_By { get; set; }
    }
}
