using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Service
{
    [Serializable]
    public class Repositories
    {
        public int Id { get; set; }

      
        public string Title { get; set; }

   
        public short Kind { get; set; }

     
        public string FilePath { get; set; }

   
        public string Description { get; set; }

   
        public int IsPublished { get; set; }

      
      //  public List<Tags> Tags { get; set; }


      
        public int Created_By { get; set; }


      
        public string Created { get; set; }

      
        public int Priority { get; set; }
    }
}
