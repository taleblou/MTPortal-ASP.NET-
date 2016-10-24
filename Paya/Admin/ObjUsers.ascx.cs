using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class ObjUsers : ModuleControl
    {

        // Methods
        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            UserName = _txtUserName.Text;
            RoleId = int.Parse(_ddlRole.SelectedItem.Value);
            SetlstboxesData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((AuthId == 0) || (ObjectId == 0))
            {
                PayaTools.AccessDenied();
            }
            _lblAuthTypeTitle.Text = (AuthName == "")
                                         ? LocalResource.GetResourceValueCurrentCulture(
                                             AuthObjBased.GetAuthKeyObjBased(AuthId))
                                         : AuthName;
            PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadControlFont.css");
            SetlstboxesData();
        }

        protected void RadListBox_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            if (e.DestinationListBox == _rdlstboxHaveUser)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    ObjUser.Add(ObjectId, int.Parse(item.Value), AuthId, ModuleConfiguration.ModuleID);
                }
            }
            else if (e.DestinationListBox == _rdlstboxLackingUser)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    ObjUser.Delete(ObjectId, int.Parse(item.Value), AuthId);
                }
            }
            SetlstboxesData();
        }

        private void SetlstboxesData()
        {
            if (!Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).Direction)
            {
                _rdlstboxLackingUser.ButtonSettings.Position = ListBoxButtonPosition.Right;
            }
            else
            {
                _rdlstboxLackingUser.ButtonSettings.Position = ListBoxButtonPosition.Left;
            }
            int portalId = 0;
            if (!PortalSetting.SingleUserBase)
            {
                portalId = PortalSetting.PortalId;
            }
            _ddlRole.DataSource = Role.GetAll("", portalId);
            _ddlRole.DataTextField = "RoleName";
            _ddlRole.DataValueField = "RoleId";
            _ddlRole.DataBind();
            _ddlRole.Items.Insert(0, new ListItem("All", "0"));
            int name = 15;
            ListItem item = _ddlRole.Items.FindByValue(name.ToString());
            if (item != null)
            {
                _ddlRole.Items.Remove(item);
            }
            name = 13;
            item = _ddlRole.Items.FindByValue(name.ToString());
            if (item != null)
            {
                _ddlRole.Items.Remove(item);
            }
            name = 14;
            item = _ddlRole.Items.FindByValue(name.ToString());
            if (item != null)
            {
                _ddlRole.Items.Remove(item);
            }
            List<PortalUser> list = UserRole.GetAllUserInRole(RoleId, portalId, UserName);
            List<PortalUser> list2 = PortalUser.GetobjUsers(ObjectId, AuthId);
            List<PortalUser> lst = list;//.Except<PortalUser>(list2, new PortalUserCompare()).ToList<PortalUser>();
            _rdlstboxLackingUser.DataSource = lst;
            _rdlstboxLackingUser.DataTextField = "UserName";
            _rdlstboxLackingUser.DataValueField = "UserId";
            _rdlstboxLackingUser.DataSortField = "UserName";
            _rdlstboxLackingUser.DataBind();
            _rdlstboxHaveUser.DataSource = list2;
            _rdlstboxHaveUser.DataTextField = "UserName";
            _rdlstboxHaveUser.DataValueField = "UserId";
            _rdlstboxHaveUser.DataSortField = "UserName";
            _rdlstboxHaveUser.DataBind();
        }

        // Properties
        public int AuthId { get; set; }

        public string AuthName { get; set; }

        public int ObjectId { get; set; }

        private int RoleId
        {
            get
            {
                if (ViewState["RoleId"] == null)
                {
                    return 0;
                }
                return (int)ViewState["RoleId"];
            }
            set
            {
                ViewState["RoleId"] = value;
            }
        }

        private string UserName
        {
            get
            {
                if (ViewState["UserName"] == null)
                {
                    return "";
                }
                return ViewState["UserName"].ToString();
            }
            set
            {
                ViewState["UserName"] = value;
            }
        }

    }
}