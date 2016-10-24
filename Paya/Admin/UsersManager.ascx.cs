using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paya.Modules.CustomProfile;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class UsersManager : ModuleControl
    {
       // Fields
    protected Button _btnBackUser;
    protected Label _lblFail;
    protected Label _lblSucc;
    protected Panel _pnlControl;
    protected Panel _pnlProfile;
    protected Panel _pnlUserRoles;
    protected Panel _pnlUsers;
    protected RadAjaxLoadingPanel _rdlpUsersManager;
    protected RadAjaxPanel _rdpUsersManager;
    protected RadGrid _rgAllRoles;
    protected RadGrid _rgPortalUser;
    protected RadGrid _rgRegisterReq;
    protected RadGrid _rgUsersRole;
    protected RadMultiPage _rmpUser;
    protected RadPageView _rpPortalUser;
    protected RadPageView _rpRequestRegUser;
    protected RadTabStrip _rtsUsers;
    protected Button Button1;
    protected RadScriptBlock scriptBlock;

    // Methods
    protected void _rgAllRoles_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        var list = UserRole.GetUserRoleByUserId(UserId);
        var lst = (from o in Role.GetAll(PortalSetting.PortalId)
            where ((o.RoleID != 15) && (o.RoleID != 14)) && (o.RoleID != 13)
            select o).ToList<Role>().Except<Role>(list, new RoleComparer()).ToList<Role>();
        _rgAllRoles.DataSource = lst;
    }

    protected void _rgPortalUser_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var item = (GridDataItem) e.Item;
        if (PortalUser.Delete(int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["UserId"].ToString())))
        {
            DisplayMessage("کاربر  با موفقیت حذف شد", false);
        }
        else
        {
            DisplayMessage("در حذف کاربر مشکلی پیش آمده است", true);
        }
        SetPageData();
    }

    protected void _rgPortalUser_InsertCommand(object source, GridCommandEventArgs e)
    {
        if (!Page.IsValid)
        {
            DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
        }
        else
        {
            var insertedItem = (GridEditFormInsertItem) e.Item;
            var user = new PortalUser {
                UserName = ((TextBox) insertedItem.FindControl("_txtUserName")).Text,
                FirstName = ((TextBox) insertedItem.FindControl("_txtFirstName")).Text,
                LastName = ((TextBox) insertedItem.FindControl("_txtLastName")).Text,
                UserPass = ((TextBox) insertedItem.FindControl("_txtPass")).Text,
                Email = ((TextBox) insertedItem.FindControl("_txtEmail")).Text,
                UserStyle = ((DropDownList) insertedItem.FindControl("_ddlUserStyle")).SelectedItem.Value
            };
            if (user.UserStyle == "0")
            {
                user.UserStyle = "";
            }
            user.IsSuperUser = ((CheckBox) insertedItem.FindControl("_cbxSuperUser")).Checked;
            user.PortalID = PortalSetting.PortalId;
            int i = user.Add();
            if (i > 0)
            {
                DisplayMessage("کاربر با موفقیت ثبت گردید.", false);
                //CustomProfileBl.AddProfile(i, "", "", null, false, "", "", "", "");
            }
            else
            {
                DisplayMessage("در ثبت کاربر مشکلی پیش آمده است.", true);
            }
            SetPageData();
        }
    }

    protected void _rgPortalUser_ItemCommand(object source, GridCommandEventArgs e)
    {
        string name = e.CommandName;
        if (name != null)
        {
            if (name != "UserRole")
            {
                if (name == "Profile")
                {

                    UserId = int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserId"].ToString());
                    PageStatus = PageState.Profile;
                    SetPageControl();
                    SetPageData();
                    
                }
                else if (name == "LockUser")
                {
                    var obj = e.Item.DataItem as PortalUser;
                    if (obj != null)
                    {
                        if (obj.IsLocked)
                        {
                            PortalUser.UpdateLockUser(obj.UserID, false);
                        }
                        else
                        {
                            var t = PortalUser.UpdateLockUser(obj.UserID, true);
                        }
                    }
                    SetPageData();
                }
            }
            else
            {
                UserId = int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserId"].ToString());
                PageStatus = PageState.UserRole;
                SetPageControl();
                SetPageData();
            }
        }
    }

    protected void _rgPortalUser_ItemDataBound(object sender, GridItemEventArgs e)
    {
        PortalUser obj;
        if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
        {
            ListItem item;
            var editFormItem = e.Item as GridEditFormItem;
            obj = e.Item.DataItem as PortalUser;
            var cbx = editFormItem.FindControl("_cbxSuperUser") as CheckBox;
            var ddl = editFormItem.FindControl("_ddlUserStyle") as DropDownList;
            var txtusername = editFormItem.FindControl("_txtUserName") as TextBox;
            var pass = editFormItem.FindControl("_txtPass") as TextBox;
            var confirmpass = editFormItem.FindControl("_txtPassConfirm") as TextBox;
            if (cbx != null)
            {
                cbx.Visible = CurrentUser.IsSuperUser;
                if (txtusername != null && !string.IsNullOrEmpty(txtusername.Text))
                    cbx.Checked = PortalUser.GetSingleByUserName(txtusername.Text,PortalSetting.PortalId).IsSuperUser;
            }
            if (ddl != null)
            {
                ddl.DataSource = Portal.AllPortalStyles();
                ddl.DataTextField = PayaTools.CurrentCulture;
                ddl.DataValueField = "Style";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("", "0"));
            }
            if (obj != null)
            {
                if (ddl != null)
                {
                    item = ddl.Items.FindByValue(obj.UserStyle);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
                if (txtusername != null)
                {
                    txtusername.Attributes.Add("readonly", "readonly");
                }
                //if (pass != null)
                //{
                //    pass.Attributes.Add("readonly", "readonly");
                //}
                //if (confirmpass != null)
                //{
                //    confirmpass.Attributes.Add("readonly", "readonly");
                //}
            }
            else if (ddl != null)
            {
                item = ddl.Items.FindByValue("");
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            obj = e.Item.DataItem as PortalUser;
            if (obj != null)
            {
                var imgbtn = (ImageButton) e.Item.FindControl("_imgbtnUserLock");
                if (imgbtn != null)
                {
                    imgbtn.ImageUrl = obj.IsLocked ? "~/Images/lock.png" : "~/Images/lock_off.png";
                }
            }
        }
    }

    protected void _rgPortalUser_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        _rgPortalUser.DataSource = PortalUser.GetAll(PortalSetting.PortalId);
    }

    protected void _rgPortalUser_UpdateCommand(object source, GridCommandEventArgs e)
    {
        if (!Page.IsValid)
        {
            DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
        }
        else
        {
            var editedItem = e.Item as GridEditableItem;
            int id = int.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserId"].ToString());
            string name = e.CommandArgument.ToString();
            if (name != null)
            {
                string userName = ((TextBox) editedItem.FindControl("_txtUserName")).Text;
                string pass = ((TextBox)editedItem.FindControl("_txtPass")).Text;
                string firstName = ((TextBox) editedItem.FindControl("_txtFirstName")).Text;
                string lastName = ((TextBox) editedItem.FindControl("_txtLastName")).Text;
                string email = ((TextBox) editedItem.FindControl("_txtEmail")).Text;
                string userSyle = ((DropDownList) editedItem.FindControl("_ddlUserStyle")).SelectedItem.Value;
                if (userSyle == "0")
                {
                    userSyle = "";
                }
                bool isSuperUser = ((CheckBox) editedItem.FindControl("_cbxSuperUser")).Checked;
                if (PortalUser.Update(id, email, firstName, false, isSuperUser, lastName, PortalSetting.PortalId, userName, userSyle)&&(pass == "" ?true: PortalUser.UpdateUserPassword(id,pass)))
                {
                    DisplayMessage("ویرایش اطلاعات با موفقیت انجام شد.", false);
                }
                else
                {
                    DisplayMessage("در ویرایش اطلاعات مشکلی پیش آمده است.", true);
                }
                #region old
                //if (name != "UpdateUserName")
                //{
                //    if (name == "UpdatePass")
                //    {
                //        string pass = ((TextBox) editedItem.FindControl("_txtPass")).Text;
                //        if (PortalUser.UpdateUserPassword(id, pass))
                //        {
                //            DisplayMessage("رمز عبور با موفقیت ویرایش شد.", false);
                //            //CustomProfileBl.UpdateLastPasswordChangeDate(id);
                //        }
                //        else
                //        {
                //            DisplayMessage("در ویرایش رمز عبور مشکلی پیش آمده است.", true);
                //        }
                //    }
                //    else if (name == "UpdateInfo")
                //    {
                //        string firstName = ((TextBox) editedItem.FindControl("_txtFirstName")).Text;
                //        string lastName = ((TextBox) editedItem.FindControl("_txtLastName")).Text;
                //        string email = ((TextBox) editedItem.FindControl("_txtEmail")).Text;
                //        string userSyle = ((DropDownList) editedItem.FindControl("_ddlUserStyle")).SelectedItem.Value;
                //        if (userSyle == "0")
                //        {
                //            userSyle = "";
                //        }
                //        bool isSuperUser = ((CheckBox) editedItem.FindControl("_cbxSuperUser")).Checked;
                //        if (PortalUser.Update(id, email, firstName, lastName, isSuperUser) && PortalUser.UpdateUserStyle(id, userSyle))
                //        {
                //            DisplayMessage("ویرایش اطلاعات با موفقیت انجام شد.", false);
                //        }
                //        else
                //        {
                //            DisplayMessage("در ویرایش اطلاعات مشکلی پیش آمده است.", true);
                //        }
                //    }
                //}
                //else
                //{
                //    string userName = ((TextBox) editedItem.FindControl("_txtUserName")).Text;
                //    if (PortalUser.UpdateUserName(id, userName))
                //    {
                //        DisplayMessage("نام کاربری با موفقیت ثبت گردید.", false);
                //    }
                //    else
                //    {
                //        DisplayMessage("در ویرایش نام کاربری مشکلی پیش آمده است.", true);
                //    }
                //}
#endregion
            }
            SetPageData();
        }
    }

    protected void _rgRegisterReq_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var item = (GridDataItem) e.Item;
        UserReq.Delete(int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["UserId"].ToString()));
    }

    protected void _rgRegisterReq_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "AcceptUser")
        {
            var obj = e.Item.DataItem as UserReq;
            if (obj != null)
            {
                string pass = PayaTools.GenerateStringWithDigit(7);
                int i = PortalUser.Add(obj.UserName, obj.FirstName, obj.LastName, pass, obj.Email, obj.PortalID, "", false);
                if (i > 0)
                {
                    string emailBody = string.Concat(new object[] { "<div align='right' dir='rtl'><font face='Tahoma' size='2'>همکار گرامي، سرکار خانم / جناب آقاي ", obj.FirstName, " ", obj.LastName, ":<br>احتراما اطلاعات پرسنلي شما براي ورود به سامانه پورتال به شرح زير مي باشد:<br>نام کاربر : <b>", i, "</b><br>رمز عبور : <b>", pass, "</b><br>شما مي توانيد با اين اطلاعات به سامانه وارد شده و در صورت تمايل در هر زمان با مراجعه به قسمت تنظيمات کاربر، کلمه عبور خود را تغيير دهيد. <br>با تشکر و احترام <br>مديريت پورتال </font></div>" });
                    PayaTools.SendEmail("Account Information", emailBody, PortalSetting.EmailAddress, obj.Email);
                    //CustomProfileBl.AddProfile(i, "", "", null, false, "", "", obj.PhoneNumber, obj.PhoneNumber);
                    UserReq.Delete(obj.ReqID);
                    SetPageData();
                }
            }
        }
    }

    protected void _rgRegisterReq_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        _rgRegisterReq.DataSource = UserReq.GetAll("", "", "", "", PortalSetting.PortalId);
    }

    protected void _rgUsersRole_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        var list = UserRole.GetUserRoleByUserId(UserId).ToList();
        _rgUsersRole.DataSource = list;
    }

    protected void BtnBackUser_Click(object sender, EventArgs e)
    {
        PageStatus = PageState.User;
        SetPageControl();
        SetPageData();
    }

    private void DisplayMessage(string message, bool fail)
    {
        _lblFail.Visible = false;
        _lblSucc.Visible = false;
        var lbl = fail ? _lblFail : _lblSucc;
        lbl.Visible = true;
        lbl.Text = message;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ModuleConfiguration.HasNoPermission())
        {
            PayaTools.AccessDenied();
        }
        else if (!ModuleConfiguration.HasDefinedPermission(13))
        {
            PayaTools.AccessDenied();
        }
        Page.Title = PortalSetting.PortalName + @" مدیریت کاربران";
        PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadControlFont.css");
        SetPageData();
        SetPageControl();
    }

    protected void RadGridRole_RowDrop(object sender, GridDragDropEventArgs e)
    {
        if (string.IsNullOrEmpty(e.HtmlElement))
        {
            int roleId;
            if (e.DraggedItems[0].OwnerGridID == _rgAllRoles.ClientID)
            {
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                    roleId = (int) draggedItem.GetDataKeyValue("RoleId");
                    UserRole.Add( roleId,UserId);
                }
                SetPageData();
            }
            else if (e.DraggedItems[0].OwnerGridID == _rgUsersRole.ClientID)
            {
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                    roleId = (int) draggedItem.GetDataKeyValue("RoleId");
                    UserRole.Delete(UserId,roleId);
                }
                SetPageData();
            }
        }
    }

    private void SetPageControl()
    {
        switch (PageStatus)
        {
            case PageState.User:
                _pnlUsers.Visible = true;
                _pnlProfile.Visible = false;
                _pnlUserRoles.Visible = false;
                break;

            case PageState.Profile:
                _pnlUsers.Visible = false;
                _pnlProfile.Visible = true;
                _pnlUserRoles.Visible = false;
                break;

            case PageState.UserRole:
                _pnlUsers.Visible = false;
                _pnlProfile.Visible = false;
                _pnlUserRoles.Visible = true;
                break;
        }
    }

    private void SetPageData()
    {
        switch (PageStatus)
        {
            case PageState.User:
                _rgPortalUser.DataSource = PortalUser.GetAll(PortalSetting.PortalId);
                _rgPortalUser.DataBind(); 
                _rgRegisterReq.DataSource = UserReq.GetAll("", "", "", "", PortalSetting.PortalId);
        _rgRegisterReq.DataBind();
                break;

            case PageState.Profile:
                {
                    _pnlControl.Controls.Clear();
                    var uc = (UserProfile)Page.LoadControl("~/Modules/CustomProfile/UserProfile.ascx");
                    uc.UserId = UserId;
                    uc.ID = "ctlrtst_" + ModuleConfiguration.ModuleID + UserId;
                    uc.SetPageData();
                    _pnlControl.Controls.Add(uc);
                    break;
                }
            case PageState.UserRole:
            {
                var list = UserRole.GetUserRoleByUserId(UserId).ToList();
                var lst = (from o in Role.GetAll(PortalSetting.PortalId)
                    where ((o.RoleID != 15) && (o.RoleID != 14)) && (o.RoleID != 13)
                    select o).ToList<Role>().Except<Role>(list, new RoleComparer()).ToList<Role>();
                _rgAllRoles.DataSource = lst;
                _rgAllRoles.DataBind();
                _rgUsersRole.DataSource = list;
                _rgUsersRole.DataBind();
                break;
            }
        }
       
    }

    // Properties
    private PageState PageStatus
    {
        get
        {
            if (ViewState["PageState"] == null)
            {
                return PageState.User;
            }
            return (PageState) ViewState["PageState"];
        }
        set
        {
            ViewState["PageState"] = value;
        }
    }

    private int UserId
    {
        get
        {
            if (ViewState["UserId"] == null)
            {
                return 0;
            }
            return (int) ViewState["UserId"];
        }
        set
        {
            ViewState["UserId"] = value;
        }
    }

    // Nested Types
    private enum AuthRole
    {
        ModuleManagment = 13
    }

    private enum PageState
    {
        User,
        Profile,
        UserRole
    }

    }
}