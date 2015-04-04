using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Comments
    {
        public static List<BO.Comments> SelectAll()
        {
            return DAL.Comments.SelectAll();
        }
        public static BO.Comments Update(BO.Comments CommentObj)
        {
            return DAL.Comments.Update(CommentObj);
        }
        public static List<BO.Comments> SelectSearch(string SearchText)
        {
            return DAL.Comments.SelectSearch(SearchText);
        }
        public static BO.Comments Insert(BO.Comments CommentObj)
        {
            return DAL.Comments.Insert(CommentObj);
        }
        public static List<BO.Comments> SelectByContentId(string contentId, string count, string lastId)
        {
            List<BO.Comments> Lst = DAL.Comments.SelectByContentId(contentId, count, lastId);
            foreach (BO.Comments item in Lst)
            {
                item.Reply = DAL.Comments.SelectByReply(item.Id.ToString());
            }
            return Lst;
        }
        public static void VoteDown(string CommentId)
        {
            DAL.Comments.VoteDown(CommentId);
        }
        public static void VoteUp(string CommentId)
        {
            DAL.Comments.VoteUp(CommentId);
        }
        public static BO.Comments SelectById(string commentId)
        {
            return DAL.Comments.SelectById(commentId);
        }
    }
}
