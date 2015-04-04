using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace CoreSerivce.PL
{
    [MessageContract(IsWrapped = false)]
    [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Polls : IPolls
    {
        public List<BO.Polls> SelectAll()
        {
            return BLL.Polls.SelectAll();
        }

        public BO.Polls Insert(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();


            var PollObj = new BO.Polls();
            PollObj = JsonConvert.DeserializeObject<BO.Polls>(body);


            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            PollObj.Created_By = CurUsr.Id;

            PollObj = BLL.Polls.Insert(PollObj);


            foreach (BO.Polls_Options PollOptionItem in PollObj.Polls_Options)
            {
                PollOptionItem.Pid = PollObj.Id;
                PollOptionItem.Created_By = CurUsr.Id;
                PollOptionItem.SelectedCount = 1;
                BLL.Polls_Options.Insert(PollOptionItem);
            }

            return PollObj;
        }

        public BO.Polls SelectById(string ID)
        {
            var PollObj = BLL.Polls.SelectById(int.Parse(ID));



            PollObj.Polls_Options = BLL.Polls_Options.SelectByPid(PollObj.Id);

            return PollObj;
        }

        public BO.Polls Update(Stream Data, string ID)
        {
            var body = new StreamReader(Data).ReadToEnd();


            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());



            var PollObj = new BO.Polls();
            PollObj = JsonConvert.DeserializeObject<BO.Polls>(body);
            PollObj.Published_By = CurUsr.Id;
            PollObj.Id = int.Parse(ID);
            PollObj = BLL.Polls.Update(PollObj);




            var DBOptions = BLL.Polls_Options.SelectByPid(PollObj.Id);


            foreach (BO.Polls_Options DBitem in DBOptions)
            {
                var Exist = false;

                foreach (BO.Polls_Options Clientitem in PollObj.Polls_Options)
                {
                    if (DBitem.Id == Clientitem.Id)
                    {
                        Exist = true;

                        BLL.Polls_Options.Update(Clientitem);
                    }
                }
                if (!Exist)
                {
                    BLL.Polls_Options.Delete(DBitem.Id);
                }
            }


            foreach (BO.Polls_Options Clientitem in PollObj.Polls_Options)
            {
                if (Clientitem.Id == 0)
                {
                    Clientitem.Pid = PollObj.Id;
                    BLL.Polls_Options.Insert(Clientitem);
                }
            }








            return PollObj;
        }
    }
}
