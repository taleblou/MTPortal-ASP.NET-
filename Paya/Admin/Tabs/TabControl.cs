using System;
using System.Web.UI;
using PayaBL.Common;

namespace Paya.Admin
{
    public class TabControl : UserControl
    {
        // Methods
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if ((TabLoad == null) || string.IsNullOrEmpty(Page.User.Identity.Name))
            {
                PayaTools.AccessDenied();
            }
        }

        // Properties
        public PayaBL.Classes.Tab TabLoad { get; set; }
    }


}
