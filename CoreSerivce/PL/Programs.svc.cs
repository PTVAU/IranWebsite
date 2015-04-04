using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace CoreSerivce.PL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Programs" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Programs.svc or Programs.svc.cs at the Solution Explorer and start debugging.
   [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Programs : IPrograms
    {
       public List<BO.Programs> SelectAll(string kind,string status)
       {
           return BLL.Programs.listPrograms(kind,status);
       }
        public BO.Programs SelectById(string Id)
       {
           return BLL.Programs.SelectbyId(int.Parse(Id));
       }
        public List<BO.Episodes> episodesSelectByPid(string Pid)
        {
            return BLL.Episodes.listEpisodes(Pid);
        }

        public BO.Episodes episodesSelectById(string Id)
        {
            return BLL.Episodes.SelectbyId(int.Parse(Id));
        }
        public BO.Programs ProgramsInsert(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();
            var ProgObj = new BO.Programs();
            ProgObj = JsonConvert.DeserializeObject<BO.Programs>(body);
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            ProgObj.Created_By = CurUsr.Id;
            ProgObj.Video = ProgObj.Video;
            if (ProgObj.Priority.ToString().Trim().Length<1)
            {
                ProgObj.Priority = 0;
            }
            if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
            {
                ProgObj.IsPublished = 0;
            }

            ProgObj = BLL.Programs.Insert(ProgObj);
            return ProgObj;
        }
        public BO.Episodes EpisodesInsert(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();
            var EspObj = new BO.Episodes();
            EspObj = JsonConvert.DeserializeObject<BO.Episodes>(body);
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            EspObj.Created_By = CurUsr.Id;
            if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
            {
                EspObj.IsPublished = 0;
            }
            EspObj = BLL.Episodes.Insert(EspObj);
            return EspObj;
        }

        public BO.Programs ProgramsUpdate(Stream Data,string id)
        {
            BO.Programs orig = BLL.Programs.SelectbyId(int.Parse(id));
            var body = new StreamReader(Data).ReadToEnd();
            var ProgObj = new BO.Programs();
            ProgObj = JsonConvert.DeserializeObject<BO.Programs>(body);
            ProgObj.Id = int.Parse(id.Trim());
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            if (string.IsNullOrEmpty(ProgObj.Priority.ToString()))
            {
                ProgObj.Priority = 0;
            }

            if (orig.IsPublished == 0)
            {
                //To publish
                if (ProgObj.IsPublished == 1)
                {
                    if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
                    {
                        ProgObj.IsPublished = orig.IsPublished;
                    }                   
                }
            }
            else
            {
                if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
                {
                    //Skip unpublish
                    if (ProgObj.IsPublished == 0)
                    {
                        ProgObj.IsPublished = orig.IsPublished;
                    }
                    if (BLL.Users_Group.UserIsInRole(CurUsr.Id, "Multimedia"))
                    {
                        orig.Youtube = ProgObj.Youtube;
                    }

                    //Skip All Chnages
                    ProgObj = orig;
                }
            }    

           return BLL.Programs.Update(ProgObj);
        }
        public  BO.Episodes EpisodesUpdate(Stream Data, string id)
        {
            BO.Episodes orig = BLL.Episodes.SelectbyId(int.Parse(id));

            var body = new StreamReader(Data).ReadToEnd();
            var EspObj = new BO.Episodes();
            EspObj = JsonConvert.DeserializeObject<BO.Episodes>(body);
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            EspObj.Id = int.Parse(id.Trim());

            if (orig.IsPublished == 0)
            {
                //To publish
                if (EspObj.IsPublished == 1)
                {
                    if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
                    {
                        EspObj.IsPublished = orig.IsPublished;
                    }
                }
            }
            else
            {
                if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
                {
                    //Skip unpublish
                    if (EspObj.IsPublished == 0)
                    {
                        EspObj.IsPublished = orig.IsPublished;
                    }
                    if (BLL.Users_Group.UserIsInRole(CurUsr.Id, "Multimedia"))
                    {
                        orig.Youtube = EspObj.Youtube;
                    }

                    //Skip All Chnages
                    EspObj = orig;
                }
            }
            return BLL.Episodes.Update(EspObj);
        }
    }
}