using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Contents_Versions
    {
        public static BO.Contents_Versions Insert(BO.Contents ContentObj)
        {
            return DAL.Contents_Versions.Insert(ContentObj);
        }
    }
}
