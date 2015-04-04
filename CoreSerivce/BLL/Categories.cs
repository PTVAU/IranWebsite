using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Categories
    {
        public static List<BO.Categories> SelectAll()
        {
            return DAL.Categories.SelectAll();
        }
        public static List<BO.Categories> subCategorySelectByPid(string pid)
        {
            return DAL.Categories.subCategorySelectByPid(pid);
        }
        public static BO.Categories selectById(string id)
        {
           return DAL.Categories.selectById(id);
        }
    }
}
