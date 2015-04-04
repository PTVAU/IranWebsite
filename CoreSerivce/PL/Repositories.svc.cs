using CoreSerivce.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace CoreSerivce.PL
{
    [MessageContract(IsWrapped = false)]
    [AspNetCompatibilityRequirements(
    RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Repositories : IRepositories
    {
        public BO.Image_Tmp UploadImage(Stream Data)
        {
            var Tmp = new BO.Image_Tmp();
            Tmp.FileHost = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString();

            var Rootpath = WebConfigurationManager.AppSettings["ImageSavePath"].ToString() + "\\Tmp\\";
            var FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-FF");
            var FullFileName = Rootpath + FileName;
           

            //if (!Directory.Exists(Rootpath))
            //{
            //    Directory.CreateDirectory(Rootpath);
            //}
            var parser = new MultipartParser(Data);
            if (parser.Success && parser.Extention != "UNKNOWN")
            {
                FileName += parser.Extention;
                FullFileName += parser.Extention;
                Tmp.FullFileName = WebConfigurationManager.AppSettings["ImageBaseHost"].ToString().Replace("/original","") + "/TMP/" + FileName;
                Tmp.Src = WebConfigurationManager.AppSettings["ImageSavePath"].ToString() + "\\TMP\\" + FileName;

                File.WriteAllBytes(FullFileName, parser.FileContents);


                Tmp.HeightCrop = 0;
                Tmp.WidthCrop = 0;
                Tmp.X = 0;
                Tmp.Y = 0;
                Tmp.ContentType = parser.ContentType;


                var Bmp = new System.Drawing.Bitmap(FullFileName);

                Tmp.HeightOriginal = Bmp.Height;
                Tmp.WidthOriginal = Bmp.Width;
                Bmp.Dispose();

                if (Tmp.HeightOriginal >= 365 && Tmp.WidthOriginal >= 650)
                {
                    Tmp.success = true;

                    Tmp.FileName = FileName;
                   // Tmp.FullFileName = FullFileName;
                    Tmp.ContentType = parser.ContentType;
                }
                else
                {
                    Tmp.success = false;
                    Tmp.error = "File is too small [ Min Width:650 , Min Height:365 ] ";

                    File.Delete(FullFileName);

                    Tmp.FileName = string.Empty;
                    Tmp.FullFileName = string.Empty;
                    Tmp.ContentType = parser.ContentType;
                }
            }
            else
            {
                Tmp.success = false;
            }


            return Tmp;
        }
        public BO.Repositories CropImage(Stream Data)
        {
            var CurUsr = BLL.Users.SelectBySessionKey(WebOperationContext.Current.IncomingRequest.Headers["Authorization"].Trim());

            var body = new StreamReader(Data).ReadToEnd();
            var Tmp = new BO.Image_Tmp();
            Tmp = JsonConvert.DeserializeObject<BO.Image_Tmp>(body);

            var Rep = new BO.Repositories();

            var SrcImg = Image.FromFile(Tmp.FullFileName);

            var crop = new Rectangle(Tmp.X, Tmp.Y, Tmp.WidthCrop, Tmp.HeightCrop);

            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(SrcImg, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }

            SrcImg.Dispose();

            var Rootpath = WebConfigurationManager.AppSettings["ImageSavePath"].ToString()+"\\Original\\";
            var datedir = DateTime.Now.ToString("yyyyMMdd");
            var FileName = DateTime.Now.ToString("HHmmssFF");
            var FullFileName = Rootpath + datedir +"\\"+ FileName + ".jpg";
           
            if (!Directory.Exists(Rootpath + datedir))
            {
                Directory.CreateDirectory(Rootpath + datedir);
            }
            bmp.Save(FullFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            Rep.FilePath = datedir + "/" + FileName + ".jpg";
            Rep.Kind = 1;
            Rep.Title = Tmp.Title;
            Rep.IsPublished = 1;
            Rep.Id = 0;
            Rep.Description = Tmp.Description;
            Rep.Created_By = CurUsr.Id;
            bmp.Dispose();

            //Genarate Thumbnails:
            var Orig = datedir + "/" + FileName + ".jpg";
            CoreSerivce.Utilities.ThumbnailGenerator.Generate(Rootpath+"\\"+datedir, FullFileName, 880, 495, "xl");
            CoreSerivce.Utilities.ThumbnailGenerator.Generate(Rootpath + "\\" + datedir, FullFileName, 430, 241, "l");
            CoreSerivce.Utilities.ThumbnailGenerator.Generate(Rootpath + "\\" + datedir, FullFileName, 280, 158, "m");
            CoreSerivce.Utilities.ThumbnailGenerator.Generate(Rootpath + "\\" + datedir, FullFileName, 208, 115, "s");
            CoreSerivce.Utilities.ThumbnailGenerator.Generate(Rootpath + "\\" + datedir, FullFileName, 80, 45, "xs");


            File.Delete(Tmp.FullFileName);
            Rep = BLL.Repositories.Insert(Rep);
            foreach (BO.Tags item in Tmp.Tags)
            {
                var Tg = new BO.Repository_Tags() { Repository_Id = Rep.Id, Tag_Id = item.id };
                BLL.Repository_Tags.Insert(Tg);
            }


            return Rep;
        }
        public List<BO.Repositories> SelectAll()
        {
            var RepositoriesList = BLL.Repositories.SelectAll();
            foreach (BO.Repositories item in RepositoriesList)
            {
                item.Tags = BLL.Repository_Tags.SelectByRepositoryId(item.Id);
            }

            return RepositoriesList;
        }
        public List<BO.Repositories> Search(string Keyword)
        {
            var RepositoriesList = BLL.Repositories.Search(Keyword);


            foreach (BO.Repositories item in RepositoriesList)
            {
                item.Tags = BLL.Repository_Tags.SelectByRepositoryId(item.Id);
            }

            return RepositoriesList;
        }
        public BO.Repositories Update(Stream Data,string repId)
        {
            var body = new StreamReader(Data).ReadToEnd();
            var Rep = new BO.Repositories();
            Rep = JsonConvert.DeserializeObject<BO.Repositories>(body);

            Rep.Id = int.Parse(repId);

            if (Rep.Tags.Count > 0)
            {
                BLL.Repository_Tags.DeleteByRepositoryId(Rep.Id);
            }



            BLL.Repositories.Update(Rep);



            foreach (BO.Tags item in Rep.Tags)
            {
                var Tg = new BO.Repository_Tags() { Repository_Id = Rep.Id, Tag_Id = item.id };
                BLL.Repository_Tags.Insert(Tg);
            }

            return Rep;
        }
    }
}
