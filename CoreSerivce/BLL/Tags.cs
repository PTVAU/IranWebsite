using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Tags
    {
        public static BO.Tags Insert(BO.Tags  TagObj)
        {
            return DAL.Tags.Insert(TagObj);
        }
        public static List<BO.Tags> SelectAll()
        {
            return DAL.Tags.SelectAll();
        }
        public static BO.Tags Update(BO.Tags TagObj)
        {
            return DAL.Tags.Update(TagObj);
        }
        public static List<BO.Tags> SelectSearch(string SearchText)
        {
            return DAL.Tags.SelectSearch(SearchText);
        }
    }
}
