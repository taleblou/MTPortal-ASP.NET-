using System;
using System.Drawing.Imaging;
using System.Web.UI;
using PayaBL.Common;
using Telerik.Web.UI;

namespace Paya.Captcha
{
    public partial class CaptchaHandler : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = PayaTools.QueryString("Id");
            if (!string.IsNullOrEmpty(id))
            {
                int w = PayaTools.QueryStringInt("W", 0);
                w = (w == 0) ? 200 : w;
                int h = PayaTools.QueryStringInt("H", 0);
                h = (h == 0) ? 50 : h;
                CaptchaImage ci = new CaptchaImage(Session["CaptchaImageText" + id].ToString(), w, h, "Century Schoolbook");
                Response.Clear();
                Response.ContentType = "image/jpeg";
                ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg);
                ci.Dispose();
            }
        }

    }
}