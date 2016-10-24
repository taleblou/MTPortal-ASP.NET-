using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Control;
using Telerik.Web.UI;
using System.Linq;

namespace Paya.Admin
{
    public partial class TabsManager : ModuleControl
    {

        // Methods
        protected void _btnBack_Click(object sender, EventArgs e)
        {
            DisplayMessage("", false);
            PageStatus = PageState.Load;
            SetPageControl();
            SetPageData();
        }

        protected void _btnSubmit_Click(object sender, EventArgs e)
        {
            string settings;
            var rtvPages = (RadTreeView)_rcbxTabs.Items[0].FindControl("radTreeTabs");
            int? tabId = 0;
            if (rtvPages.SelectedNode != null)
            {
                tabId = int.Parse(rtvPages.SelectedValue);
                if (tabId == 0)
                {
                    tabId = null;
                }
            }
            if (string.IsNullOrEmpty(TabName))
            {
                settings = string.Format("<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>", new object[] { Convert.ToByte(_cbxMenu.Checked), Convert.ToByte(_cbxHeader.Checked), (_txtTabTitle.Text == "") ? _txtTabName.Text : _txtTabTitle.Text, "Default", "Default" });
                int i = PayaBL.Classes.Tab.Add(_txtTabName.Text, PortalSetting.PortalId, _txtLink.Text, _cbxMainMenu.Checked,
                                               _cbxHorzMenu.Checked, _cbxFooter.Checked, 0,
                                               (sbyte)
                                               ((PayaBL.Classes.Tab.TargetTypes)
                                                Enum.Parse(typeof(PayaBL.Classes.Tab.TargetTypes),
                                                           _ddlTypeLink.SelectedValue)),
                                               Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).
                                                   LanguageID, tabId, _txtKeywords.Text,
                                               _txtDescription.Text, "15;", settings, 0);
                if (i > 0)
                {
                    PageStatus = PageState.Edit;
                    TabId = i;
                    TabName = _txtTabName.Text;
                    SetPageControl();
                    SetPageData();
                    DisplayMessage("صفحه جدید با موفقیت ثبت گردید", false);
                }
                else
                {
                    DisplayMessage("در ثبت صفحه مشکلی پیش آمده است.", true);
                }
            }
            else
            {
                PayaBL.Classes.Tab tabLoad = PayaBL.Classes.Tab.GetSingleByID(TabId);
                tabLoad.ShowMenu = _cbxMenu.Checked;
                tabLoad.ShowHeader = _cbxHeader.Checked;
                tabLoad.ShowTitle = (_txtTabTitle.Text == "") ? _txtTabName.Text : _txtTabTitle.Text;
                settings = tabLoad.GetSetting();
                if (PayaBL.Classes.Tab.Update(tabLoad.TabID, _txtTabName.Text, tabId, _txtLink.Text, _cbxMainMenu.Checked, _cbxHorzMenu.Checked, _cbxFooter.Checked, (byte)((PayaBL.Classes.Tab.TargetTypes)Enum.Parse(typeof(PayaBL.Classes.Tab.TargetTypes), _ddlTypeLink.SelectedValue)), _txtKeywords.Text, _txtDescription.Text, settings))
                {
                    SetLoadPeroperties();
                    DisplayMessage("صفحه  با موفقیت ویرایش گردید", false);
                }
                else
                {
                    DisplayMessage("در ویرایش صفحه مشکلی پیش آمده است.", true);
                }
            }
        }

        protected void _rmpTabs_PageViewCreated(object sender, RadMultiPageEventArgs e)
        {
            string userControlName = "~/Admin/Tabs/" + e.PageView.ID + ".ascx";
            var userControl = (TabControl)Page.LoadControl(userControlName);
            userControl.ID = e.PageView.ID + "_userControl";
            if (TabId != 0)
            {
                PayaBL.Classes.Tab tab = PayaBL.Classes.Tab.GetSingleByID(TabId);
                if (TabName == "")
                {
                    tab.TabName = "";
                }
                userControl.TabLoad = tab;
            }
            else
            {
                userControl.TabLoad = new PayaBL.Classes.Tab
                                          {
                                              TabID = 0
                                          };

            }
            e.PageView.Controls.Add(userControl);
        }

        protected void _rtrvTabs_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            RadTreeNode clickedNode = e.Node;
            DisplayMessage("", false);
            switch (e.MenuItem.Value)
            {
                case "New":
                    TabId = int.Parse(clickedNode.Value);
                    TabName = "";
                    PageStatus = PageState.Add;
                    _rtsTabs.Tabs[0].PageView.Selected = true;
                    _rtsTabs.Tabs[0].Selected = true;
                    SetPageControl();
                    SetPageData();
                    return;

                case "Edit":
                    TabId = int.Parse(clickedNode.Value);
                    TabName = clickedNode.Text;
                    PageStatus = PageState.Edit;
                    if (_rmpTabs.PageViews.Count > 1)
                    {
                        for (int i = 1; i < _rmpTabs.PageViews.Count; i++)
                        {
                            _rmpTabs.PageViews.RemoveAt(i);
                        }
                    }
                    break;

                case "Copy":
                case "AllPage":
                    return;

                case "Delete":
                    try
                    {
                        if (!PayaBL.Classes.Tab.Delete(int.Parse(clickedNode.Value)))
                        {
                            DisplayMessage("در حذف صفحه مشکلی پیش آمده است", true);
                        }
                        else
                        {
                            DisplayMessage("صفحه با موفقیت حذف گردید.", false);
                            SetPageControl();
                            SetPageData();
                        }
                    }
                    catch (Exception)
                    {
                        DisplayMessage("در حذف صفحه خطایی رخ داده است", true);
                    }
                    return;

                case "Up":
                    try
                    {
                        int uptabId = int.Parse(clickedNode.Prev.Value);
                        int tabId = int.Parse(clickedNode.Value);
                        if (!PayaBL.Classes.Tab.UpdateTabOrder(tabId, uptabId))
                        {
                            DisplayMessage("در جابجایی صفحات مشکلی پیش آمده است", true);
                        }
                        else
                        {
                            DisplayMessage("جابجایی صفحات با موفقیت انجام شد.", false);
                            SetPageControl();
                            SetPageData();
                        }
                    }
                    catch (Exception)
                    {
                        DisplayMessage("در جابجایی صفحات خطایی رخ داده است", true);
                    }
                    return;

                case "Down":
                    try
                    {
                        int downtabId = int.Parse(clickedNode.Next.Value);
                        if (!PayaBL.Classes.Tab.UpdateTabOrder(downtabId, int.Parse(clickedNode.Value)))
                        {
                            DisplayMessage("در جابجایی صفحات مشکلی پیش آمده است", true);
                        }
                        else
                        {
                            DisplayMessage("جابجایی صفحات با موفقیت انجام شد.", false);
                            SetPageControl();
                            SetPageData();
                        }
                    }
                    catch (Exception)
                    {
                        DisplayMessage("در جابجایی صفحات خطایی رخ داده است", true);
                    }
                    return;

                default:
                    return;
            }
            _rtsTabs.Tabs[0].PageView.Selected = true;
            _rtsTabs.Tabs[0].Selected = true;
            SetPageControl();
            SetPageData();
        }

        protected void _rtrvTabs_NodeEdit(object sender, RadTreeNodeEditEventArgs e)
        {
            if (int.Parse(e.Node.Value) != 0)
            {
                RadTreeNode nodeEdited = e.Node;
                string newText = e.Text;
                nodeEdited.Text = newText;
                nodeEdited.Value = e.Node.Value;
                PayaBL.Classes.Tab.UpdateTabName(int.Parse(e.Node.Value), newText);
            }
        }

        protected void _rtsTabs_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Value == @"Properties")
            {
                e.Tab.PageViewID = "Properties";
            }
            else
            {
                Control item = _rmpTabs.FindControl(TabSelectName);
                if (item != null)
                {
                    _rmpTabs.PageViews.Remove(item);
                }
                AddPageView(e.Tab);
                TabSelectName = e.Tab.Value;
            }
            e.Tab.PageView.Selected = true;
        }

        private void AddPageView(RadTab tab)
        {
            RadPageView initLocal2 = new RadPageView
            {
                ID = tab.Value
            };
            RadPageView pageView = initLocal2;
            _rmpTabs.PageViews.Add(pageView);
            pageView.CssClass = "pageView";
            tab.PageViewID = pageView.ID;
        }

        private void AddTab(string tabName, string value)
        {
            var tab = new RadTab
            {
                Text = tabName,
                Value = value
            };
            _rtsTabs.Tabs.Add(tab);
            if (value == "Properties")
            {
                tab.PageViewID = value;
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
            Page.Title = PortalSetting.PortalName + @" مدیریت صفحات";
            PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadControlFont.css");
            if (!Page.IsPostBack)
            {
                AddTab("مشخصات صفحه", "Properties");
                AddTab("تعیین دسترسی صفحه", "Permission");
                AddTab("قالب و پوسته صفحه", "TemplateTheme");
                AddTab("برنامه های صفحه", "ModulesManagment");
            }
            if (Session["TabId"] != null)
            {
                PayaBL.Classes.Tab tab = PayaBL.Classes.Tab.GetSingleByID((int)Session["TabId"]);
                if (tab != null)
                {
                    TabId = tab.TabID;
                    PageStatus = PageState.Edit;
                    Session["TabId"] = null;
                }
            }
            SetPageData();
            SetPageControl();
        }

        private void SetcboData(int tabId, int? parentId)
        {
            var tree = (RadTreeView)_rcbxTabs.Items[0].FindControl("radTreeTabs");
            var portalTabs = PayaBL.Classes.Tab.GetTabsArray(PortalSetting.PortalId, Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).LanguageID);
            tree.Nodes.Clear();
            tree.Nodes.Insert(0, new RadTreeNode("-----", "0"));
            foreach (PayaBL.Classes.Tab t in portalTabs)
            {
                if (tabId != t.TabID)
                {
                    RadTreeNode node = new RadTreeNode(string.Concat(new object[] { t.TabName, " (", t.TabID, ")" }), t.TabID.ToString());
                    if (t.ParentID == null)
                    {
                        tree.Nodes.Add(node);
                    }
                    else
                    {
                        RadTreeNode pNode = tree.FindNodeByValue(t.ParentID.ToString());
                        if (pNode != null)
                        {
                            pNode.Nodes.Add(node);
                        }
                    }
                    if (parentId != null)
                    {
                        if (node.Value == parentId.ToString())
                        {
                            node.ExpandParentNodes();
                            node.Expanded = true;
                            node.Selected = true;
                            _rcbxTabs.Items[0].Text = node.Text;
                        }
                    }
                }
            }
            if ((tabId != 0) && (parentId == null))
            {
                tree.Nodes[0].Selected = true;
                _rcbxTabs.Items[0].Text = tree.Nodes[0].Text;
            }
        }

        private void SetLoadPeroperties()
        {
            ListItem item;
            if (!string.IsNullOrEmpty(TabName))
            {
                var tabLoad = PayaBL.Classes.Tab.GetSingleByID(TabId);
                _txtTabName.Text = tabLoad.TabName;
                _txtTabTitle.Text = tabLoad.ShowTitle;
                _txtLink.Text = tabLoad.Url;
                _txtKeywords.Text = tabLoad.Keywords;
                foreach (ListItem listItem in _ddlTypeLink.Items)
                {
                    listItem.Selected = false;
                }
                item = _ddlTypeLink.Items.FindByValue(Enum.GetName(typeof(PayaBL.Classes.Tab.TargetTypes), tabLoad.Target));
                if (item != null)
                {
                    item.Selected = true;
                }
                _txtDescription.Text = tabLoad.Description;
                _cbxMainMenu.Checked = tabLoad.ShowVertical;
                _cbxHorzMenu.Checked = tabLoad.ShowHorizontal;
                _cbxFooter.Checked = tabLoad.ShowFooter;
                _cbxMenu.Checked = tabLoad.ShowMenu;
                _cbxHeader.Checked = tabLoad.ShowHeader;
                SetcboData(tabLoad.TabID, tabLoad.ParentID);
                _btnSubmit.Text = @"ویرایش صفحه";
            }
            else
            {
                _txtTabName.Text = "";
                _txtTabTitle.Text = "";
                _txtLink.Text = "";
                _txtKeywords.Text = "";
                item = _ddlTypeLink.Items.FindByValue("Self");
                if (item != null)
                {
                    item.Selected = true;
                }
                _txtDescription.Text = "";
                _cbxMainMenu.Checked = false;
                _cbxHorzMenu.Checked = false;
                _cbxFooter.Checked = false;
                _cbxMenu.Checked = false;
                _cbxHeader.Checked = false;
                _btnSubmit.Text = @"ثبت اطلاعات";
                SetcboData(0, TabId);
            }
        }

        protected void _btnTabSearch_Click(object sender, EventArgs e)
        {
            SetTreeView(_txtTabSearch.Text);
            //SetPageControl();
        }
        protected void _txtTabSearch_TextChanged(object sender, EventArgs e)
        {
            SetTreeView(_txtTabSearch.Text);
            //SetPageControl();
        }


        private void SetPageControl()
        {
            switch (PageStatus)
            {
                case PageState.Load:
                    _pnlTreeView.Visible = true;
                    _pnlTabView.Visible = false;
                    break;

                case PageState.Add:
                    _pnlTreeView.Visible = false;
                    _pnlTabView.Visible = true;
                    _rtsTabs.Tabs[1].Enabled = false;
                    _rtsTabs.Tabs[2].Enabled = false;
                    _rtsTabs.Tabs[3].Enabled = false;
                    break;

                case PageState.Edit:
                    _pnlTreeView.Visible = false;
                    _pnlTabView.Visible = true;
                    _rtsTabs.Tabs[1].Enabled = true;
                    _rtsTabs.Tabs[2].Enabled = true;
                    _rtsTabs.Tabs[3].Enabled = true;
                    break;
            }
        }

        private void SetPageData()
        {
            switch (PageStatus)
            {
                case PageState.Load:
                    SetTreeView("");
                    TabId = 0;
                    TabName = "";
                    break;

                case PageState.Add:
                    SetLoadPeroperties();
                    break;

                case PageState.Edit:
                    SetLoadPeroperties();
                    break;
            }
        }

        private void SetTreeView(string searchKey)
        {
            _rtrvTabs.Nodes.Clear();
            var rootNode = new RadTreeNode
                               {
                                   Text = @"تمام صفحات",
                                   Value = @"0",
                                   ContextMenuID = "RootContextMenuTabs"
                               };

            int langId = Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).LanguageID;
            List<PayaBL.Classes.Tab> portalTabs = PayaBL.Classes.Tab.GetTabsArray(PortalSetting.PortalId, langId);
            var tabs = new List<PayaBL.Classes.Tab>();
            if (!string.IsNullOrEmpty(searchKey))
            {
                tabs =
                    portalTabs.Where(
                        o =>
                        o.TabName.IndexOf(searchKey, StringComparison.CurrentCultureIgnoreCase) > -1).ToList();
                if (tabs.Count == 0)
                {
                    rootNode.Nodes.Clear();
                    rootNode = new RadTreeNode
                                   {
                                       Text = @"صفحه ای با این اسم یافت نشد",
                                       Value = @"0",
                                       ContextMenuID = "RootContextMenuTabs"
                                   };
                }

                //int? parentID = 0;
                //do
                //{
                //    var tabs = new List<PayaBL.Classes.Tab>();
                //    foreach (var temp in t)
                //        if (temp.ParentID != null && t.SingleOrDefault(o => o.TabID == temp.ParentID) == null)
                //            tabs.Add(portalTabs.Single(oo => oo.TabID == temp.ParentID));

                //    if (tabs.Count != 0)
                //        t.AddRange(tabs);
                //    else
                //        parentID = null;
                //} while (parentID == null);
                //portalTabs = t;

            }
            _rtrvTabs.Nodes.Add(rootNode);
            int homeTabId =
                PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, PortalSetting.PortalId).HomeTabID;
            foreach (PayaBL.Classes.Tab t in portalTabs)
            {
                var node = new RadTreeNode(t.TabName, t.TabID.ToString())
                               {
                                   ToolTip = @"کد صفحه : " + t.TabID.ToString()
                               };

                if (t.TabID == homeTabId)
                {
                    node.Attributes["Deleted"] = "1";
                }
                if (t.ParentID == null)
                {
                    if (tabs.Count > 0 && tabs.SingleOrDefault(o => o.TabName == node.Text) != null)
                    {
                        node.BackColor = Color.Yellow;
                        //node.ParentNode.Expanded = true;
                    }
                    rootNode.Nodes.Add(node);
                }
                else
                {
                    var pNode = rootNode.TreeView.FindNodeByValue(t.ParentID.ToString());
                    if (pNode != null)
                    {
                        if (tabs.Count > 0 && tabs.SingleOrDefault(o => o.TabName == node.Text) != null)
                        {
                            node.BackColor = Color.Yellow;
                            //node.ParentNode.Expanded = true;
                        }
                        pNode.Nodes.Add(node);
                        pNode.Attributes["Deleted"] = "1";
                    }
                }
            }
            if (tabs.Count > 0)
            {
                _rtrvTabs.Nodes[0].Expanded = true;
                foreach (var tab in tabs)
                {
                    _rtrvTabs.FindNodeByText(tab.TabName).ExpandParentNodes();
                }

            }
            foreach (var node in _rtrvTabs.Nodes.Cast<RadTreeNode>().Where(node => node.BackColor == Color.Yellow))
                node.ExpandParentNodes();
            
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

        private int TabId
        {
            get
            {
                if (ViewState["TabId"] == null)
                {
                    return 0;
                }
                return (int)ViewState["TabId"];
            }
            set
            {
                ViewState["TabId"] = value;
            }
        }

        private string TabName
        {
            get
            {
                if (ViewState["TabName"] == null)
                {
                    return "";
                }
                return ViewState["TabName"].ToString();
            }
            set
            {
                ViewState["TabName"] = value;
            }
        }

        private string TabSelectName
        {
            get
            {
                if (ViewState["TabSelectName"] == null)
                {
                    return "";
                }
                return ViewState["TabSelectName"].ToString();
            }
            set
            {
                ViewState["TabSelectName"] = value;
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