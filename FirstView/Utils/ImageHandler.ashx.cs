using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

using FirstView.BusinessLayer;

namespace FirstView.Utils
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int ArtistID = 0;
            int Height = 0;
            int Width = 0;
            string Filename = "";
            string FilePath = "";
            Height  = Convert.ToInt32(HttpContext.Current.Request.QueryString["Height"]);
            Width = Convert.ToInt32(HttpContext.Current.Request.QueryString["Width"]);
            ArtistID = Convert.ToInt32(HttpContext.Current.Request.QueryString["ArtistID"]);

            cArtistWorks aw = new cArtistWorks();
            DataView dv = new DataView();

            dv = aw.ListFirstApprovedWorkItem(ArtistID);
            if (dv.Table.Rows.Count > 0)
            {
                for (int i = 0; i< dv.Table.Rows.Count;i++)
                {
                    Filename = dv.Table.Rows[i]["ImageFileName"].ToString();
                }
            }

            if (Filename.Length > 0)
            { FilePath = HttpContext.Current.Server.MapPath("~/Uploads/Original/" + Filename); }
            else
            { FilePath = HttpContext.Current.Server.MapPath("~/Uploads/Thumbnails/blank.png"); }

            System.Drawing.Image image = System.Drawing.Image.FromFile(FilePath);
            var destRect = new Rectangle(0, 0, Width, Height);
            var destImage = new Bitmap(Width, Height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Byte[] imageBytes;

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }


                ImageCodecInfo[] info;
                info = ImageCodecInfo.GetImageEncoders();
                EncoderParameters encoderParameters;
                encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                MemoryStream imageStream = new MemoryStream();
                destImage.Save(imageStream, info[1], encoderParameters);
                imageStream.Close();
                destImage.Dispose();
                              
                imageBytes = imageStream.GetBuffer();
            }

            context.Response.ContentType = "image/jpeg";
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.BufferOutput = false;
            context.Response.OutputStream.Write(imageBytes, 0, imageBytes.Length);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}