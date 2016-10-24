using System;
using System.Threading;
using System.Web.UI;
using PayaBL.Classes;
using PayaBL.Common;

namespace PayaBL.Control
{

    public class PortalPage : Page
    {
        #region Fields
        protected long EndTime;
        protected long StartTime;
        #endregion

        #region Methods

        protected override void InitializeCulture()
        {
            PayaTools.SetLanguage(PayaTools.QueryStringInt("TabID", -1), PayaTools.QueryStringInt("ModuleID", -1));
            Culture = Thread.CurrentThread.CurrentCulture.ToString();
            UICulture = Thread.CurrentThread.CurrentUICulture.ToString();
            base.InitializeCulture();
        }

        protected override void OnPreRender(EventArgs e)
        {
            Session["REFRESH_CHECK_GUID"] = Guid.NewGuid().ToString();
            ViewState["REFRESH_CHECK_GUID"] = Session["REFRESH_CHECK_GUID"];
            base.OnPreRender(e);
        }

        #endregion

        #region Properties

        public PortalUser CurrentUser
        {
            get
            {
                if (CurrentUserId.HasValue)
                {
                    return PortalUser.GetSingleByID(CurrentUserId.Value);
                }
                return null;
            }
        }

        public int? CurrentUserId
        {
            get
            {
                if (User.Identity.Name != "")
                {
                    return  int.Parse(User.Identity.Name);
                }
                return null;
            }
        }

        protected bool Ispaya
        {
            get
            {
                return ((Session["REFRESH_CHECK_GUID"] != null) && (ViewState["REFRESH_CHECK_GUID"] != null)) &&
                       !Session["REFRESH_CHECK_GUID"].ToString().Equals(ViewState["REFRESH_CHECK_GUID"].ToString());
            }
        }

        #endregion
    }

}
