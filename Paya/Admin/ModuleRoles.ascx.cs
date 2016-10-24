using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class ModuleRoles :ModuleControl
    {
        // Fields
        protected Label _lblAuthTypeTitle;
        protected RadAjaxLoadingPanel _rdlpModuleRole;
        protected RadListBox _rdlstboxHaveRole;
        protected RadListBox _rdlstboxLackingRole;
        protected RadAjaxPanel _rdpModuleRole;

        // Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AuthId == 0)
            {
                PayaTools.AccessDenied();
            }
            _lblAuthTypeTitle.Text = LocalResource.GetResourceValueCurrentCulture(AuthRoleBased.GetAuthKeyRoleBased(AuthId));
            SetlstboxesData();
        }

        protected void RadListBox_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            if (e.DestinationListBox == _rdlstboxHaveRole)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    ModuleRole.Add(AuthId,ModuleConfiguration.ModuleID, int.Parse(item.Value) );
                }
            }
            else if (e.DestinationListBox == _rdlstboxLackingRole)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    ModuleRole.Delete(ModuleConfiguration.ModuleID, int.Parse(item.Value), AuthId);
                }
            }
            SetlstboxesData();
        }

        private void SetlstboxesData()
        {
            if (!Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).Direction)
            {
                _rdlstboxLackingRole.ButtonSettings.Position = ListBoxButtonPosition.Right;
            }
            else
            {
                _rdlstboxLackingRole.ButtonSettings.Position = ListBoxButtonPosition.Left;
            }
            List<Role> list = Role.GetTabRolesByTabId(ModuleConfiguration.TabID).ToList();
            if (list.Exists(o => o.RoleID == 15))
            {
                list = Role.GetAll(PortalSetting.PortalId);
            }
            List<Role> list2 = Role.GetRolesOfModule(ModuleConfiguration.ModuleID, AuthId);
            var lst = list.Except(list2, new RoleComparer());
            _rdlstboxLackingRole.DataSource = lst;
            _rdlstboxLackingRole.DataTextField = "RoleName";
            _rdlstboxLackingRole.DataValueField = "RoleId";
            _rdlstboxLackingRole.DataSortField = "RoleName";
            _rdlstboxLackingRole.DataBind();
            _rdlstboxHaveRole.DataSource = list2;
            _rdlstboxHaveRole.DataTextField = "RoleName";
            _rdlstboxHaveRole.DataValueField = "RoleId";
            _rdlstboxHaveRole.DataSortField = "RoleName";
            _rdlstboxHaveRole.DataBind();
        }

        // Properties
        public int AuthId { get; set; }

    }
}