using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Message
    {
        public static List<BO.Message> SelectAllInboxForCurrentUser(int UserId, string Limit)
        {
            return DAL.Message.SelectAllInboxForCurrentUser(UserId, Limit);
        }
        public static List<BO.Message> SelectAllSentForCurrentUser(int UserId)
        {
            return DAL.Message.SelectAllSentForCurrentUser(UserId);
        }
        public static BO.Message Insert(BO.Message Msg)
        {
            return DAL.Message.Insert(Msg);
        }
        public static int SelectCountUnread(int UserId)
        {
            return  DAL.Message.SelectCountUnread(UserId);
        }
        public static void UpdateSeen(int messageId,int userId)
        {
            DAL.Message.UpdateSeen(messageId,userId);
        }
    }
}
