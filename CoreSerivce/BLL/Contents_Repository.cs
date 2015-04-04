using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace CoreSerivce.BLL
{
    public class Contents_Repository
    {
        public static BO.Contents_Repository Insert(BO.Contents_Repository ContentsRepObj)
        {
            return DAL.Contents_Repository.Insert(ContentsRepObj);
        }

        public static List<BO.Repositories> SelectByConetentsId(int Content_Id)
        {
            List<BO.Repositories> Cntns= DAL.Contents_Repository.SelectByConetentsId(Content_Id);

            foreach (BO.Repositories item in Cntns)
            {
                if(item.Kind==2)
                {
                    item.Thumbnail = WebConfigurationManager.AppSettings["VideoBaseHost"].ToString() + item.FilePath.Replace(".mpg", ".mp4").Replace(".mp4", ".jpg");
                }
                 if(item.Kind==1)
                {
                    item.Thumbnail = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.FilePath;
                }         
            }        
            return Cntns;
        }
        public static void DeleteByConetentsId(int Content_Id)
        {
            DAL.Contents_Repository.DeleteByConetentsId(Content_Id);
        }
    }
}
