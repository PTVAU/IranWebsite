using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Service
{
    public class Contents
    {
        public string Title { get; set; }

        public string Alias { get; set; }


        public string Introtext { get; set; }


        public string Fulltext { get; set; }


        public short State { get; set; }


        public string Created { get; set; }


        public int Created_By { get; set; }


        public string Modified { get; set; }


        public int Modified_By { get; set; }


        public int Owner { get; set; }


        public string Published { get; set; }


        public int Published_By { get; set; }



        public string Metadesc { get; set; }


        public int Viewcount { get; set; }


        public int Id { get; set; }


        public string ShortTitle { get; set; }

        public List<Bo.Service.Tags> Tags { get; set; }

        public List<Bo.Service.Categories> Categories { get; set; }

        public List<Bo.Service.Repositories> Repositories { get; set; }

        public int IsPublished { get; set; }


        // public List<BO.Contents_Flow> Flow { get; set; }


        public string OwnerName { get; set; }

        public string Youtube { get; set; }
        public List<Bo.Service.Comments> Comments { get; set; }
        public short ItemPriority { get; set; }
    }
}
