using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CoreSerivce.BLL
{
    public class Programs
    {
        public static List<BO.Programs> listPrograms(string kind, string status)
        {
            List<BO.Programs> progs = DAL.Programs.listPrograms(kind,status);
            foreach (BO.Programs item in progs)
            {
                item.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + item.Image;

                item.Video = WebConfigurationManager.AppSettings["VideoBaseHost"].ToString() + item.Video.Replace(".mpg",".mp4");
            }
            return progs;
        }
        public static BO.Programs SelectbyId(int ProgId)
        {
            BO.Programs prog = DAL.Programs.SelectbyId(ProgId);
            prog.EpisodesList = BLL.Episodes.listEpisodes(ProgId.ToString());
            prog.Image = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString() + prog.Image;

            prog.Video = WebConfigurationManager.AppSettings["VideoBaseHost"].ToString() + prog.Video.Replace(".mpg", ".mp4");
            prog.VideoThumbnail = prog.Video.Replace(".mp4", ".jpg").Replace(".mpg", ".jpg");
            return prog;
        }
        public static BO.Programs Insert(BO.Programs Prog)
        {
            return DAL.Programs.Insert(Prog);
        }
        public static BO.Programs Update(BO.Programs Prog)
        {
            Prog.Image = Prog.Image.Replace(WebConfigurationManager.AppSettings["ImageBaseHost"].ToString(), "");
            Prog.Video = Prog.Video.Replace(WebConfigurationManager.AppSettings["VideoBaseHost"].ToString(), "").Replace(".mpg", ".mp4");
            return DAL.Programs.Update(Prog);
        }
        public static List<BO.Programs> FrontendlistPrograms(string kind, string count, string order,string status)
        {
            return DAL.Programs.FrontendlistPrograms(kind, count, order,status);
        }
        public static BO.Programs FrontendSelectbyId(int ProgId)
        {
            return DAL.Programs.FrontendSelectbyId(ProgId);
        }
        public static bool UpdateViewCount(int Id)
        {
            return DAL.Programs.UpdateViewCount(Id);
        }
    }
}