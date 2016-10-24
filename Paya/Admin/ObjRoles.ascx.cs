using System;
using System.Collections.Generic;
using System.Linq;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class ObjRoles : ModuleControl
    {

        // Methods
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
            if (e.DestinationListBox == _rdlstboxHaveRole)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    var t =ObjRole.Add(AuthId,ObjectId, int.Parse(item.Value),  ModuleConfiguration.ModuleID);
                }
            }
            else if (e.DestinationListBox == _rdlstboxLackingRole)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                   var t = ObjRole.Delete(ObjectId, int.Parse(item.Value), AuthId);
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
            List<Role> list = Role.GetRolesAllOfModule(ModuleConfiguration.ModuleID);
            if (list.Exists(r => (r.RoleID == 15) || (r.RoleID == 13)))
            {
                list = Role.GetAll(PortalSetting.PortalId);
            }
            List<Role> list2 = ObjRole.GetobjRoles(ObjectId, AuthId);
            List<Role> lst = list.Except(list2, new RoleComparer()).ToList();
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

        public string AuthName { get; set; }

        public int ObjectId { get; set; }

    }
}