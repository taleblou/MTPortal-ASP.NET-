using System;
using System.Collections.Generic;
using System.Linq;
using PayaBL.Classes;
using PayaBL.Common;
using Telerik.Web.UI;

namespace Paya.Admin.Tabs
{
    public partial class Permission : TabControl
    {

        // Methods
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            _lblPageTitle.Text = @"دسترسی صفحه """ + TabLoad.TabName + @"""";
            SetlstboxesData();
        }

        protected void RadListBox_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            if (e.DestinationListBox == _rdlstboxHaveRole)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    TabRole.Add( int.Parse(item.Value),TabLoad.TabID);
                }
            }
            else if (e.DestinationListBox == _rdlstboxLackingRole)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    TabRole.Delete(TabLoad.TabID, int.Parse(item.Value));
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
            var list = Role.GetAll(PortalSetting.PortalId);
            if (list.Exists(r => (r.RoleID == 15) || (r.RoleID == 13)))
            {
                list = Role.GetAll(PortalSetting.PortalId);
            }
            var list2 = Role.GetTabRolesByTabId(TabLoad.TabID).ToList();
            var lst = list.Except(list2, new RoleComparer()).ToList();
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

    }
}