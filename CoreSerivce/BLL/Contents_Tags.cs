using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Contents_Tags
    {
        public static void DeleteByConetntId(int Content_Id)
        {
            DAL.Contents_Tags.DeleteByConetntId(Content_Id);
        }
        public static BO.Contents_Tags Insert(BO.Contents_Tags ContentsTagObj)
        {
            return DAL.Contents_Tags.Insert(ContentsTagObj);
        }
        public static List<BO.Tags> SelectByConetntsId(int Content_Id)
        {
            return DAL.Contents_Tags.SelectByConetntsId(Content_Id);
        }
    }
}
