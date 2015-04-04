using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Net;

namespace CoreSerivce.PL
{
    [MessageContract(IsWrapped = false)]
    [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Comments : IComments
    {
        public List<BO.Comments> SelectAll()
        {
            return BLL.Comments.SelectAll();
        }

        public BO.Comments Update(Stream Data, string ID)
        {
            var CommentObj = new BO.Comments();
            try
            {
                var body = new StreamReader(Data).ReadToEnd();



                CommentObj = JsonConvert.DeserializeObject<BO.Comments>(body);

                var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

                CommentObj.Published_By = CurUsr.Id;
                CommentObj.Id = int.Parse(ID);
                CommentObj = BLL.Comments.Update(CommentObj);
            }
            catch (Exception Exp)
            {
                var customError = new BO.ErrorHandling(Exp.Source, Exp.Message);
                throw new WebFaultException<BO.ErrorHandling>(customError, HttpStatusCode.MethodNotAllowed);
            }


            return CommentObj;
        }
        public List<BO.Comments> SelectSearch(string query)
        {
            return BLL.Comments.SelectSearch(query.Trim());
        }
        public BO.Comments SelectById(string id)
        {
            return BLL.Comments.SelectById(id);
        }
    }
}
