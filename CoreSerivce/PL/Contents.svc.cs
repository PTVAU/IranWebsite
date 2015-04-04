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
    public class Contents : IContents
    {
        public BO.Contents Insert(Stream Data)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            var body = new StreamReader(Data).ReadToEnd();


            var ContentObj = new BO.Contents();
            ContentObj = JsonConvert.DeserializeObject<BO.Contents>(body);
            ContentObj.Alias = "";




            if (ContentObj.IsPublished.ToString() == "1")
            {
                ContentObj.State = 0;
                ContentObj.IsPublished = 1;
                ContentObj.Published_By = CurUsr.Id;
                ContentObj.Published = DateTime.Now.ToString();
            }
            else
            {
                ContentObj.State = -100;
                ContentObj.IsPublished = 0;
                ContentObj.Published_By = 0;
                ContentObj.Published = DateTime.Now.ToString();
            }

            if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
            {
                ContentObj.IsPublished = 0;
            }

            ContentObj.Metadesc = "";


            ContentObj.Created_By = CurUsr.Id;
            ContentObj.Published_By = 0;
            ContentObj.Owner = CurUsr.Id;

            ContentObj = BLL.Contents.Insert(ContentObj);




            if (ContentObj.IsPublished == 1)
            {
                var Flw = new BO.Contents_Flow();
                Flw.Content_Id = ContentObj.Id;
                Flw.User_FromId = CurUsr.Id;
                Flw.User_ToId = 0;
                Flw = BLL.Contents_Flow.Insert(Flw);

                BLL.Contents_Flow.UpdateSeen(CurUsr.Id, ContentObj.Id);
            }





            foreach (BO.Tags TagObj in ContentObj.Tags)
            {
                var CtnTg = new BO.Contents_Tags();
                CtnTg.Content_Id = ContentObj.Id;
                CtnTg.Tag_Id = TagObj.id;
                BLL.Contents_Tags.Insert(CtnTg);
            }




            foreach (BO.Categories CtgObj in ContentObj.Categories)
            {
                var CtnTg = new BO.Contents_Categories();
                CtnTg.Contents_Id = ContentObj.Id;
                CtnTg.Categories_Id = CtgObj.Id;
                BLL.Contents_Categories.Insert(CtnTg);
            }



            //foreach (BO.Repositories RepObj in ContentObj.Repositories)
            //{
            //    var CtnRP = new BO.Contents_Repository();
            //    CtnRP.Contents_Id = ContentObj.Id;
            //    CtnRP.IsPublished = 1;
            //    CtnRP.Priority = RepObj.Priority;
            //    CtnRP.Repository_Id = RepObj.Id;
            //    BLL.Contents_Repository.Insert(CtnRP);
            //}


            //Insert Repositories:
            foreach (BO.Repositories RepObj in ContentObj.Repositories)
            {
                if (RepObj.Kind == 2)
                {
                    List<BO.Repositories> RepVideos = BLL.Repositories.SelectRepByFilePath(RepObj.FilePath);
                    if (RepVideos.Count == 0)
                    {
                        //Rep.Title = Tmp.Title;
                        RepObj.IsPublished = 1;
                        // RepObj.Description = Tmp.Description;
                        RepObj.Created_By = CurUsr.Id;
                        RepObj.Id = BLL.Repositories.Insert(RepObj).Id;
                    }
                    else
                    {
                        RepObj.Id = RepVideos[0].Id;
                    }
                }

                var CtnRP = new BO.Contents_Repository();
                CtnRP.Contents_Id = ContentObj.Id;
                CtnRP.IsPublished = 1;
                CtnRP.Priority = RepObj.Priority;
                CtnRP.Repository_Id = RepObj.Id;
                BLL.Contents_Repository.Insert(CtnRP);
            }




            return ContentObj;
        }
        public List<BO.Contents> SelectByState(string StateId)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            var bLLContentsSelectByState = BLL.Contents.SelectByState(int.Parse(StateId), "-100", CurUsr.Id.ToString());
            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);
            }

            return bLLContentsSelectByState;
        }
        public BO.Contents SelectById(string ContentId)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());


            var ContentObj = BLL.Contents.SelectById(int.Parse(ContentId));


            ContentObj.Tags = BLL.Contents_Tags.SelectByConetntsId(ContentObj.Id);


            ContentObj.Categories = BLL.Contents_Categories.SelectByConetentsId(ContentObj.Id);



            ContentObj.Repositories = BLL.Contents_Repository.SelectByConetentsId(ContentObj.Id);


            var FlList = BLL.Contents_Flow.SelectByContentId(ContentObj.Id);
            foreach (BO.Contents_Flow flowitem in FlList)
            {
                flowitem.User_From = BLL.Users.SelectById(flowitem.User_FromId.ToString()).Name;
                flowitem.User_To = BLL.Users.SelectById(flowitem.User_ToId.ToString()).Name;
                flowitem.Content_Id = ContentObj.Id;
            }


            if (CurUsr.Id == ContentObj.Owner)
            {
                BLL.Contents_Flow.UpdateSeen(CurUsr.Id, ContentObj.Id);
            }




            ContentObj.Flow = FlList;

            return ContentObj;
        }
        public List<BO.Contents_Categories> UpdatePriority(Stream Data)
        {
            var body = new StreamReader(Data).ReadToEnd();


            var CatList = new List<BO.Contents_Categories>();
            CatList = JsonConvert.DeserializeObject<List<BO.Contents_Categories>>(body);



            foreach (BO.Contents_Categories CtgObj in CatList)
            {
                BLL.Contents_Categories.UpdatePriority(CtgObj);
            }

            return CatList;
        }
        public List<BO.Contents> SelectByStatePublishedCategory(string Published, string Category, string Order)
        {
            var bLLContentsSelectByState = new List<BO.Contents>();
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());
            if (Category == "0")
            {
                bLLContentsSelectByState = BLL.Contents.SelectByState(0, Published, "0");
            }

            if (Category != "0")
            {
                bLLContentsSelectByState = BLL.Contents.SelectByStatePublishedCategory(Published, Category, Order);
            }
            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);


                item.OwnerName = BLL.Users.SelectById(item.Owner.ToString()).Name;


                if (int.Parse(item.Owner.ToString()) < 1)
                {
                    item.OwnerName = BLL.Users_Group.SelectById(int.Parse(item.Owner.ToString().Replace("-", string.Empty))).Title;
                }
                if (int.Parse(item.Owner.ToString()) == 0)
                {
                    item.OwnerName = "Site Items";
                }
            }

            return bLLContentsSelectByState;
        }
        public List<BO.Contents> Search(string query)
        {
            var bLLContentsSelectByState = BLL.Contents.Search(query);
            foreach (BO.Contents item in bLLContentsSelectByState)
            {
                item.Categories = BLL.Contents_Categories.SelectByConetentsId(item.Id);
            }

            return bLLContentsSelectByState;
        }
        public BO.Contents Update(Stream Data, string contentId)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            var body = new StreamReader(Data).ReadToEnd();

            var ContentObj = new BO.Contents();
            ContentObj = JsonConvert.DeserializeObject<BO.Contents>(body);
            ContentObj.Id = int.Parse(contentId);

            //If content is  published dont update publishe date:
            BO.Contents OrigCnt = new BO.Contents();
            OrigCnt = BLL.Contents.SelectById(ContentObj.Id);

            ContentObj.State = 0;
            if (ContentObj.IsPublished == 1)
            {

                if (OrigCnt.IsPublished == 0)
                {
                    ContentObj.Published = DateTime.Now.ToString();
                }
                else
                {
                    ContentObj.Published = OrigCnt.Published;
                }
            }
            else
            {
                if (OrigCnt.IsPublished == 0 && OrigCnt.State != 0)
                {
                    ContentObj.State = -100;
                }
            }

            bool updateDetails = false;


            //Check Update access:
            //if DB is Unpublihsed
            if (OrigCnt.IsPublished == 0)
            {
                //Allow Update Details
                updateDetails = true;

                //To publish
                if (ContentObj.IsPublished == 1)
                {
                    if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
                    {
                        ContentObj.IsPublished = OrigCnt.IsPublished;
                    }
                    else
                    {
                        ContentObj.Published_By = CurUsr.Id;
                    }
                }
            }
            else
            {
                updateDetails = true;
                if (!BLL.Users_Group.UserIsInRole(CurUsr.Id, "Admins#Chief Editors"))
                {
                    updateDetails = false;

                    //Skip unpublish
                    if (ContentObj.IsPublished == 0)
                    {
                        ContentObj.IsPublished = OrigCnt.IsPublished;
                    }
                    if (BLL.Users_Group.UserIsInRole(CurUsr.Id, "Multimedia"))
                    {
                        OrigCnt.Youtube = ContentObj.Youtube;
                    }

                    //Skip All Chnages
                    ContentObj = OrigCnt;
                }      
            }          

            ContentObj.Metadesc = "";
            ContentObj.Owner = CurUsr.Id;         
            ContentObj.Modified_By = CurUsr.Id;

            //Update Main Fields:
            BLL.Contents.Update(ContentObj);

            if (updateDetails)
            {

                //Delete Tags:
                BLL.Contents_Tags.DeleteByConetntId(ContentObj.Id);

                //Insert Tags
                foreach (BO.Tags TagObj in ContentObj.Tags)
                {
                    var CtnTg = new BO.Contents_Tags();
                    CtnTg.Content_Id = ContentObj.Id;
                    CtnTg.Tag_Id = TagObj.id;
                    BLL.Contents_Tags.Insert(CtnTg);
                }

                //Select Cats from data base
                var DBCategories = BLL.Contents_Categories.SelectByConetentsId(ContentObj.Id);

                //Loop Between item from data base and Delete removed items.
                foreach (BO.Categories DBitem in DBCategories)
                {
                    var Exist = false;

                    foreach (BO.Categories Clientitem in ContentObj.Categories)
                    {
                        if (DBitem.Id == Clientitem.Id)
                        {
                            Exist = true;
                        }
                    }
                    if (!Exist)
                    {
                        BLL.Contents_Categories.DeleteById(ContentObj.Id, DBitem.Id);
                    }
                }

                //Insert Items from client which is not exist in DB:
                foreach (BO.Categories Clientitem in ContentObj.Categories)
                {
                    var Exist = false;

                    foreach (BO.Categories DBitem in DBCategories)
                    {
                        if (DBitem.Id == Clientitem.Id)
                        {
                            Exist = true;
                        }
                    }
                    if (!Exist)
                    {
                        var CtnTg = new BO.Contents_Categories();
                        CtnTg.Contents_Id = ContentObj.Id;
                        CtnTg.Categories_Id = Clientitem.Id;
                        BLL.Contents_Categories.Insert(CtnTg);
                    }

                }

                //Delete Repositories:
                BLL.Contents_Repository.DeleteByConetentsId(ContentObj.Id);

                //Insert Repositories:
                foreach (BO.Repositories RepObj in ContentObj.Repositories)
                {
                    if (RepObj.Kind == 2)
                    {
                        List<BO.Repositories> RepVideos = BLL.Repositories.SelectRepByFilePath(RepObj.FilePath);
                        if (RepVideos.Count == 0)
                        {
                            //Rep.Title = Tmp.Title;
                            RepObj.IsPublished = 1;
                            // RepObj.Description = Tmp.Description;
                            RepObj.Created_By = CurUsr.Id;
                            RepObj.Id = BLL.Repositories.Insert(RepObj).Id;
                        }
                        else
                        {
                            RepObj.Id = RepVideos[0].Id;
                        }
                    }

                    var CtnRP = new BO.Contents_Repository();
                    CtnRP.Contents_Id = ContentObj.Id;
                    CtnRP.IsPublished = 1;
                    CtnRP.Priority = RepObj.Priority;
                    CtnRP.Repository_Id = RepObj.Id;
                    BLL.Contents_Repository.Insert(CtnRP);
                }
            }

            return ContentObj;
        }
        public bool UpdateState(string Id, string StateId)
        {
            return BLL.Contents.UpdateState(int.Parse(Id), int.Parse(StateId));
        }
    }
}
