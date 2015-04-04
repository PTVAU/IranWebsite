using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Polls
    {
        public static List<BO.Polls> SelectAll()
        {
            return DAL.Polls.SelectAll();
        }
        public static BO.Polls Insert(BO.Polls PollsObj)
        {
            return DAL.Polls.Insert(PollsObj);
        }
        public static BO.Polls SelectById(int Id)
        {
            return DAL.Polls.SelectById(Id);
        }
        public static BO.Polls Update(BO.Polls PollsObj)
        {
            return DAL.Polls.Update(PollsObj);
        }
        public static BO.Polls SelectActive()
        {
            return DAL.Polls.SelectActive();
        }
    }
}
