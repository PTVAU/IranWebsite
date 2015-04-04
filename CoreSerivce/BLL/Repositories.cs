using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Repositories
    {
        public static BO.Repositories Insert(BO.Repositories RepObj)
        {
            return DAL.Repositories.Insert(RepObj);
        }
        public static List<BO.Repositories> SelectAll()
        {
            return DAL.Repositories.SelectAll();
        }
        public static List<BO.Repositories> Search(string Keyword)
        {
            return DAL.Repositories.Search(Keyword);
        }
        public static BO.Repositories Update(BO.Repositories RepObj)
        {
            return DAL.Repositories.Update(RepObj);
        }
        public static List<BO.Repositories> SelectRepByFilePath(string FilePath)
        {
            return DAL.Repositories.SelectRepByFilePath(FilePath);
        }
    }
}
