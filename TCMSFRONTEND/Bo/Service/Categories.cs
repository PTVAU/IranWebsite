using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Service
{
    
    public class Categories
    {
     
        public int Id { get; set; }

       
        public int Parent_Id { get; set; }

       
        public string Title { get; set; }

       
        public string Metadesc { get; set; }

        
        public int Published { get; set; }

        public int Sort { get; set; }
        public string parentTitle { get; set; }
    }
}
