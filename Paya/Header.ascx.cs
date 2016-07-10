using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayaBL.Common;

namespace Paya
{
    public partial class Header : System.Web.UI.UserControl
    {
        // Fields
        protected PlaceHolder _plhHeader;

        // Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            string bannerPath = PayaTools.SetStyle(Context.User.Identity.Name, false) + "/Header/Header.ascx";
            Control ctrl = Page.LoadControl(bannerPath);
            ctrl.ID = "Header_ctrl";
            _plhHeader.Controls.Add(ctrl);
        }

    }
}