using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using FirstView.BusinessLayer;

namespace FirstView.Utils
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UploadFolder = "";
            string OriginalFileName = "";
            string NewFileName = "";
            string UniqueID = "";

            if (Request.QueryString["UniqueID"] != null)
            {
                if (Convert.ToString(Request.QueryString["UniqueID"]) == "EventCover")
                {
                    UniqueID = Guid.NewGuid().ToString();
                }
                else
                {
                    UniqueID = Request.QueryString["UniqueID"].ToString();
                }
               
                cTempImages ti = new cTempImages();

                UploadFolder = Server.MapPath("~/Uploads");
                foreach (string s in Request.Files)
                {
                    HttpPostedFile file = Request.Files[s];

                    int fileSizeInBytes = file.ContentLength;
                    OriginalFileName = file.FileName;                    

                    string fileExtension = Path.GetExtension(OriginalFileName);
                    NewFileName = UniqueID + fileExtension;

                    string savedFileName = Path.Combine(UploadFolder, "Original", NewFileName);
                        file.SaveAs(savedFileName);

                    // Resize Image - Thumbanil
                    ResizeImages(NewFileName, UploadFolder + @"\Original\", UploadFolder + @"\Thumbnails\", 150);
                    // Resize Image - New Size
                    ResizeImages(NewFileName, UploadFolder + @"\Original\", UploadFolder + @"\Resized\", 600);
                    // Resize Image - Preview
                    ResizeImagesPreview(NewFileName, UploadFolder + @"\Original\", UploadFolder + @"\Preview\");

                    ti.Add(OriginalFileName, NewFileName, UniqueID);
                }
            }
        }

        private void ResizeImages(string FileName, string SourceDir, string DestDir, int ImageWidth)
        {
            int OrigWidth = 0;
            int OrigHeight = 0;
            int NewWidth = 0;
            int NewHeight = 0;
            double lnRatio = 0;
            //double TempWidth = 0;
            double TempHeight = 0;
            //int ImageHeight = 0;
            string FilePath = "";
            string FileSavePath = "";

            FilePath = SourceDir + FileName;

            System.Drawing.Image image = System.Drawing.Image.FromFile(FilePath);
            OrigWidth = image.Width;
            OrigHeight = image.Height;

            lnRatio = (double)OrigWidth / OrigHeight;

            NewWidth = ImageWidth;
            TempHeight = NewWidth / lnRatio;
            NewHeight = (Int32)(TempHeight);

            //NewHeight = ImageHeight;
            //TempWidth = NewHeight * lnRatio;
            //NewWidth = (Int32)(TempWidth);

            System.Drawing.Image thumbnail = new Bitmap(NewWidth, NewHeight);
            Graphics graphic = Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            graphic.DrawImage(image, 0, 0, NewWidth, NewHeight);
            ImageCodecInfo[] info;
            info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            FileSavePath = DestDir + FileName;
            thumbnail.Save(FileSavePath, info[1], encoderParameters);
            thumbnail.Dispose();
            graphic.Dispose();
            image.Dispose();
        }

        private void ResizeImagesPreview(string FileName, string SourceDir, string DestDir)
        {
            int Height = 60;
            int Width = 60;
            string FilePath = "";
            string FileSavePath = "";

            FilePath = SourceDir + FileName;

            System.Drawing.Image image = System.Drawing.Image.FromFile(FilePath);
            var destRect = new Rectangle(0, 0, Width, Height);
            var destImage = new Bitmap(Width, Height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

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
                FileSavePath = DestDir + FileName;
                destImage.Save(FileSavePath, info[1], encoderParameters);
                destImage.Dispose();
            }
        }
    }
}