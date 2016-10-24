using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class ModuleInfo : ModuleControl
    {
        protected Button _btnSubmit;
        protected CheckBox _cbxShowInAllTabs;
        protected Label _lblFail;
        protected Label _lblSucc;
        protected RadComboBox _rdcboModuleLayout;
        protected RadComboBox _rdcboModuleTabs;
        protected RadEditor _rdedModuleLayout;
        protected RadNumericTextBox _rdtxtCachTime;
        protected RadNumericTextBox _rdtxtRefreshCachTime;
        protected TextBox _txtModuleTitle;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;

        // Methods
        protected void _btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
            }
            else if ((_rdcboModuleLayout.SelectedValue == "DesignedLayout.ascx") && (this._rdedModuleLayout.Text.ToLower().IndexOf("[modulebody]") == -1))
            {
                DisplayMessage("در قالب برنامه می بایست [modulebody] وجود داشته باشد.", true);
            }
            else
            {
                int tabid = ModuleConfiguration.TabID;
                var rtvPages = (RadTreeView)_rdcboModuleTabs.Items[0].FindControl("radTreeTabs");
                if (rtvPages.SelectedNode != null)
                {
                    tabid = int.Parse(rtvPages.SelectedValue);
                }
                if (tabid != base.ModuleConfiguration.TabID)
                {
                    int mcount = Module.GetModulesOfTab(tabid).Count;
                    if (!(base.ModuleConfiguration.ModuleDef.IsUserControl() || (mcount <= 0)))
                    {
                        DisplayMessage("امکان ثبت این برنامه در این صفحه وجود ندارد!<br>برنامه " + base.ModuleConfiguration.ModuleTitle + " باید در یک صفحه جداگانه بدون وجود هیچ برنامه دیگری ثبت شود", true);
                        return;
                    }
                    if (mcount == 1)
                    {
                        Module module = Module.GetModulesOfTab(tabid)[0];
                        if (!module.ModuleDef.IsUserControl())
                        {
                            DisplayMessage("امکان ثبت این برنامه در این صفحه وجود ندارد!<br>برنامه " + module.ModuleTitle + " باید در یک صفحه جداگانه بدون وجود هیچ برنامه دیگری ثبت شود", true);
                            return;
                        }
                    }
                }
                if (_cbxShowInAllTabs.Checked)
                {
                    tabid = PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, PortalSetting.PortalId).HomeTabID;
                }
                string skinhtml = (this._rdcboModuleLayout.SelectedValue == "DesignedLayout.ascx") ? this._rdedModuleLayout.Content : "";
                if (Module.UpdateModuleInfo(ModuleConfiguration.ModuleID, _txtModuleTitle.Text, tabid, _rdcboModuleLayout.SelectedValue, skinhtml, (int)this._rdtxtRefreshCachTime.Value.Value, (int)this._rdtxtCachTime.Value.Value, this._cbxShowInAllTabs.Checked))
                {
                    DisplayMessage("اطلاعات با موفقیت ثبت شد.", false);
                }
                else
                {
                    DisplayMessage("در ثبت اطلاعات مشکلی پیش آمده است.", true);
                }
            }
        }

        private void DisplayMessage(string message, bool fail)
        {
            this._lblFail.Visible = false;
            this._lblSucc.Visible = false;
            Label lbl = fail ? this._lblFail : this._lblSucc;
            lbl.Visible = true;
            lbl.Text = message;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string script = "<script type=\"text/javascript\"> function nodeClicking(sender, args) { var comboBox = $find(\"" + this._rdcboModuleTabs.ClientID + "\"); var node = args.get_node(); comboBox.set_text(node.get_text()); comboBox.trackChanges(); comboBox.get_items().getItem(0).set_value(node.get_text()); comboBox.commitChanges(); comboBox.hideDropDown(); } function StopPropagation(e) { if (!e) { e = window.event; } e.cancelBubble = true; } function OnClientDropDownOpenedHandler(sender, eventArgs) { var tree = sender.get_items().getItem(0).findControl(\"radTreeTabs\"); var selectedNode = tree.get_selectedNode(); if (selectedNode) { selectedNode.scrollIntoView(); } } function OnClientSelectedIndexChanged(sender, eventArgs) { var item = eventArgs.get_item(); var tr = document.getElementById('trEditor'); if (item.get_value() == \"DesignedLayout.ascx\") tr.style.display = ''; else tr.style.display = 'none'; } </script> <script type=\"text/javascript\"> function pageLoad() { var comboBox = $find(\"" + this._rdcboModuleLayout.ClientID + "\"); if (comboBox != null) { var item = comboBox.get_selectedItem(); var tr = document.getElementById('trEditor'); if (item.get_value() == \"DesignedLayout.ascx\") tr.style.display = ''; else tr.style.display = 'none'; } } </script>";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClientSelectedIndexScript", script, false);
            SetPageData();
        }

        private void SetPageData()
        {
            int portalid = PortalSetting.SingleUserBase ? 0 : PortalSetting.PortalId;
            if (!Directory.Exists(Server.MapPath("~/Upload/Portal" + portalid + "/Modules")))
            {
                Directory.CreateDirectory(base.Server.MapPath("~/Upload/Portal" + portalid + "/Modules"));
            }
            _rdedModuleLayout.ImageManager.ViewPaths = _rdedModuleLayout.ImageManager.UploadPaths = _rdedModuleLayout.FlashManager.DeletePaths = new string[] { "~/Upload/Portal" + portalid + "/Modules" };
            _txtModuleTitle.Text = ModuleConfiguration.ModuleTitle;
            _rdtxtCachTime.Text = ModuleConfiguration.CacheTime.ToString();
            _rdedModuleLayout.Content = ModuleConfiguration.SkinHTML;
            _cbxShowInAllTabs.Checked = ModuleConfiguration.ShowinAllTab;
            DisplayMessage("", false);
            if (ModuleConfiguration.ModuleDef.Updatable)
            {
                _rdtxtRefreshCachTime.Text = ModuleConfiguration.UpdatePeriod.ToString();
                _rdtxtRefreshCachTime.Enabled = true;
            }
            else
            {
                this._rdtxtRefreshCachTime.Text = @"0";
                this._rdtxtRefreshCachTime.Enabled = false;
            }
            this.SetTreeView(base.ModuleConfiguration.TabID);
            if (this._rdcboModuleLayout.Items.Count == 0)
            {
                XDocument doc = Caching.GetCachedSettingsXml();
                try
                {
                    var query = from element in doc.Descendants("ModuleLayout") select new { name = element.Attribute("name"), control = element.Attribute("control") };
                    foreach (var item in query)
                    {
                        this._rdcboModuleLayout.Items.Add(new RadComboBoxItem(item.name.Value, item.control.Value));
                    }
                    this._rdcboModuleLayout.Items.Add(new RadComboBoxItem("طراحی قالب", "DesignedLayout.ascx"));
                }
                catch (Exception)
                {
                }
            }
            RadComboBoxItem layout = this._rdcboModuleLayout.FindItemByValue(base.ModuleConfiguration.Container);
            if (layout != null)
            {
                layout.Selected = true;
            }
        }

        private void SetTreeView(int tabid)
    {
        var tree = (RadTreeView) this._rdcboModuleTabs.Items[0].FindControl("radTreeTabs");
        tree.Nodes.Clear();
        List<PayaBL.Classes.Tab> portalTabs = PayaBL.Classes.Tab.GetTabsArray(PortalSetting.PortalId, Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).LanguageID);
        foreach (PayaBL.Classes.Tab t in portalTabs)
        {
            var initLocal2 = new RadTreeNode(t.TabName, t.TabID.ToString()) {
                ToolTip = @"کد صفحه : " + t.TabID.ToString()
            };
            RadTreeNode node = initLocal2;
            if (t.ParentID == 0)
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
            if (node.Value == tabid.ToString())
            {
                node.ExpandParentNodes();
                node.Expanded = true;
                node.Selected = true;
                this._rdcboModuleTabs.Items[0].Text = node.Text;
            }
        }
    }

    }
}