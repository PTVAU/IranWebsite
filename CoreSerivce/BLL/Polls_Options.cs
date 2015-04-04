using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSerivce.BLL
{
    public class Polls_Options
    {
        public static BO.Polls_Options Insert(BO.Polls_Options PollsOpObj)
        {
            return DAL.Polls_Options.Insert(PollsOpObj);
        }
        public static List<BO.Polls_Options> SelectByPid(int Pid)
        {
            List<BO.Polls_Options> Lst = DAL.Polls_Options.SelectByPid(Pid);

            try
            {
                int TtCount = 0;
                foreach (BO.Polls_Options item in Lst)
                {
                    TtCount += item.SelectedCount;
                }

                foreach (BO.Polls_Options item in Lst)
                {
                    if (item.SelectedCount !=0 && TtCount>0)
                    {
                        item.Percent = Math.Round(double.Parse((item.SelectedCount * 100 / TtCount).ToString()), 2).ToString();
                    }
                    else
                    {
                        item.Percent = "0";
                    }
                }
            }
            catch 
            {
                
                
            }

            return Lst;
        }
        public static BO.Polls_Options Update(BO.Polls_Options PollsOpObj)
        {
            return DAL.Polls_Options.Update(PollsOpObj);
        }
        public static bool Delete(int ID)
        {
            return DAL.Polls_Options.Delete(ID);
        }
        public static void UpdateCount(int optionId)
        {
            DAL.Polls_Options.UpdateCount(optionId);
        }
    }
}
