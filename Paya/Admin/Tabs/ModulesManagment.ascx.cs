using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using Telerik.Web.UI;

namespace Paya.Admin.Tabs
{
    public partial class ModulesManagment : TabControl
    {

    // Methods
    protected void _lstModuleDef_ItemDataBound(object sender, RadListBoxItemEventArgs e)
    {
        Image img = (Image) e.Item.FindControl("_imgModuleDef");
        Label lbl = (Label) e.Item.FindControl("_lblModuleDef");
        ModuleDef obj = e.Item.DataItem as ModuleDef;
        if (((img != null) && (obj != null)) && (lbl != null))
        {
            lbl.Text = obj.FriendlyName;
            string[] file = Directory.GetFiles(Server.MapPath("~/Images/"), obj.ModuleKey + ".*");
            if ((file.Length > 0) && (file[0] != ""))
            {
                img.ImageUrl = PortalSetting.PortalPath + "/Images/" + obj.ModuleKey + Path.GetExtension(file[0]);
            }
            else
            {
                img.ImageUrl = PortalSetting.PortalPath + "/Images/ModuleDef.png";
            }
        }
    }

    protected void _rdctmnuModuleDef_ItemClick(object sender, RadMenuEventArgs e)
    {
        string name = e.Item.Value;
        if (name != null)
        {
            ModuleDef mdef;
            if (!(name == "AddHeader"))
            {
                if (name == "AddPage")
                {
                    mdef = ModuleDef.GetSingleByID(int.Parse(_lstModuleDef.SelectedValue));
                    int mcount = Module.GetModulesOfTab(TabLoad.TabID).Count;
                    if (!(mdef.IsUserControl() || (mcount <= 0)))
                    {
                        DisplayMessage(
                            "امکان ثبت این برنامه در این صفحه وجود ندارد!<br>برنامه " + mdef.FriendlyName +
                            " باید در یک صفحه جداگانه بدون وجود هیچ برنامه دیگری ثبت شود", true);
                    }
                    else
                    {
                        int morder = Module.GetTabModules(TabLoad.TabID, "content").Count + 1;
                        //Module.Add(null, "LayoutControl.ascx", DateTime.Now, mdef.ModuleDefId, morder, mdef.FriendlyName,
                        //           "content", false, null, TabLoad.TabID, 0);
                        Module.AddModule(TabLoad.TabID, morder, mdef.ModuleDefId, mdef.FriendlyName, "content", false);
                        Caching.DeleteModulesCache();
                        SetPageControls(true);
                    }
                }
            }
            else if ((_lstModuleDef.SelectedIndex > -1) && 
                (((_lstModuleDef.SelectedValue == "31") || (_lstModuleDef.SelectedValue == "34")) 
                || (_lstModuleDef.SelectedValue == "3")))
            {
                mdef = ModuleDef.GetSingleByID(int.Parse(_lstModuleDef.SelectedValue));
                Module.AddModule(
                    PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, PortalSetting.PortalId).
                        HomeTabID, 0, mdef.ModuleDefId, mdef.FriendlyName, "header", false);
                Caching.DeleteModulesCache();
                SetPageControls(true);
            }
        }
    }

    private void DisplayMessage(string message, bool fail)
    {
        _lblFail.Visible = false;
        _lblSucc.Visible = false;
        Label lbl = fail ? _lblFail : _lblSucc;
        lbl.Visible = true;
        lbl.Text = message;
    }

    private void LoadModuleDef()
    {
        bool b = PortalUser.GetSingleByID(int.Parse(Page.User.Identity.Name)).IsSuperUser;
        var t = ModuleDef.GetAll(PortalSetting.PortalId, b);
        t.AddRange(ModuleDef.GetAll(PortalSetting.PortalId,null));
        _lstModuleDef.DataSource =b? t:ModuleDef.GetAll(PortalSetting.PortalId,null);
        _lstModuleDef.DataValueField = "ModuleDefId";
        _lstModuleDef.DataBind();
    }

    protected override void Page_Load(object sender, EventArgs e)
    {
        string script = "<script type=\"text/javascript\">" +
                        "function showContextMenu(sender, e) { var menu = $find(\""
                        + _rdctmnuModuleDef.ClientID +
                        "\"); var rawEvent = e.get_domEvent().rawEvent; menu.show(rawEvent); e.get_item().select();" +
                        " $telerik.cancelRawEvent(rawEvent); var listBox = $find(\"" +
                        _lstModuleDef.ClientID +
                        "\"); var listItem = listBox.get_selectedItem(); var value = listItem.get_value(); var showAddHeader; " +
                        "if (value == 34 || value == 3 || value == 31) { showAddHeader = true; } " +
                        "else { showAddHeader = false; }" +
                        " menuItems = menu.get_items(); " +
                        "for (var i = 0; i < menuItems.get_count(); i++) { var menuItem = menuItems.getItem(i);" +
                        " switch (menuItem.get_value()) { case \"AddHeader\": menuItem.set_enabled(showAddHeader); break; } } }" +
                        "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "showContextMenuScript", script, false);
        LoadModuleDef();
        SetPageControls(false);
        base.Page_Load(sender, e);
    }

    private void SetPageControls(bool load)
    {
        _phlModules.Controls.Clear();
        var userControl = (TabControl) Page.LoadControl("~/Admin/Tabs/Modules.ascx");
        userControl.ID = "Modules_userControl";
        userControl.TabLoad = !load ? TabLoad : PayaBL.Classes.Tab.GetSingleByID(TabLoad.TabID);
        _phlModules.Controls.Add(userControl);
    }

    }
}