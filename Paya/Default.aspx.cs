using System;
using System.IO;
using Paya.UI.Templates;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Control;
using UrlRewritingNet.Configuration;

namespace Paya
{
    public partial class Default : PortalPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BlackListManager.IsIpAddressBanned(Request.UserHostAddress))
                PayaTools.AccessDenied();
            int portalId = PortalSetting.PortalId;
            int tabId = PayaTools.QueryStringInt("TabID", -1);
            if (tabId == -1)
            {
                tabId = PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, portalId).HomeTabID;
            }
            //if (
            //    !(((!Request.IsAuthenticated || !PortalSetting.UserMustChangePasswordOnFirstLogin) ||
            //       !CurrentUserId.HasValue)))|| CustomProfileBl.HasChangedPassword))
            //{
            //    Response.Redirect(
            //        PayaTools.BuildUrl("~/PageManagment.aspx?ModuleID=" + Module.GetTabModulesObjs(tabId)[0].ModuleId +
            //                        "&Page=ChangePassword"));
            //}
            Tab tab = Tab.GetSingleByID(tabId);
            if (tab == null)
            {
                PayaTools.AccessDenied();
            }
            else
            {
                if (!Role.IsInRoles(tab.Roles))
                {
                    PayaTools.AccessDenied();
                }
                Title = PortalSetting.PortalName + "-" + tab.ShowTitle;
                _keywordsMeta.Content = tab.Keywords;
                _descMeta.Content = tab.Description;
                string templatePath = PayaTools.SetStyle(Context.User.Identity.Name, false) + "/Templates/" +
                                      tab.Template +
                                      "/Template.ascx";
                if (!File.Exists(Server.MapPath(templatePath)))
                {
                    templatePath = "~/UI/Templates/Template.ascx";
                }
                var temp = (Template)Page.LoadControl(templatePath);
                temp.TabLoad = tab;
                temp.ID = "PageTemplate_ctrl";
                _plhMain.Controls.Add(temp);
                PayaTools.RegisterJavaScriptInclude(Page, "jquery",
                                                    PortalSetting.PortalPath + "/Scripts/jquery.min.js");
                //if (Ispaya)
                //{
                //    Monitoring.AddMonitoring(base.CurrentUserId, tabId, "PageRequest");
                //}
            }
        }
    }
}