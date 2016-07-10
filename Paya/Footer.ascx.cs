using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayaBL.Common;

namespace Paya
{
    public partial class Footer : UserControl
    {
        // Fields
        protected PlaceHolder _plhFooter;

        // Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            string bannerPath = PayaTools.SetStyle(this.Context.User.Identity.Name, false) + "/Footer/Footer.ascx";
            Control ctrl = Page.LoadControl(bannerPath);
            ctrl.ID = "Footer_ctrl";
            _plhFooter.Controls.Add(ctrl);
        }

    }
}