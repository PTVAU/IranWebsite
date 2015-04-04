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
    public class Tags : ITags
    {
        public BO.Tags Insert(Stream Data)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());


            var body = new StreamReader(Data).ReadToEnd();

            var TagObj = new BO.Tags();
            TagObj = JsonConvert.DeserializeObject<BO.Tags>(body);
            TagObj.Modified_By = TagObj.Created_By = CurUsr.Id;
            TagObj = BLL.Tags.Insert(TagObj);

            return TagObj;
        }
        public BO.Tags Update(Stream Data)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            var body = new StreamReader(Data).ReadToEnd();

            var TagObj = new BO.Tags();
            TagObj = JsonConvert.DeserializeObject<BO.Tags>(body);
            TagObj.Modified_By = CurUsr.Id;
            TagObj = BLL.Tags.Update(TagObj);

            return TagObj;
        }
        public List<BO.Tags> SelectAll()
        {
            return BLL.Tags.SelectAll();
        }
        public List<BO.Tags> SelectSearch(string query)
        {
            return BLL.Tags.SelectSearch(query.Trim());
        }
    }
}
