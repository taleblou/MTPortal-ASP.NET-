using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paya.UI.Templates;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using PayaBL.Control;
using Telerik.Web.UI;
using Telerik.Web.UI.Dock;

namespace Paya.Admin
{
    public partial class ContentsManager : ModuleControl
    {
        // Fields
        protected Button _btnTabSearch;
        private Hashtable _dockZoneModules = new Hashtable();
        protected PlaceHolder _phlModules;
        protected RadAjaxLoadingPanel _rdlpContentsManager;
        protected RadAjaxPanel _rdpContentsManager;
        protected RadTreeView _rdtrvTabs;
        protected TextBox _txtTabSearch;
        protected Panel Panel1;

        // Methods
        protected void _rdtrvTabs_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            TabId = int.Parse(e.Node.Value);
            _phlModules.Controls.Clear();
            _dockZoneModules.Clear();
            SetUpDockZone();
        }

        private RadDock CreateRadDock(string id, string title)
        {
             var dock= new RadDock
            {
                DockMode = DockMode.Docked,
                UniqueName = id
            };
            dock.ID = string.Format("RadDock{0}", dock.UniqueName);
            dock.Title = title;
            dock.EnableAnimation = true;
            dock.CssClass = "radDock";
            dock.DockMode = DockMode.Docked;
            dock.Width = Unit.Pixel(60);
            dock.DefaultCommands = DefaultCommands.None;
            dock.Pinned = true;
            return dock;
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
                        ((HyperLink)c).Target = "_blank";
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
            PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadControlFont.css");
            SetTreeView();
            if (!IsPostBack)
            {
                SetUpDockZone();
                _rdtrvTabs.FindNodeByValue(TabId.ToString()).Selected = true;
            }
        }

        private void SetTreeView()
        {
            _rdtrvTabs.Nodes.Clear();
            var portalTabs = PayaBL.Classes.Tab.GetTabsArray(PortalSetting.PortalId,
                                                                                  Language.
                                                                                      GetSingleLangaugeByCultureName(
                                                                                          PayaTools.CurrentCulture).
                                                                                      LanguageID);
            foreach (var t in portalTabs)
            {
                var node = new RadTreeNode(t.TabName, t.TabID.ToString())
                {
                    ToolTip = @"کد صفحه : " + t.TabID,
                    PostBack = true
                };
                if (t.ParentID == null)
                {
                    _rdtrvTabs.Nodes.Add(node);
                }
                else
                {
                    RadTreeNode pNode = _rdtrvTabs.FindNodeByValue(t.ParentID.ToString());
                    if (pNode != null)
                    {
                        pNode.Nodes.Add(node);
                    }
                }
            }
        }

        private void SetUpDockZone()
        {
            PayaBL.Classes.Tab tabLoad = PayaBL.Classes.Tab.GetSingleByID(TabId);
            string templateEditPath = PayaTools.SetStyle(Context.User.Identity.Name, false) + "/Templates/" +
                                      tabLoad.Template + "/TemplateEdit.ascx";
            if (!File.Exists(Server.MapPath(templateEditPath)))
            {
                templateEditPath = "~/UI/Templates/TemplateEdit.ascx";
            }
            TemplateEdit temp = (TemplateEdit)Page.LoadControl(templateEditPath);
            foreach (Control ctrl in temp.Controls)
            {
                if ((((ctrl is Panel) && ctrl.Visible) && (ctrl.ID != "header")) && (ctrl.ID != "footer"))
                {
                    RadDockZone initLocal1 = new RadDockZone
                    {
                        Width = new Unit(100.0, UnitType.Percentage),
                        MinWidth = new Unit(50.0, UnitType.Pixel),
                        ID = ctrl.ID + "zone",
                        Height = new Unit(100.0, UnitType.Percentage),
                        MinHeight = new Unit(100.0, UnitType.Pixel)
                    };
                    RadDockZone rdz = initLocal1;
                    ctrl.Controls.Add(rdz);
                    ((Panel)ctrl).GroupingText = ctrl.ID;
                    _dockZoneModules.Add(rdz.ID, rdz);
                }
            }
            if (tabLoad.ShowMenu)
            {
                var query = (from element in Caching.GetCachedSettingsXml().Descendants("Setting")
                             let xAttribute = element.Attribute("id")
                             where xAttribute != null && xAttribute.Value.ToLower() == "MenuOrientation".ToLower()
                             select new { id = element.Attribute("id"), value = element.Attribute("value") }).First();
                if (((query != null) && (query.value.Value.ToUpper() == "VERTICAL")) && (_dockZoneModules["menuzone"] is RadDockZone))
                {
                    ((RadDockZone)_dockZoneModules["menuzone"]).Controls.Add(CreateRadDock("Menu", "Menu"));
                }
            }
            List<Module> lstModules = tabLoad.Modules;
            foreach (Module module in lstModules)
            {
                if (((module.PaneName.ToLower() != "header") && (module.PaneName.ToLower() != "footer")) && (_dockZoneModules[module.PaneName.ToLower() + "zone"] is RadDockZone))
                {
                    RadDock dock = CreateRadDock(module.ModuleID.ToString(), module.ModuleTitle);
                    RadDockZone dz = (RadDockZone)_dockZoneModules[module.PaneName.ToLower() + "zone"];
                    dz.Controls.Add(dock);
                    LoadWidget(dock, module);
                }
            }
            temp.ID = "ctrl_Template";
            _phlModules.Controls.Add(temp);
        }

        // Properties
        private int TabId
        {
            get
            {
                if (ViewState["TabId"] == null)
                {
                    return PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, PortalSetting.PortalId).HomeTabID;
                }
                return (int)ViewState["TabId"];
            }
            set
            {
                ViewState["TabId"] = value;
            }
        }

        // Nested Types
        private enum AuthRole
        {
            ModuleManagment = 13
        }

    }
}