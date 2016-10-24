using System;
using System.Web.UI;
using PayaBL.Common;

namespace Paya.Captcha
{
    public partial class CaptchaControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CaptchaImageText" + ID] = (RandomCount == 0)
                                                            ? PayaTools.GenerateStringWithDigit(6)
                                                            : PayaTools.GenerateStringWithDigit(RandomCount);
            }
        }

        public bool ValidateCaptcha()
        {
            if (_txtCode.Text != Session["CaptchaImageText" + ID].ToString())
            {
                _txtMessage.Text = Resources.Paya1.CaptchaControl_ValidateCaptcha_wrongCaptchaCode;
                Session["CaptchaImageText"] = PayaTools.GenerateStringWithDigit(6).ToLower();
                return false;
            }
            return true;
        }

        // Properties
        public int HeightCaptcha { get; set; }

        public int RandomCount { get; set; }

        public int WidthCaptcha { get; set; }

    }
}