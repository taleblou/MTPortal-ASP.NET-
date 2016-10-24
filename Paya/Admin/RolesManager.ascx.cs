using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class RolesManager : ModuleControl
    {
        // Fields
        protected Button _btnBackRole;
        protected Label _lblFail;
        protected Label _lblSucc;
        protected Label _lblUserInRole;
        protected Panel _pnlRoles;
        protected Panel _pnlUsers;
        protected RadAjaxLoadingPanel _rdlpRolesManager;
        protected RadAjaxPanel _rdpRolesManager;
        protected RadGrid _rgAllUser;
        protected RadGrid _rgRoles;
        protected RadGrid _rgUsersInRole;
        protected RadScriptBlock scriptBlock;

        // Methods
        protected void _btnBackRole_Click(object sender, EventArgs e)
        {
            PageStatus = PageState.Role;
            SetPageControl();
            SetPageData();
        }

        protected void _rgAllUser_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var list2 = UserRole.GetAllUserInRole(RoleId, PortalSetting.PortalId, "");
            var lst = PortalUser.GetAll(PortalSetting.PortalId).Except(list2, new PortalUserCompare()).ToList();
            _rgAllUser.DataSource = lst;
        }

        protected void _rgRoles_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int roleId = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["RoleId"].ToString());
            try
            {
                Role role = Role.GetSingleById(roleId);
                if (role != null)
                {
                    string roleKey = role.RoleKey;
                    foreach (LocalResource resource in LocalResource.GetResourceValues(roleKey))
                    {
                        resource.Delete();
                    }
                    if (role.Delete())
                    {
                        DisplayMessage("نقش  با موفقیت حذف شد", false);
                    }
                    else
                    {
                        DisplayMessage("در حذف نقش مشکلی پیش آمده است", true);
                    }
                    SetPageData();
                }
                else
                {
                    DisplayMessage("در حذف نقش مشکلی پیش آمده است", true);
                }
            }
            catch
            {
                DisplayMessage("در حذف نقش خطایی رخ داده است", true);
            }
        }

        protected void _rgRoles_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (!Page.IsValid)
            {
                DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
            }
            else
            {
                var insertedItem = (GridEditFormInsertItem)e.Item;
                string roleKey = ((TextBox)insertedItem.FindControl("_txtRoleKey")).Text;
                var rp = (Repeater)insertedItem.FindControl("_rpLangRole");
                try
                {
                    int i = Role.Add( PortalSetting.PortalId,roleKey);
                    if (i > 0)
                    {
                        bool show = true;
                        RoleId = i;
                        foreach (RepeaterItem item in rp.Items)
                        {
                            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                            {
                                var txtRoleName = (TextBox)item.FindControl("_txtRoleName");
                                var lblLanguageId = (Label)item.FindControl("_lblLanguageId");
                                if (((txtRoleName.Text != "") && (lblLanguageId.Text != "")) && (LocalResource.AddLocaleResource(int.Parse(lblLanguageId.Text), roleKey, txtRoleName.Text) <= 0))
                                {
                                    show = false;
                                }
                            }
                        }
                        if (show)
                        {
                            DisplayMessage("نقش مورد نظر با موفقیت ثبت گردید.", false);
                        }
                        else
                        {
                            DisplayMessage("در ثبت نقش مشکلی پیش آمده است", true);
                        }
                        
                    }
                }
                catch
                {
                    DisplayMessage("در ثبت نقش خطایی رخ داده است", true);
                }
                finally
                {
                    int plCount = Enumerable.Count(PortalLanguage.GetAllEnabledLanguagePortal(),
                                                   o => (o.PortalID == PortalSetting.PortalId));
                    if (LocalResource.GetResourceValues(roleKey).Count != plCount)
                    {
                        foreach (LocalResource resource in LocalResource.GetResourceValues(roleKey))
                        {
                            resource.Delete();
                        }
                        Role.Delete(RoleId);
                    }
                }
                PageStatus = PageState.UserRole;
                
                SetPageControl();
                SetPageData();
            }
        }

        protected void _rgRoles_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "User")
            {
                PageStatus = PageState.UserRole;
                RoleId = int.Parse(_rgRoles.MasterTableView.DataKeyValues[e.Item.ItemIndex]["RoleId"].ToString());
                SetPageControl();
                SetPageData();
            }
        }

        protected void _rgRoles_ItemDataBound(object sender, GridItemEventArgs e)
        {
            Role obj;
            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                var editFormItem = e.Item as GridEditFormItem;
                var txt = (TextBox)editFormItem.FindControl("_txtRoleKey");
                obj = e.Item.DataItem as Role;
                if ((obj != null) && (txt != null))
                {
                    txt.Enabled = false;
                    RoleKey = txt.Text = obj.RoleKey;
                }
                var rp = (Repeater)editFormItem.FindControl("_rpLangRole");
                if (rp != null)
                {
                    Caching.Remove("AllLocalResource");
                    rp.DataSource = (from l in PortalLanguage.GetAllEnabledLanguagePortal()
                                     where l.PortalID == PortalSetting.PortalId
                                     select Language.GetSingleByID(l.LanguageID)).ToList<Language>();
                    rp.DataBind();
                }
            }
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                obj = e.Item.DataItem as Role;
                var imgbtn = (ImageButton)e.Item.FindControl("_imgbtnDelete");
                if (imgbtn != null)
                {
                    imgbtn.OnClientClick = "javascript:return confirm('آیا شما مطمئن هستید؟');";
                }
                if ((obj != null) && (((obj.RoleID == 15) || (obj.RoleID == 13)) || (obj.RoleID == 14)))
                {
                    if (imgbtn != null)
                    {
                        imgbtn.Visible = false;
                    }
                    var lnkbtn = (LinkButton)e.Item.FindControl("_lnkbtnUsers");
                    if (lnkbtn != null)
                    {
                        lnkbtn.Visible = false;
                    }
                }
            }
        }

        protected void _rgRoles_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            _rgRoles.DataSource = Role.GetAll("", PortalSetting.PortalId);
        }

        protected void _rgRoles_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (!Page.IsValid)
            {
                DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
            }
            else
            {
                try
                {
                    var editedItem = e.Item as GridEditableItem;
                    var rp = (Repeater)editedItem.FindControl("_rpLangRole");
                    string roleKey = ((TextBox)editedItem.FindControl("_txtRoleKey")).Text;
                    bool show = true;
                    foreach (RepeaterItem item in rp.Items)
                    {
                        if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                        {
                            var txtRoleName = (TextBox)item.FindControl("_txtRoleName");
                            var lblLanguageId = (Label)item.FindControl("_lblLanguageId");
                            if ((txtRoleName.Text != "") && (lblLanguageId.Text != ""))
                            {
                                show &= LocalResource.UpdateLocaleResource(int.Parse(lblLanguageId.Text), roleKey, txtRoleName.Text);
                            }
                        }
                    }
                    if (show)
                    {
                        DisplayMessage("نقش مورد نظر با موفقیت ویرایش گردید.", false);
                    }
                    else
                    {
                        DisplayMessage("در ویرایش نقش مشکلی پیش آمده است", true);
                    }
                    SetPageData();
                }
                catch
                {
                    DisplayMessage("در ویرایش نقش خطایی رخ داده است", true);
                }
            }
        }

        protected void _rgUsersInRole_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var list2 = UserRole.GetAllUserInRole(RoleId, PortalSetting.PortalId);
            _rgUsersInRole.DataSource = list2;
        }

        protected void _rpLangRole_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                var txtRoleName = (TextBox)e.Item.FindControl("_txtRoleName");
                List<LocalResource> obj = LocalResource.GetResourceValues(RoleKey);
                if ((obj != null) && (obj.Count > 0))
                {
                    var content = (Language)e.Item.DataItem;
                    if (content != null)
                    {
                        var resource = Enumerable.SingleOrDefault(obj, o =>o.LanguageId == content.LanguageID);
                        if (resource != null)
                        {
                            txtRoleName.Text = resource.ResourceValue;
                        }
                    }
                }
            }
        }

        private void DisplayMessage(string message, bool fail)
        {
            Label lbl = fail ? _lblFail : _lblSucc;
            lbl.Text = message;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _rgAllUser.Culture = new CultureInfo(PayaTools.CurrentCulture);
            _rgAllUser.Rebind();
            _rgRoles.Culture = new CultureInfo(PayaTools.CurrentCulture);
            _rgRoles.Rebind();
            _rgUsersInRole.Culture = new CultureInfo(PayaTools.CurrentCulture);
            _rgUsersInRole.Rebind();
           
            if (ModuleConfiguration.HasNoPermission())
            {
                PayaTools.AccessDenied();
            }
            else if (!ModuleConfiguration.HasDefinedPermission(13))
            {
                PayaTools.AccessDenied();
            }
            Page.Title = PortalSetting.PortalName + @" مدیریت نقش ها";
            PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadControlFont.css");
            SetPageData();
            SetPageControl();
        }

        protected void RadGridUser_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                int userId;
                if (e.DraggedItems[0].OwnerGridID == _rgAllUser.ClientID)
                {
                    foreach (GridDataItem draggedItem in e.DraggedItems)
                    {
                        userId = (int)draggedItem.GetDataKeyValue("UserId");
                        var t =UserRole.Add(RoleId,userId);
                    }
                    SetPageData();
                }
                else if (e.DraggedItems[0].OwnerGridID == _rgUsersInRole.ClientID)
                {
                    foreach (GridDataItem draggedItem in e.DraggedItems)
                    {
                        userId = (int)draggedItem.GetDataKeyValue("UserId");
                        UserRole.Delete( userId,RoleId);
                    }
                    SetPageData();
                }
            }
        }

        private void SetPageControl()
        {
            switch (PageStatus)
            {
                case PageState.Role:
                    _pnlRoles.Visible = true;
                    _pnlUsers.Visible = false;
                    break;

                case PageState.UserRole:
                    _pnlRoles.Visible = false;
                    _pnlUsers.Visible = true;
                    if (_rgRoles.SelectedValue != null)
                    {
                        RoleId = (int)_rgRoles.SelectedValue;
                    }
                    break;
            }
        }

        private void SetPageData()
        {
            if (PageStatus == PageState.UserRole)
            {
                _lblUserInRole.Text = @"کاربران دارای نقش " + Role.GetSingleById(RoleId).RoleName;
                var list2 = UserRole.GetAllUserInRole(RoleId, PortalSetting.PortalId);
                var lst = PortalUser.GetAll(PortalSetting.PortalId).Except(list2, new PortalUserCompare()).ToList();
                _rgUsersInRole.DataSource = list2;
                _rgUsersInRole.DataBind();
                _rgAllUser.DataSource = lst;
                _rgAllUser.DataBind();
            }
            else
            {
                _rgRoles.DataSource = Role.GetAll(PortalSetting.PortalId);
                _rgRoles.DataBind();
            }
        }

        // Properties
        private PageState PageStatus
        {
            get
            {
                if (ViewState["PageState"] == null)
                {
                    return PageState.Role;
                }
                return (PageState)ViewState["PageState"];
            }
            set
            {
                ViewState["PageState"] = value;
            }
        }

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

        private string RoleKey
        {
            get
            {
                if (ViewState["RoleKey"] == null)
                {
                    return "";
                }
                return ViewState["RoleKey"].ToString();
            }
            set
            {
                ViewState["RoleKey"] = value;
            }
        }

        // Nested Types
        private enum AuthRole
        {
            ModuleManagment = 13
        }

        private enum PageState
        {
            Role,
            UserRole
        }

    }
}