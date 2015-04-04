using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CoreSerivce.BLL
{
    public class Episodes
    {
        public static List<BO.Episodes> listEpisodes(string programId)
        {
            List<BO.Episodes> epsList = DAL.Episodes.listEpisodes(programId);
            foreach (BO.Episodes item in epsList)
            {
                item.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.Image;
                item.Video = WebConfigurationManager.AppSettings["VideoBaseHost"].ToString() + item.Video;
            }
            return epsList;
        }
        public static BO.Episodes SelectbyId(int EpisodeId)
        {
            BO.Episodes eps = DAL.Episodes.SelectbyId(EpisodeId);
            eps.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + eps.Image;
            eps.Video = WebConfigurationManager.AppSettings["VideoBaseHost"].ToString() + eps.Video.Replace(".mpg", ".mp4");
            eps.VideoThumbnail = eps.Video.Replace(".mp4", ".jpg").Replace(".mpg", ".jpg");
            return eps;
        }
        public static BO.Episodes Insert(BO.Episodes Eps)
        {
            return DAL.Episodes.Insert(Eps);
        }
        public static BO.Episodes Update(BO.Episodes Eps)
        {
            Eps.Image = Eps.Image.Replace(WebConfigurationManager.AppSettings["ImageBaseHost"].ToString(), "");
            Eps.Video = Eps.Video.Replace(WebConfigurationManager.AppSettings["VideoBaseHost"].ToString(), "");
            return DAL.Episodes.Update(Eps);
        }
        public static List<BO.Episodes> listEpisodesByPid(string programId, string count, string ordering)
        {
            return DAL.Episodes.listEpisodesByPid(programId, count, ordering);
        }
        public static BO.Episodes FrontendSelectbyId(int EpisodeId)
        {
            return DAL.Episodes.FrontendSelectbyId(EpisodeId);            
        }
        public static bool UpdateViewCount(int Id)
        {
            return DAL.Episodes.UpdateViewCount(Id);
        }
        public static List<BO.Episodes> FrontendListEpisodes(string kinds, string count, string order)
        {
            return DAL.Episodes.FrontendListEpisodes(kinds, count, order);
        }
    }
}