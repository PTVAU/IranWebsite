using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Contents_Categories
    {
        public static BO.Contents_Categories Insert(BO.Contents_Categories ContentsCatObj)
        {
            return DAL.Contents_Categories.Insert(ContentsCatObj);
        }
        public static void DeleteByConetentsId(int Content_Id)
        {
            DAL.Contents_Categories.DeleteByConetentId(Content_Id);
        }
        public static List<BO.Categories> SelectByConetentsId(int Content_Id)
        {
            return DAL.Contents_Categories.SelectByConetentsId(Content_Id);
        }
        public static BO.Contents_Categories UpdatePriority(BO.Contents_Categories ContentsCatObj)
        {
            return DAL.Contents_Categories.UpdatePriority(ContentsCatObj);
        }

        public static void DeleteById(int Contents_Id, int Categories_Id)
        {
            DAL.Contents_Categories.DeleteById(Contents_Id, Categories_Id);
        }
    }
}
