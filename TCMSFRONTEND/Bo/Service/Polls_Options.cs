using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Service
{
    public class Polls_Options
    {
      
        public int Id { get; set; }

        public string Title { get; set; }

        public int Pid { get; set; }

        public string Created { get; set; }

        public int Created_By { get; set; }

        public short Priority { get; set; }

        public int SelectedCount { get; set; }
        public string Percent { get; set; }
    }
}
