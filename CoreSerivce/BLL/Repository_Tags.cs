using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Repository_Tags
    {
        public static BO.Repository_Tags Insert(BO.Repository_Tags TgObj)
        {
            return DAL.Repository_Tags.Insert(TgObj);
        }
        public static List<BO.Tags> SelectByRepositoryId(int RepositoryId)
        {
            return DAL.Repository_Tags.SelectByRepositoryId(RepositoryId);
        }
        public static void DeleteByRepositoryId(int RepositoryId)
        {
            DAL.Repository_Tags.DeleteByRepositoryId(RepositoryId);
        }
    }
}
