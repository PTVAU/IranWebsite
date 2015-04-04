using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Data
{

    [Serializable]
    public class Polls
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public short Kind { get; set; }

        public int Parent_Id { get; set; }

        public string Created { get; set; }

        public int Created_By { get; set; }

        public string Published { get; set; }

        public int Published_By { get; set; }

        public short IsPublished { get; set; }

        public bool AllowNew { get; set; }


        public bool ShowResult { get; set; }


        public bool ShowValues { get; set; }


        public bool ShowTotal { get; set; }

        public bool ShowComments { get; set; }


        public string Expired { get; set; }


        public string Description { get; set; }

        public List<Polls_Options> Polls_Options { get; set; }
        public string TotalCount { get; set; }
    }
}
