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
    public class ContentsFlow : IContentsFlow
    {
        public List<BO.Contents> Select(string GroupId)
        {
            if (GroupId == "0")
            {
                var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

                GroupId = CurUsr.Id.ToString();
            }

            var Lst = BLL.Contents_Flow.Select(int.Parse(GroupId));

            foreach (BO.Contents item in Lst)
            {
                foreach (BO.Contents_Flow flowitem in item.Flow)
                {
                    flowitem.User_From = BLL.Users.SelectById(flowitem.User_FromId.ToString()).Name;
                    flowitem.User_To = BLL.Users.SelectById(flowitem.User_ToId.ToString()).Name;
                }
            }

            return Lst;
        }

        public List<BO.Contents> SelectSent()
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            var Lst = BLL.Contents_Flow.SelectSent(CurUsr.Id);

            foreach (BO.Contents item in Lst)
            {
                foreach (BO.Contents_Flow flowitem in item.Flow)
                {
                    flowitem.User_From = BLL.Users.SelectById(flowitem.User_FromId.ToString()).Name;
                    flowitem.User_To = BLL.Users.SelectById(flowitem.User_ToId.ToString()).Name;


                    if (int.Parse(flowitem.User_ToId.ToString()) < 1)
                    {
                        flowitem.User_To = BLL.Users_Group.SelectById(int.Parse(flowitem.User_ToId.ToString().Replace("-", string.Empty))).Title;
                    }
                    if (int.Parse(flowitem.User_ToId.ToString()) == 0)
                    {
                        flowitem.User_To = "Site Items";
                    }
                }
            }

            return Lst;
        }

        public BO.Contents_Flow SendFlow(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();



            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());




            var FlwObj = new BO.Contents_Flow();
            FlwObj = JsonConvert.DeserializeObject<BO.Contents_Flow>(body);




            FlwObj.User_FromId = (int)CurUsr.Id;




            BLL.Contents_Flow.UpdateSeen(FlwObj.User_FromId, FlwObj.Content_Id);


            var Cnt = BLL.Contents.SelectById(FlwObj.Content_Id);



            var Vr = BLL.Contents_Versions.Insert(Cnt);



            FlwObj.Version_Id = Vr.Id;




            int state = -100;

            if (FlwObj.User_To.ToString() == "-100")
            {
                FlwObj.User_ToId = CurUsr.Id;
                state = -100;
            }
            else
            {
                FlwObj.User_ToId = int.Parse(FlwObj.User_To);
                state = 0;
            }

            BLL.Contents_Flow.Insert(FlwObj);


            BLL.Contents.UpdateOwner(FlwObj.Content_Id, FlwObj.User_ToId);


            BLL.Contents.UpdateState(FlwObj.Content_Id, state);

            return FlwObj;
        }
        public int SelectNotSeen()
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            var Lst = BLL.Contents_Flow.SelectNotSeen(int.Parse(CurUsr.Id.ToString()));
            return Lst.Count;
        }
    }
}
