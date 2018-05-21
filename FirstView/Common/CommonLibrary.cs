using System;
using System.Web.UI.WebControls;
namespace FirstView.Common
{
    /// <summary>
    /// Summary description for CommonLibrary
    /// </summary>
    public class CommonLibrary
    {
        public CommonLibrary()
        {
        }

        public void FormatGridView(GridView gv)
        {
            gv.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D7B9D");
            gv.HeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            gv.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F7F6F3");
            gv.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D7B9D");
            gv.PagerStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            gv.PageSize = 20;
            gv.PagerSettings.Position = PagerPosition.TopAndBottom;
        }

        public string FormatDate(DateTime TempDate)
        {
            string TempDay = "";
            string TempMonth = "";
            string TempYear = "";

            TempDay = TempDate.Day.ToString();
            TempMonth = TempDate.ToString("MMMM");
            TempYear = TempDate.Year.ToString();

            return TempDay + " " + TempMonth + " " + TempYear;
        }

        public string FormatDateTimeHH(DateTime TempDate)
        {
            string TempHour = "";

            TempHour = TempDate.Hour.ToString("#00");

            return TempHour;
        }

        public string FormatDateTimeMM(DateTime TempDate)
        {
            string TempMin = "";

            TempMin = TempDate.Minute.ToString("#00");

            return TempMin;
        }

        public string FormatDateTime(DateTime TempDate)
        {
            string TempDay = "";
            string TempMonth = "";
            string TempYear = "";

            TempDay = TempDate.Day.ToString();
            TempMonth = TempDate.ToString("MMMM");
            TempYear = TempDate.Year.ToString();

            return TempDay + " " + TempMonth + " " + TempYear + " " + TempDate.Hour.ToString("#00") + ":" + TempDate.Minute.ToString("#00");
        }

        public string FormatTimeOnly(DateTime TempDate)
        {
            return TempDate.Hour.ToString("#00") + ":" + TempDate.Minute.ToString("#00");
        }

        public string FormatTime(string TempTime)
        {
            string RetTime = "";

            RetTime = TempTime.Substring(0, 5);

            return RetTime;
        }

        public string FormatTimeHH(string TempTime)
        {
            string RetTime = "";

            RetTime = TempTime.Substring(0, 2);

            return RetTime;
        }

        public string FormatTimeMM(string TempTime)
        {
            string RetTime = "";

            RetTime = TempTime.Substring(3, 2);

            return RetTime;
        }

        public string CheckExtType(string FileType)
        {
            string imgName = "";

            switch (FileType.ToUpper())
            {
                case ".DOC":
                case ".DOT":
                case ".RTF":
                case ".DOCX":
                    imgName = "ico_word.gif";
                    break;
                case ".MDB":
                    imgName = "ico_access.gif";
                    break;
                case ".XLS":
                case ".XLT":
                case ".XLSX":
                    imgName = "ico_excel.gif";
                    break;
                case ".MSG":
                case ".PST":
                case ".OST":
                    imgName = "ico_outlook.gif";
                    break;
                case ".PPT":
                case ".PPS":
                    imgName = "ico_powerpoint.gif";
                    break;
                case ".MPP":
                    imgName = "ico_project.gif";
                    break;
                case ".PUB":
                    imgName = "ico_publisher.gif";
                    break;
                case ".MPG":
                case ".MPEG":
                case ".AVI":
                case ".WMV":
                case ".ASF":
                    imgName = "ico_media.gif";
                    break;
                case ".MP3":
                case ".WAV":
                case ".MID":
                    imgName = "ico_sound.gif";
                    break;
                case ".BMP":
                case ".GIF":
                case ".JPG":
                case ".JPEG":
                case ".TIF":
                case ".PNG":
                    imgName = "ico_pic.gif";
                    break;
                case ".ZIP":
                    imgName = "ico_zip.gif";
                    break;
                case ".PDF":
                    imgName = "ico_pdf.gif";
                    break;
                case ".TXT":
                    imgName = "ico_notepad.gif";
                    break;
                case ".VSD":
                    imgName = "ico_visio.gif";
                    break;
                case ".MHT":
                    imgName = "ico_mht.gif";
                    break;
                default:
                    imgName = "ico_other.gif";
                    break;
            }
            return imgName;
        }
    }
}