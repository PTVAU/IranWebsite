using System;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Hosting;

namespace CoreSerivce.Utilities
{
    public class ThumbnailGenerator
    {
        public static string Generate(string path,string fullPath, int thumbWidth, int thumbHeight, string fileSuffix)
        {
            string OutPutValue = "";

            string savePath = "";
           // string[] OrigPath = fullPath.Split('/');
            FileInfo ImageFile = new FileInfo(fullPath);

            DirectoryInfo ThumbDir = new DirectoryInfo(path.Replace("Original", "Thumbnail"));
            if (!ThumbDir.Exists)
            {
                ThumbDir.Create();
            }


            if (ImageFile.Exists)
            {

                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(fullPath);

                // Calculate thumbnail Height
                if (thumbHeight > 0 && thumbWidth > 0)
                {
                    int tmbHeight = Convert.ToInt32(Math.Round((Convert.ToDouble(thumbWidth) / originalImage.Width) * originalImage.Height));
                    if (tmbHeight > thumbHeight)
                        thumbWidth = Convert.ToInt32(Math.Round((Convert.ToDouble(thumbHeight) / originalImage.Height) * originalImage.Width));
                    else
                        thumbHeight = tmbHeight;

                }
                else if (thumbWidth > 0)
                {
                    thumbHeight = Convert.ToInt32(Math.Round((Convert.ToDouble(thumbWidth) / originalImage.Width) * originalImage.Height));
                }
                else if (thumbHeight > 0)
                {
                    thumbWidth = Convert.ToInt32(Math.Round((Convert.ToDouble(thumbHeight) / originalImage.Height) * originalImage.Width));
                }
                else
                {
                    thumbHeight = 1;
                    thumbWidth = 1;
                }
                // create thumbnail image
                System.Drawing.Image thumbnail = new Bitmap(thumbWidth, thumbHeight, originalImage.PixelFormat);
                Graphics oGraphic = Graphics.FromImage(thumbnail);

                //setting drawing properties
                oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                oGraphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                Rectangle oRectangle = new Rectangle(0, 0, thumbWidth, thumbHeight);
                oGraphic.DrawImage(originalImage, oRectangle);

                savePath = fullPath.Replace("Original", "Thumbnail").Replace(".jpg", "_" + fileSuffix+".jpg");
                //savePath = HostingEnvironment.MapPath("/Thumbnail/" + OrigPath[0].ToString() + "/" + Path.GetFileNameWithoutExtension(ImageFile.FullName) + "_" + fileSuffix.ToString() + ".jpg");
                FileInfo DestFile = new FileInfo(savePath);
                if (!DestFile.Exists)
                {
                    thumbnail.Save(savePath, ImageFormat.Jpeg);
                    thumbnail.Dispose();
                }
                originalImage.Dispose();
                thumbnail.Dispose();
                OutPutValue = savePath;


            }

            //  ImageFile.
            return OutPutValue;
        }
    }
}