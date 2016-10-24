using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paya.Admin.Tab;
using Paya.UI.Templates;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using PayaBL.Control;
using Telerik.Web.UI;
using Telerik.Web.UI.Dock;

namespace Paya.Admin.Tabs
{
    public partial class Modules : TabControl
    {
    private bool _dockStateCleared = false;
    private Hashtable _dockZoneModules = new Hashtable();
    private Panel _header;

    // Methods
    protected void BackToTemplate_Click(object sender, EventArgs e)
    {
        PageState = PageStates.Load;
        SetPageControls();
    }

    private void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        var lb = (RadListBox) _header.FindControl("_rdlstHeaderModules");
        if (((lb != null) && (lb.SelectedIndex > -1)) && Module.Delete(int.Parse(lb.SelectedValue)))
        {
            ModuleId = -1;
        }
        PageState = PageStates.Load;
        SetPageControls();
    }

    private void btnEditInfo_Click(object sender, ImageClickEventArgs e)
    {
        var lb = (RadListBox) _header.FindControl("_rdlstHeaderModules");
        if ((lb != null) && (lb.SelectedIndex > -1))
        {
            int mId = int.Parse(lb.SelectedValue);
            PageState = PageStates.ModuleInfo;
            ModuleId = mId;
            SetPageControls();
        }
    }

    private void btnEditSettings_Click(object sender, ImageClickEventArgs e)
    {
        var lb = (RadListBox) _header.FindControl("_rdlstHeaderModules");
        if ((lb != null) && (lb.SelectedIndex > -1))
        {
            int mId = int.Parse(lb.SelectedValue);
            PageState = PageStates.Setting;
            ModuleId = mId;
            SetPageControls();
        }
    }

    private void btnPermission_Click(object sender, ImageClickEventArgs e)
    {
        var lb = (RadListBox) _header.FindControl("_rdlstHeaderModules");
        if ((lb != null) && (lb.SelectedIndex > -1))
        {
            int mId = int.Parse(lb.SelectedValue);
            PageState = PageStates.Roles;
            ModuleId = mId;
            SetPageControls();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        foreach (object item in _dockZoneModules.Keys)
        {
            var zone = FindControl("ctrl_Template").FindControl(item.ToString()) as RadDockZone;
            if (zone != null)
            {
                foreach (RadDock dock in zone.Docks)
                {
                    if (dock.UniqueName.ToLower() != "menu")
                    {
                        string title = ((LinkButton) dock.TitlebarContainer.Controls[0]).Text;
                        int mid = int.Parse(dock.UniqueName);
                        var t =Module.UpdateModulePane(mid, zone.ID.Replace("zone", ""));
                         t =Module.UpdateModuleOrder(mid, dock.Index);
                        
                    }
                }
            }
        }
    }

    private RadDock CreateRadDock(string id, string title)
    {
        var initLocal1 = new RadDock {
            DockMode = DockMode.Docked,
            UniqueName = id
        };
        var dock = initLocal1;
        dock.ID = string.Format("RadDock{0}", dock.UniqueName);
        dock.EnableAnimation = true;
        dock.CssClass = "radDock";
        dock.DockMode = DockMode.Docked;
        dock.Width = Unit.Pixel(60);
        var initLocal2 = new List<DockCommand>();
        var initLocal3 = new DockCommand {
            CssClass = "CommandEditAuth",
            Text = "تعیین دسترسی های برنامه",
            AutoPostBack = true,
            Name = "EditAuth"
        };
        initLocal2.Add(initLocal3);
        var initLocal4 = new DockCommand {
            CssClass = "CommandEditInfo",
            Text = "تنظیمات برنامه",
            AutoPostBack = true,
            Name = "EditInfo"
        };
        initLocal2.Add(initLocal4);
        var initLocal5 = new DockCommand {
            CssClass = "CommandEditSetting",
            Text = "تنظیمات اختصاصی برنامه",
            AutoPostBack = true,
            Name = "EditSetting"
        };
        initLocal2.Add(initLocal5);
        var initLocal6 = new DockCommand {
            CssClass = "CommandDelete",
            Text = "حذف برنامه",
            AutoPostBack = true,
            Name = "Delete",
            OnClientCommand = "DeleteDock"
        };
        initLocal2.Add(initLocal6);
        var initLocal7 = new DockCommand {
            CssClass = "CommandUp",
            Text = "انتقال به بالا",
            AutoPostBack = false,
            Name = "Up",
            OnClientCommand = "SetDockPosition"
        };
        initLocal2.Add(initLocal7);
        var initLocal8 = new DockCommand {
            CssClass = "CommandDown",
            Text = "انتقال به پایین",
            AutoPostBack = false,
            Name = "Down",
            OnClientCommand = "SetDockPosition"
        };
        initLocal2.Add(initLocal8);
        var dockCommands = initLocal2;
        foreach (DockCommand command in dockCommands)
        {
            dock.Commands.Add(command);
        }
        dock.TitlebarTemplate = new DockTitleTemplate(dock, title);
        dock.DockPositionChanged += rd_DockPositionChanged;
        dock.Command += RadDock_Command;
        return dock;
    }

    private RadDock CreateRadDockMenu()
    {
        var initLocal0 = new RadDock {
            DockMode = DockMode.Docked,
            UniqueName = "Menu"
        };
        var dock = initLocal0;
        dock.ID = string.Format("RadDock{0}", dock.UniqueName);
        dock.Title = "Menu";
        dock.Pinned = true;
        dock.Width = Unit.Pixel(60);
        dock.EnableAnimation = true;
        dock.CssClass = "radDock";
        dock.DockMode = DockMode.Docked;
        dock.DefaultCommands = DefaultCommands.None;
        return dock;
    }

    private void DisplayMessage(string message, bool fail)
    {
        _lblFail.Visible = false;
        _lblSucc.Visible = false;
        Label lbl = fail ? _lblFail : _lblSucc;
        lbl.Visible = true;
        lbl.Text = message;
    }

    private void LoadWidget(RadDock dock, Module m)
    {
        try
        {
            var myControl =
                (ModuleControl)
                Page.LoadControl(m.ModuleDef.DeskTopSRC.Substring(0, m.ModuleDef.DeskTopSRC.Length - 5) + "Man.ascx");
            myControl.ModuleConfiguration = m;
            foreach (Control c in myControl.Controls)
            {
                if (c is Label)
                {
                    c.Visible = false;
                }
                else if (c is HyperLink)
                {
                    ((HyperLink) c).Target = "_blank";
                }
            }
            if (!myControl.ModuleConfiguration.HasNoPermission())
            {
                dock.ContentContainer.Controls.Add(myControl);
            }
        }
        catch (Exception)
        {
        }
    }

    protected override void OnInit(EventArgs e)
    {
        SetUpDockZone();
        base.OnInit(e);
    }

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadDockModule.css");
        SetPageControls();
    }

    protected void RadDock_Command(object sender, DockCommandEventArgs e)
    {
        var dock = (RadDock) sender;
        int mId = int.Parse(dock.UniqueName);
        string name = e.Command.Name;
        if (name != null)
        {
            if (name != "EditAuth")
            {
                if (name == "EditInfo")
                {
                    PageState = PageStates.ModuleInfo;
                    ModuleId = mId;
                    SetPageControls();
                }
                else if (name == "EditSetting")
                {
                    PageState = PageStates.Setting;
                    ModuleId = mId;
                    SetPageControls();
                }
                else if (name == "Delete")
                {
                    if (Module.Delete(mId))
                    {
                        dock.Visible = false;
                    }
                    else
                    {
                        DisplayMessage("در حذف برنامه مشکلی پیش آمده است", true);
                    }
                    ModuleId = -1;
                    PageState = PageStates.Load;
                    SetPageControls();
                }
            }
            else
            {
                PageState = PageStates.Roles;
                ModuleId = mId;
                SetPageControls();
            }
        }
    }

    private void rd_DockPositionChanged(object sender, DockPositionChangedEventArgs e)
    {
        string zoneid = e.DockZoneID.Substring(e.DockZoneID.LastIndexOf("_") + 1);
        if (_dockZoneModules[zoneid] is RadDockZone)
        {
            ((RadDock) sender).Undock();
            ((RadDock) sender).Dock((RadDockZone) _dockZoneModules[zoneid]);
        }
    }

    private void SetHeaderModules()
    {
        if ((_header != null) && _header.Visible)
        {
            _header.Controls.Clear();
            
            var lst = Module.GetTabModules(PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, PortalSetting.PortalId).HomeTabID, "header");
            var initLocale = new RadListBox {
                ID = "_rdlstHeaderModules",
                SelectionMode = ListBoxSelectionMode.Single,
                Width = Unit.Percentage(100.0),
                Height = Unit.Pixel(100),
                AllowAutomaticUpdates = false,
                AllowDelete = false,
                AllowTransfer = false,
                AllowReorder = false
            };
            var lb = initLocale;
            foreach (var module in lst)
            {
                lb.Items.Add(new RadListBoxItem(module.ModuleTitle + " (" + module.ModuleDef.FriendlyName + " )", module.ModuleID.ToString()));
            }
            var initLocalf = new Table {
                Width = Unit.Percentage(100.0)
            };
            Table table = initLocalf;
            var initLocal10 = new TableCell {
                Width = Unit.Percentage(90.0)
            };
            var tc1 = initLocal10;
            tc1.Controls.Add(lb);
            var btnPermission = new ImageButton();
            btnPermission.Click += btnPermission_Click;
            btnPermission.ToolTip = @"تعیین دسترسی های برنامه";
            btnPermission.ImageUrl = "~/Images/tools.png";
            btnPermission.ID = "_imgbtnPermission";
            var btnDelete = new ImageButton();
            btnDelete.Click += btnDelete_Click;
            btnDelete.OnClientClick = "CloseCommand(this,null);";
            btnDelete.ImageUrl = "~/Images/delete.png";
            btnDelete.ToolTip = @"حذف برنامه";
            btnDelete.OnClientClick = "javascript:return confirm(\"آیا شما مطمئن هستید؟\");";
            btnDelete.ID = "_imgbtnDelete";
            var btnEditSettings = new ImageButton();
            btnEditSettings.Click += btnEditSettings_Click;
            btnEditSettings.ImageUrl = "~/Images/process.png";
            btnEditSettings.ToolTip = @"تنظیمات اختصاصی برنامه";
            btnEditSettings.ID = "_imgbtnEditSettings";
            var btnEditInfo = new ImageButton();
            btnEditInfo.Click += btnEditInfo_Click;
            btnEditInfo.ToolTip = @"تنظیمات برنامه";
            btnEditInfo.ImageUrl = "~/Images/edit.png";
            btnEditInfo.ID = "_imgbtnEditInfo";
            var tc2 = new TableCell();
            var initLocal11 = new Table {
                Width = Unit.Percentage(100.0)
            };
            var tbl = initLocal11;
            var tr2 = new TableRow();
            var tc3 = new TableCell();
            tc3.Controls.Add(btnPermission);
            tr2.Controls.Add(tc3);
            tbl.Controls.Add(tr2);
            tr2 = new TableRow();
            tc3 = new TableCell();
            tc3.Controls.Add(btnEditInfo);
            tr2.Controls.Add(tc3);
            tbl.Controls.Add(tr2);
            tr2 = new TableRow();
            tc3 = new TableCell();
            tc3.Controls.Add(btnEditSettings);
            tr2.Controls.Add(tc3);
            tbl.Controls.Add(tr2);
            tr2 = new TableRow();
            tc3 = new TableCell();
            tc3.Controls.Add(btnDelete);
            tr2.Controls.Add(tc3);
            tbl.Controls.Add(tr2);
            tc2.Controls.Add(tbl);
            var tr = new TableRow();
            tr.Cells.Add(tc1);
            tr.Cells.Add(tc2);
            table.Rows.Add(tr);
            _header.Controls.Add(table);
            _header.GroupingText = "header";
        }
    }

    private void SetPageControls()
    {
        Module m;
        ModuleControl ms;
        switch (PageState)
        {
            case PageStates.Setting:
                _pnlSettings.Visible = true;
                _pnlInfo.Visible = _pnlRoles.Visible = _pnlTemplate.Visible = false;
                m = Module.GetSingleByID(ModuleId);
                ms = (ModuleControl) Page.LoadControl("~/Admin/ModuleSettings.ascx");
                ms.ID = "moduleSetting" + ModuleId;
                ms.ModuleConfiguration = m;
                _pnlSettings.Controls.Add(ms);
                break;

            case PageStates.Roles:
                _pnlRoles.Visible = true;
                _pnlSettings.Visible = _pnlInfo.Visible = _pnlTemplate.Visible = false;
                m = Module.GetSingleByID(ModuleId);
                foreach (AuthRoleBased item in m.AuthRoleBaseds)
                {
                    var myControl = (ModuleRoles) Page.LoadControl("~/Admin/ModuleRoles.ascx");
                    myControl.ID = "moduleRoles" + ModuleId + item.AuthID;
                    myControl.ModuleConfiguration = m;
                    myControl.AuthId = item.AuthID;
                    _pnlRoles.Controls.Add(myControl);
                }
                break;

            case PageStates.Load:
                _pnlTemplate.Visible = true;
                _pnlSettings.Visible = _pnlRoles.Visible = _pnlInfo.Visible = false;
                SetHeaderModules();
                break;

            case PageStates.ModuleInfo:
                _pnlInfo.Visible = true;
                _pnlSettings.Visible = _pnlRoles.Visible = _pnlTemplate.Visible = false;
                m = Module.GetSingleByID(ModuleId);
                ms = (ModuleControl) Page.LoadControl("~/Admin/ModuleInfo.ascx");
                ms.ID = "moduleInfo" + ModuleId;
                ms.ModuleConfiguration = m;
                _pnlInfo.Controls.Add(ms);
                break;
        }
    }

    private void SetUpDockZone()
    {
        string templateEditPath = PayaTools.SetStyle(Context.User.Identity.Name, false) + "/Templates/" +
                                  TabLoad.Template + "/TemplateEdit.ascx";
        if (!File.Exists(Server.MapPath(templateEditPath)))
        {
            templateEditPath = "~/UI/Templates/TemplateEdit.ascx";
        }
        var temp = (TemplateEdit) Page.LoadControl(templateEditPath);
        foreach (Control ctrl in temp.Controls)
        {
            if ((((ctrl is Panel) && ctrl.Visible) && (ctrl.ID != "header")) && (ctrl.ID != "footer"))
            {
                var initLocal9 = new RadDockZone {
                    Width = new Unit(100.0, UnitType.Percentage),
                    MinWidth = new Unit(50.0, UnitType.Pixel),
                    ID = ctrl.ID + "zone",
                    Height = new Unit(100.0, UnitType.Percentage),
                    MinHeight = new Unit(100.0, UnitType.Pixel)
                };
                var rdz = initLocal9;
                ctrl.Controls.Add(rdz);
                ((Panel) ctrl).GroupingText = ctrl.ID;
                _dockZoneModules.Add(rdz.ID, rdz);
            }
        }
        _header = (Panel) temp.FindControl("header");
        if (TabLoad.ShowMenu)
        {
            var query = (from element in Caching.GetCachedSettingsXml().Descendants("Setting")
                         let xAttribute = element.Attribute("id")
                         where xAttribute != null && xAttribute.Value.ToLower() == "MenuOrientation".ToLower()
                select new { id = element.Attribute("id"), value = element.Attribute("value") }).First();
            if (((query != null) && (query.value.Value.ToUpper() == "VERTICAL")) && (_dockZoneModules["menuzone"] is RadDockZone))
            {
                ((RadDockZone) _dockZoneModules["menuzone"]).Controls.Add(CreateRadDockMenu());
            }
        }
        List<Module> lstModules = TabLoad.Modules;
        foreach (Module module in lstModules)
        {
            if (((module.PaneName.ToLower() != "header") && (module.PaneName.ToLower() != "footer")) && (_dockZoneModules[module.PaneName.ToLower() + "zone"] is RadDockZone))
            {
                var dock = CreateRadDock(module.ModuleID.ToString(), module.ModuleTitle);
                var dz = (RadDockZone) _dockZoneModules[module.PaneName.ToLower() + "zone"];
                dz.Controls.Add(dock);
                LoadWidget(dock, module);
            }
        }
        temp.ID = "ctrl_Template";
        _plhTemplateEdit.Controls.Add(temp);
    }

    // Properties
    private int ModuleId
    {
        get
        {
            if (ViewState["TemplateEditmoduleID"] != null)
            {
                return int.Parse(ViewState["TemplateEditmoduleID"].ToString());
            }
            return -1;
        }
        set
        {
            ViewState["TemplateEditmoduleID"] = value;
        }
    }

    private PageStates PageState
    {
        get
        {
            if (ViewState["TemplateEditPageState"] != null)
            {
                return (PageStates) ViewState["TemplateEditPageState"];
            }
            return PageStates.Load;
        }
        set
        {
            ViewState["TemplateEditPageState"] = value;
        }
    }

    // Nested Types
    private enum PageStates
    {
        Setting,
        Roles,
        Load,
        ModuleInfo
    }

    }
}