using System;
using PayaBL.Control;
using System.Text;
using System.Collections.Generic;
using PayaBL.Classes;
using System.Threading;
using PayaBL.Common;
using System.Xml.Linq;
using System.Linq;
using PayaBL.Common.PortalCach;

namespace Paya.Admin
{
    public partial class AdminPanel : ModuleControl
    {
        public string RightToLeft;
        public string Orientation;
        public string MainMenuStyle = "MM";
        public string SubMenuStyle = "SM";
        public int Levels = -1;
        public int BaseLevel;
        private StringBuilder _strBldr;
        private List<PayaBL.Classes.Tab> _lstTabs;
        private Language _lng;
        private const string MenuItemSchema = "<li@seperator style='@style'><a href='@href' class='@class'><span class='@spanclass'>@title</span></a>";


        protected void Page_Load(object sender, EventArgs e)
        {
            _strBldr = new StringBuilder();
            _lng = Language.GetSingleLangaugeByCultureName(Thread.CurrentThread.CurrentCulture.Name);
            RightToLeft = !_lng.Direction ? "true" : "false";
            Orientation = "horizontal";

            CreateMainMenu();


            string script;
            if (ModuleConfiguration == null || ModuleConfiguration.ModuleID == 0)
            {
                if (_strBldr.Length > 0)
                {
                    _strBldr.Remove(0, 4);
                    _strBldr.Insert(0, string.Format("<ul id='Menu{0}' class='" + MainMenuStyle + "'>", 1));
                }
                string theme;
                if (PayaTools.QueryStringInt("TabID", -1) == -1)
                {
                    int tabid = PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, PortalSetting.PortalId).HomeTabID;
                    theme = PayaBL.Classes.Tab.GetSingleByID(tabid).Theme;
                }
                else
                {
                    theme = PayaBL.Classes.Tab.GetSingleByID(PayaTools.QueryStringInt("TabID")).Theme;
                }
                script = "<script type=\"text/javascript\" src=\"" +
                         PayaTools.SetStyle(Context.User.Identity.Name, true) + "/Themes/" + theme +
                         "/c_config.js\"> </script>";
                PayaTools.RegisterJavaScriptBlock(Page, "MenuScript", script, false);

                string width = Orientation == "vertical" ? "100%" : "auto";
                script = "<script type=\"text/javascript\">c_rightToLeft=" + RightToLeft + ";c_menus['Menu1']=[['"
                    + Orientation + "','relative','0','0',false,false,0,0,'" + width + "','" + MainMenuStyle +
                         "',false],[5,1,'auto','auto','auto','" + SubMenuStyle +
                         "',false]];</script>";
                PayaTools.RegisterJavaScriptBlock(Page, "MenuScript1", script, false);
            }
            else
            {
                if (_strBldr.Length > 0)
                {
                    _strBldr.Remove(0, 4);
                    _strBldr.Insert(0,
                                    string.Format("<ul id='Menu{0}' class='" + MainMenuStyle + "'>",
                                                  ModuleConfiguration.ModuleID));
                }
                script = "<script type=\"text/javascript\" src=\"" +
                         PayaTools.SetStyle(Context.User.Identity.Name, true) + "/Themes/" + ModuleConfiguration.ModuleTab.Theme +
                         "/c_config.js\"></script>";
                PayaTools.RegisterJavaScriptBlock(Page, "MenuScript", script, false);

                string width = Orientation == "vertical" ? "100%" : "auto";
                script = "<script type=\"text/javascript\">c_rightToLeft=" + RightToLeft + ";c_menus['Menu" + ModuleConfiguration.ModuleID +
                         "']=[['" + Orientation + "','relative','0','0',false,false,0,0,'" + width + "','" + MainMenuStyle +
                         "',false],[5,1,'auto','auto','auto','" + SubMenuStyle +
                         "',false]];</script>";
                PayaTools.RegisterJavaScriptBlock(Page, "MenuScript" + ModuleConfiguration.ModuleID, script, false);
            }

            if (!Page.ClientScript.IsClientScriptBlockRegistered("MenuScriptStartup"))
            {
                script = "<script type=\"text/javascript\" src=\"" +
                         PortalSetting.PortalPath +
                         "/Modules/menu/c_smartmenus.js\"></script>";
                Page.ClientScript.RegisterStartupScript(GetType(), "MenuScriptStartup", script, false);
            }
            PayaTools.RegisterJavaScriptInclude(Page, "MenuScriptStartup", PortalSetting.PortalPath +
                         "/Modules/menu/c_smartmenus.js");
              ltrMenu.Text = _strBldr.ToString();
        }

        private void CreateMainMenu()
        {
            try
            {
                XDocument doc = Caching.GetCachedSettingsXml();
                var query = from c in doc.Descendants("Setting")
                            where c.Attribute("id").Value == "MenuOrientation"
                            select c.Attribute("value");
                if (query.Any(element => element.Value.ToUpper() == "VERTICAL"))
                {
                    Orientation = "vertical";
                }
            }
            catch
            {
            }

            _lstTabs = PayaBL.Classes.Tab.GetTabsTree(PortalSetting.PortalId, _lng.LanguageID);
            //var sublist =
            //    _lstTabs.Where(
            //        o =>
            //        o.ParentID == 0 && o.ShowVertical && o.NestLevel == 0 &&
            //        o.IsReserved == (int) Tab.ReservedType.NotReserved).ToList();
            RecursivyCreateMenu(_lstTabs.Where(o => o.ParentID == null).ToList());
        }

        private void RecursivyCreateMenu(List<PayaBL.Classes.Tab> lstTab)
        {
            //string menuClass = strBldr.Length == 0 ? "MainMenu" : "SubMenu";
            string menuClass = _strBldr.Length == 0 ? MainMenuStyle : SubMenuStyle;
            string style = _strBldr.Length == 0 ? "width:100%" : "";
            string spanClass = menuClass + "Item";
            if (lstTab == null || lstTab.Count == 0)
                return;
            bool flag = false;
            int? parentID;
            foreach (PayaBL.Classes.Tab item in lstTab)
            {
                if (!Role.IsInRoles(item.Roles))
                    continue;
                string href = PayaTools.BuildUrl("~/Default.aspx?TabID=" + item.TabID);

                if (item.Url.Trim() != "")
                    href = item.Url;
                //switch (item.Target)
                //{
                //    case Tab.TargetTypes.Blank:
                //        href += "' target='_blank";
                //        break;
                //    case Tab.TargetTypes.Empty:
                //        href = "javascript:void(0);";
                //        break;
                //}

                if (!flag)
                {
                    _strBldr.Append("<ul>");
                    flag = true;
                    _strBldr.Append(MenuItemSchema.Replace("@href", href)
                                        .Replace("@title", item.TabName)
                                        .Replace("@class", menuClass).Replace("@spanclass", spanClass).Replace(
                                            "@seperator", "").Replace("@style", style));
                }
                else
                    _strBldr.Append(MenuItemSchema.Replace("@href", href)
                                        .Replace("@title", item.TabName)
                                        .Replace("@class", menuClass).Replace("@spanclass", spanClass).Replace(
                                            "@seperator", "").Replace("@style", style));

                if (Levels == -1)
                {
                    PayaBL.Classes.Tab item1 = item;
                    RecursivyCreateMenu(_lstTabs.Where(o => o.ShowVertical && o.ParentID == item1.TabID).ToList());
                }
                _strBldr.Append("</li>");
            }
            if (flag)
                _strBldr.Append("</ul> ");
        }
    
    }
}