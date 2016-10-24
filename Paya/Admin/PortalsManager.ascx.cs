using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class PortalsManager : ModuleControl
    {

        // Methods
        protected void _btnBack_Click(object sender, EventArgs e)
        {
            PageStatus = PageState.Load;
            SetPageControl();
            SetPageData();
        }

        protected void _btnSave_Click(object sender, EventArgs e)
        {
            switch (PageStatus)
            {
                case PageState.Add:
                    {
                        int portalId;
                        try
                        {
                            portalId = Portal.Add(_txtPortalPath.Text, _txtPortalName.Text, _txtPortalPath.Text);
                        }
                        catch (Exception)
                        {
                            portalId = -1;
                        }
                        if (portalId == -1)
                        {
                            DisplayMessage("در ثبت پرتال جدید مشکلی پیش آمده است.", true);
                            break;
                        }
                        //PortalLanguage.Add(true, 19,Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).LanguageID, portalId);
                        PortalSetting.UpdatePortalSettings(portalId, "DefaultCulture", _rdcboLanguage.SelectedValue);
                        PortalSetting.UpdatePortalSettings(portalId, "DefaultStyle", _rdcboStyle.SelectedValue);
                        PortalSetting.UpdatePortalSettings(portalId, "EmailAddress", _txtEmail.Text);
                        PortalSetting.UpdatePortalSettings(portalId, "LoginAttempts",
                                                           ((int) _rdtxtCount.Value.Value).ToString());
                        PortalSetting.UpdatePortalSettings(portalId, "Monitoring", _cbxMonitoring.Checked ? "on" : "off");
                        PortalSetting.UpdatePortalSettings(portalId, "ShowRegisterCaptcha",
                                                           _cbxCaptcha.Checked ? "on" : "off");
                        PortalSetting.UpdatePortalSettings(portalId, "LoginAction", _rdcboAction.SelectedValue);
                        int roleid = 3;
                        List<int> lstLanguages = (from o in Language.GetAll() select o.LanguageID).ToList<int>();
                        if (!PortalSetting.SingleUserBase)
                        {
                            int userid = PortalUser.Add(_txtUserEmail.Text, _txtFirstName.Text, false, true,
                                                        _txtLastName.Text,
                                                        portalId, _txtUserName.Text,
                                                        _txtPass.Text, "");
                            roleid = Role.Add(portalId, "SiteManager" + _txtPortalPath.Text);
                            foreach (int i in lstLanguages)
                            {
                                LocalResource.AddLocaleResource(i, "SiteManager" + _txtPortalPath.Text,
                                                                "SiteManager" + _txtPortalName.Text);
                            }
                            UserRole.Add(roleid, userid);
                        }
                        foreach (int i in lstLanguages)
                        {
                            Language lang = Language.GetSingleByID(i);
                            string settings =
                                string.Format(
                                    "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                                    new object[]
                                        {
                                            Convert.ToByte(true), Convert.ToByte(true), lang.HomeTabName, "Default",
                                            "Default"
                                        });
                            int homeTabId = PayaBL.Classes.Tab.AddTab(lang.HomeTabName + " " + _txtPortalPath.Text,null,
                                                                      "", true,
                                                                      true, true, 0, 0, i, portalId, "", "", settings,
                                                                      15);
                            if (homeTabId > 0)
                            {
                                TabRole.Add(15,homeTabId);
                                PortalLanguage.Add(true, homeTabId, i, portalId);
                                if (lang.Culture == _rdcboLanguage.SelectedValue)
                                {
                                    ModuleRole.Add(4, Module.AddModule(homeTabId, 1, 3, "Login", "menu", false), 14);
                                }
                                ModuleRole.Add(4, Module.AddModule(homeTabId, 1, 34, "Content", "content", false), 13);
                                
                                var tabName = (lang.Culture == "fa-IR") ? "مدیریت سایت" : "Site Management";
                                settings =
                                    string.Format(
                                        "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                                        new object[]
                                            {
                                                Convert.ToByte(true), Convert.ToByte(true), tabName, "OneColumn", "Default"
                                            });
                                int adminTabId = PayaBL.Classes.Tab.AddTab(tabName, null, "", true, false, false, 0, 1, i,
                                                                           portalId, "", "", settings, 13);
                                TabRole.Add(roleid,adminTabId);
                                if (!PortalSetting.SingleUserBase)
                                {
                                    tabName = (lang.Culture == "fa-IR") ? "مدیریت کاربران" : "Users Management";
                                    settings =
                                        string.Format(
                                            "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                                            new object[]
                                                {
                                                    Convert.ToByte(true), Convert.ToByte(true), tabName, "OneColumn",
                                                    "Default"
                                                });
                                    int userTabId = PayaBL.Classes.Tab.AddTab(tabName, adminTabId, "", true, false, true,
                                                                              0, 0, i, portalId, "", "", settings, 13);
                                    TabRole.Add(roleid,userTabId);
                                    ModuleRole.Add(13, Module.AddModule(userTabId, 1, 7, tabName, "content", false),
                                                   roleid);
                                    tabName = (lang.Culture == "fa-IR") ? "مدیریت نقشها" : "Roles Management";
                                    settings =
                                        string.Format(
                                            "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                                            new object[]
                                                {
                                                    Convert.ToByte(true), Convert.ToByte(true), tabName, "OneColumn",
                                                    "Default"
                                                });
                                    int roleTabId = PayaBL.Classes.Tab.AddTab(tabName, adminTabId, "", true, false, true,
                                                                              0, 0, i, portalId, "", "", settings, 13);
                                    TabRole.Add(roleid,roleTabId);
                                    ModuleRole.Add(13, Module.AddModule(roleTabId, 3, 8, tabName, "content", false),
                                                   roleid);
                                }
                                //=======================================================
                                tabName = (lang.Culture == "fa-IR") ? "مدیریت صفحات" : "Pages Management";
                                settings =
                                    string.Format(
                                        "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                                        new object[]
                                            {
                                                Convert.ToByte(true), Convert.ToByte(true), tabName, "OneColumn", "Default"
                                            });
                                int tabsTabId = PayaBL.Classes.Tab.AddTab(tabName, adminTabId, "", true, false, true, 0,
                                                                          0, i, portalId, "", "", settings, 13);
                                int tabsModuleId = Module.AddModule(tabsTabId, 5, 9, tabName, "content", false);
                                TabRole.Add(roleid,tabsTabId);
                                ModuleRole.Add(13,tabsModuleId, roleid);
                                //=======================================================
                                tabName = (lang.Culture == "fa-IR") ? "مدیریت محتوای سایت" : "Contents Management";
                                settings =
                                    string.Format(
                                        "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                                        new object[]
                                            {
                                                Convert.ToByte(true), Convert.ToByte(true), tabName, "OneColumn", "Default"
                                            });
                                int contentsTabId = PayaBL.Classes.Tab.AddTab(tabName, adminTabId, "", true, false, true,
                                                                              0, 0, i, portalId, "", "", settings, 13);
                                TabRole.Add(roleid,contentsTabId);
                                ModuleRole.Add(13,Module.AddModule(contentsTabId, 7, 35, tabName, "content", false),
                                               roleid);
                                //=======================================================
                                tabName = (lang.Culture == "fa-IR") ? "خروج" : "Sign Out";
                                settings =
                                    string.Format(
                                        "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                                        new object[] { Convert.ToByte(true), Convert.ToByte(true), tabName, "Default", "Default" });
                                int loguotId = PayaBL.Classes.Tab.AddTab(tabName, null, "", false, false, true, 0, 1, i,
                                                                         portalId, "",
                                                                         "", settings, 13);
                                TabRole.Add(13, loguotId);
                                ModuleRole.Add(4, Module.AddModule(loguotId, 1, 10, "Sign Out", "content", false), 13);
                            }
                        }
                        Caching.DeleteLanguagesCache();
                        Caching.DeletePortalsCache();
                        Caching.DeletePortalsSettingdCache();
                        PortalId = portalId;
                        PortalAlias = _txtPortalPath.Text;
                        PageStatus = PageState.Edit;
                        DisplayMessage("پرتال با موفقیت ثبت گردید.", false);
                        SetPageData();
                        SetPageControl();
                        break;
                    }
                case PageState.Edit:
                    {
                        bool b = Portal.Update(PortalId, PortalAlias, _txtPortalName.Text, PortalId!=0?_txtPortalPath.Text:string.Empty) &
                                 PortalSetting.UpdatePortalSettings(PortalId, "DefaultCulture",
                                                                    _rdcboLanguage.SelectedValue);
                        b &= PortalSetting.UpdatePortalSettings(PortalId, "DefaultStyle", _rdcboStyle.SelectedValue);
                        b &= PortalSetting.UpdatePortalSettings(PortalId, "EmailAddress", _txtEmail.Text);
                        var name = (int) _rdtxtCount.Value.Value;
                        b &= PortalSetting.UpdatePortalSettings(PortalId, "LoginAttempts", name.ToString());
                        b &= PortalSetting.UpdatePortalSettings(PortalId, "Monitoring",
                                                                _cbxMonitoring.Checked ? "on" : "off");
                        b &= PortalSetting.UpdatePortalSettings(PortalId, "ShowRegisterCaptcha",
                                                                _cbxCaptcha.Checked ? "on" : "off");
                        if (
                            !(b &
                              PortalSetting.UpdatePortalSettings(PortalId, "LoginAction", _rdcboAction.SelectedValue)))
                        {
                            DisplayMessage("در ویرایش پرتال مشکلی پیش آمده است.", true);
                            break;
                        }
                        DisplayMessage("پرتال با موفقیت ویرایش شد.", false);
                        Caching.DeletePortalsCache();
                        Caching.DeletePortalsSettingdCache();
                        SetPageData();
                        SetPageControl();
                        break;
                    }
            }
            SetPageControl();
            SetPageData();
        }

        protected void _rdctmnuportals_ItemClick(object sender, RadMenuEventArgs e)
        {
            string name = e.Item.Value;
            if (name != null)
            {
                if (name != "AddPortal")
                {
                    if (name == "EditPortal")
                    {
                        if (!string.IsNullOrEmpty(_rdlstPortals.SelectedValue))
                        {
                            PortalId = int.Parse(_rdlstPortals.SelectedValue);
                            PortalAlias = Portal.GetSingleByID(PortalId).PortalAlias;
                            PageStatus = PageState.Edit;
                            SetPageControl();
                            SetPageData();
                        }
                    }
                    else if ((name == "DeletePortal") && !string.IsNullOrEmpty(_rdlstPortals.SelectedValue))
                    {
                        if (Portal.Delete(int.Parse(_rdlstPortals.SelectedValue)))
                        {
                            DisplayMessage("پرتال با موفقیت حذف گردید", false);
                        }
                        else
                        {
                            DisplayMessage("در حذف پرتال مشکلی پیش آمده است.", true);
                        }
                        PageStatus = PageState.Load;
                        SetPageControl();
                        SetPageData();
                    }
                    else if (name == "BrowsPortal")
                    {
                        var p = Portal.GetSingleByID(int.Parse(_rdlstPortals.SelectedValue));
                        var tab =
                            PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, p.PortalID).HomeTabID;
                        Response.Redirect(PayaTools.BuildUrl(string.Format("~/Default.aspx?TabId={0}&PortalAlias={1}", new object[] { tab, p.PortalAlias })));// + Portal.GetSingleByID(int.Parse(_rdlstPortals.SelectedValue)).PortalPath));
                    }
                    
                }
                else
                {
                    PageStatus = PageState.Add;
                    SetPageControl();
                    SetPageData();
                }
            }
        }

        private void BindCombo()
        {
            _rdcboLanguage.DataSource = Language.GetAll();
            _rdcboLanguage.DataTextField = "LanguageName";
            _rdcboLanguage.DataValueField = "Culture";
            _rdcboLanguage.DataBind();

            if (_rdcboStyle.Items.Count == 0)
            {
                XDocument doc = Caching.GetCachedStyleXml();
                try
                {
                    //var query = from element in doc.Descendants("ModuleLayout") select new { name = element.Attribute("name"), control = element.Attribute("control") };
                    var query = from element in doc.Descendants("Style") select new { name = element.Attribute(PayaTools.CurrentCulture), control = element.Attribute("folder") };
                    foreach (var item in query)
                    {
                        _rdcboStyle.Items.Add(new RadComboBoxItem(item.name.Value, item.control.Value));
                    }
                    //_rdcboStyle.Items.Add(new RadComboBoxItem("طراحی قالب", "DesignedLayout.ascx"));
                }
                catch (Exception)
                {
                }
            }
            //_rdcboStyle.DataSource = Portal.GetAll();
            //_rdcboStyle.DataTextField = PayaTools.IsPersian ? "Fa" : "En";
            //_rdcboStyle.DataValueField = "Style";
            //_rdcboStyle.DataBind();
        }

        private void ClearControls()
        {
            _txtPortalName.Text = "";
            _txtPortalPath.Text = "";
            _txtEmail.Text = "";
            _rdtxtCount.Text = "0";
            _cbxCaptcha.Checked = false;
            _cbxMonitoring.Checked = false;
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
            Page.Title = PortalSetting.PortalName + @" مدیریت پرتال ها";
            PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadControlFont.css");
            SetPageControl();
            SetPageData();
        }

        protected void RadListBox_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            if (e.DestinationListBox == _rdlstboxModuleDefPortal)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    ModuleDef.AddModuleDefInPortal(int.Parse(item.Value), PortalId);
                    Caching.Remove(string.Format("AllModuleDefInPortal{0}", PortalId));
                }
            }
            else if (e.DestinationListBox == _rdlstboxAllModuleDef)
            {
                foreach (RadListBoxItem item in e.Items)
                {
                    ModuleDef.DeleteModuleDefInPortal(int.Parse(item.Value), PortalId);
                    Caching.Remove(string.Format("AllModuleDefInPortal{0}", PortalId));
                }
            }
            SetPageControl();
            SetPageData();
        }

        private void SetPageControl()
        {
            switch (PageStatus)
            {
                case PageState.Load:
                    _pnlPortalInfo.Visible = false;
                    _pnlUser.Visible = false;
                    _pnlModuleDefPortal.Visible = false;
                    break;

                case PageState.Add:
                    _pnlPortalInfo.Visible = true;
                    _pnlUser.Visible = !PortalSetting.SingleUserBase;
                    _pnlModuleDefPortal.Visible = false;
                    break;

                case PageState.Edit:
                    _pnlPortalInfo.Visible = true;
                    _pnlUser.Visible = false;
                    _pnlModuleDefPortal.Visible = true;
                    if (PortalId == 0)
                    {
                        _pnlModuleDefPortal.Visible = false;
                    }
                    break;
            }
        }

        private void SetPageData()
        {
            switch (PageStatus)
            {
                case PageState.Load:
                    _rdlstPortals.DataSource = Portal.GetAll();
                    _rdlstPortals.DataTextField = "PortalName";
                    _rdlstPortals.DataValueField = "PortalId";
                    _rdlstPortals.DataBind();
                    break;

                case PageState.Add:
                    BindCombo();
                    ClearControls();
                    break;

                case PageState.Edit:
                    {
                        BindCombo();
                        Portal obj = Portal.GetSingleByPortalAlias(PortalAlias);
                        if (obj != null)
                        {
                            //RadComboBoxItem item = _rdcboLanguage.FindItemByValue(Portal.SettingsName.DefaultCulture.ToString());
                            RadComboBoxItem item = _rdcboLanguage.FindItemByValue(PortalSetting.GetSingleById(PortalId, "DefaultCulture").SettingValue);
                            if (item != null)
                            {
                                item.Selected = true;
                            }
                            // item = _rdcboStyle.FindItemByValue(obj.Settings[(int) Portal.SettingsName.DefaultStyle].ToString());
                            item = _rdcboStyle.FindItemByValue(PortalSetting.GetSingleById(PortalId, "DefaultStyle").SettingValue);
                            if (item != null)
                            {
                                item.Selected = true;
                            }
                            // item = _rdcboAction.FindItemByValue(obj.Settings[(int) Portal.SettingsName.LoginAction].ToString());
                            item = _rdcboAction.FindItemByValue(PortalSetting.GetSingleById(PortalId, "LoginAction").SettingValue);
                            if (item != null)
                            {
                                item.Selected = true;
                            }
                            if (PortalId != 0)
                            { 
                                var list=ModuleDef.GetAll(PortalId, null);

                                _rdlstboxModuleDefPortal.DataSource = list;
                                _rdlstboxModuleDefPortal.DataTextField = "FriendlyName";
                                _rdlstboxModuleDefPortal.DataValueField = "ModuleDefId";
                                _rdlstboxModuleDefPortal.DataBind();

                                var t =
                                    ModuleDef.GetAll(0, null).Where(o => o.ModuleDefId != 84 && o.ModuleDefId != 2000).
                                        Except(list, new ModuleDefComparer());

                                _rdlstboxAllModuleDef.DataSource = t;
                                _rdlstboxAllModuleDef.DataTextField = "FriendlyName";
                                _rdlstboxAllModuleDef.DataValueField = "ModuleDefId";
                                _rdlstboxAllModuleDef.DataBind();
                                
                            }
                            _txtPortalName.Text = obj.PortalName;
                            _txtPortalPath.Text = obj.PortalPath;
                            if (PortalId==0)
                            {
                                _txtPortalPath.Enabled = false;
                                RequiredFieldValidator4.Enabled = false;
                            }
                            else
                            {
                                 _txtPortalPath.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                            }
                            _txtEmail.Text = PortalSetting.GetSingleById(PortalId, "EmailAddress").SettingValue;// obj.Settings[(int)Portal.SettingsName.EmailAddress].ToString();
                            _rdtxtCount.Text = PortalSetting.GetSingleById(PortalId, "LoginAttempts").SettingValue;// obj.Settings[(int)Portal.SettingsName.LoginAttempts].ToString();
                            _cbxCaptcha.Checked = PortalSetting.GetSingleById(PortalId, "ShowRegisterCaptcha").SettingValue == "on";// obj.Settings[(int) Portal.SettingsName.ShowRegisterCaptcha].ToString() == "on";
                            _cbxMonitoring.Checked = PortalSetting.GetSingleById(PortalId, "Monitoring").SettingValue == "on";// obj.Settings[(int) Portal.SettingsName.Monitoring].ToString() == "on";
                        }
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
                    return PageState.Load;
                }
                return (PageState)ViewState["PageState"];
            }
            set
            {
                ViewState["PageState"] = value;
            }
        }

        private string PortalAlias
        {
            get
            {
                if (ViewState["PortalAlias"] == null)
                {
                    return "";
                }
                return ViewState["PortalAlias"].ToString();
            }
            set
            {
                ViewState["PortalAlias"] = value;
            }
        }

        private int PortalId
        {
            get
            {
                if (ViewState["PortalId"] == null)
                {
                    return 0;
                }
                return (int)ViewState["PortalId"];
            }
            set
            {
                ViewState["PortalId"] = value;
            }
        }

        // Nested Types
        private enum AuthRole
        {
            ModuleManagment = 13
        }

        private enum PageState
        {
            Load,
            Add,
            Edit
        }

    }
}