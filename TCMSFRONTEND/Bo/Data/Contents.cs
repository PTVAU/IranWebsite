using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TCMSFRONTEND.Bo.Data
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
        public List<Bo.Data.contentsImages> Images { get; set; }
        public string image { get; set; }
        public string OwnerName { get; set; }
        public string categoryTitle { get; set; }
        public string categoryUrl { get; set; }
        public string url { get; set; }
        public List<Bo.Data.Tags> Tags { get; set; }
        public List<Bo.Data.Contents> relatedByTag { get; set; }
        public bool first { get; set; }
        public bool hasVideo { get; set; }
        public string categoryTitleParent { get; set; }
        public string categoryUrlParent { get; set; }
        public List<Bo.Data.contentsVideo> Videos { get; set; }
        public bool isBreaking { get; set; }
        public List<Bo.Data.Contents> relatedByCategory { get; set; }
        public string Youtube { get; set; }
        public List<Bo.Data.Comments> Comments { get; set; }
        public string CommentsCount { get; set; }
    }   
}
