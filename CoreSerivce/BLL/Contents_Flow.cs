using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Contents_Flow
    {
        public static List<BO.Contents> Select(int UserId)
        {
            return DAL.Contents_Flow.Select(UserId);
        }
        public static List<BO.Contents> SelectSent(int UserId)
        {
            return DAL.Contents_Flow.SelectSent(UserId);
        }
        public static List<BO.Contents_Flow> SelectByContentId(int Content_Id)
        {
            return DAL.Contents_Flow.SelectByContentId(Content_Id);
        }
        public static BO.Contents_Flow Insert(BO.Contents_Flow FlwObj)
        {
            return DAL.Contents_Flow.Insert(FlwObj);
        }
        public static void UpdateSeen(int UserId, int Content_Id)
        {
            DAL.Contents_Flow.UpdateSeen(UserId, Content_Id);
        }
        public static List<BO.Contents> SelectNotSeen(int UserId)
        {
            return DAL.Contents_Flow.SelectNotSeen(UserId);
;        }
    }
}
