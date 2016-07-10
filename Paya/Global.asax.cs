using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using PayaBL.Classes;
using PayaBL.Common;

namespace Paya
{
    public class Global : HttpApplication
    {

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
           
            var portalSettings = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
            HttpContext context = HttpContext.Current;
            if (portalSettings != null)
            {
                string[] roles;
                string cookieName = !PortalSetting.SingleUserBase ? ("PayaPortal_" + portalSettings.portal.PortalAlias.ToLower()) : "PayaPortal";
                if (!context.Request.IsAuthenticated && (context.Request.Cookies[cookieName] != null))
                {
                    if (context.Request.Cookies[cookieName].Expires > DateTime.Now)
                    {
                        string userId = FormsAuthentication.Decrypt(Context.Request.Cookies[cookieName].Value).Name;
                        roles = (from o in UserRole.GetUserRoleByUserId(int.Parse(userId)) select o.RoleID.ToString()).ToArray<string>();
                        string roleStr = UserRole.GetUserRolesToString(int.Parse(userId));
                        FormsAuthentication.SetAuthCookie(userId, true);
                        var cTicket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddMinutes(90.0), false, roleStr);
                        context.User = new GenericPrincipal(new FormsIdentity(cTicket), roles);
                    }
                }
                else if (context.Request.IsAuthenticated)
                {
                    try
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Context.Request.Cookies[cookieName].Value);
                        roles = ticket.UserData.Split(new[] { ';' }).ToList().ToArray();
                        Context.User = new GenericPrincipal(new FormsIdentity(ticket), roles);
                    }
                    catch
                    {
                        SignOnController.SignOut();
                    }
                }
                if (!(!IsTelerikHandler() || context.Request.IsAuthenticated))
                {
                    Response.Redirect("~/AccessDenied.aspx");
                }
                if (!(!IsCuteEditor() || context.Request.IsAuthenticated))
                {
                    Response.Redirect("~/AccessDenied.aspx");
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
            CheckLicence();
            if ((PortalSetting.WhiteSpaceFilter && Request.Url.AbsolutePath.ToLower().EndsWith("Default.aspx")) || Request.Url.AbsolutePath.ToLower().EndsWith("PageManagment.aspx"))
            {
                Response.Filter = new WhiteSpaceFilter(Response.Filter);
            }
            if (IsPortalPage())
            {
                string portalAlias = PayaTools.UniqueId;
                bool saveCookie = false;
                bool refreshSite = false;
                if (Request.Cookies["PortalAlias"] != null)
                {
                    if (Request.Cookies["PortalAlias"].Value.ToUpper() != portalAlias.ToUpper())
                    {
                        refreshSite = true;
                        saveCookie = true;
                    }
                }
                else
                {
                    saveCookie = true;
                }
                try
                {
                    PortalSetting settings = new PortalSetting(portalAlias);
                    if (settings.portal.PortalAlias == null)
                    {
                        portalAlias = PortalSetting.DefaultPortal;
                        settings = new PortalSetting(portalAlias);
                        if (settings.portal.PortalAlias == null)
                        {
                            Response.Redirect("~/Errors/NoPortal.htm", true);
                        }
                        saveCookie = true;
                    }
                    portalAlias = settings.portal.PortalAlias;
                    Context.Items.Add("PortalSettings", settings);
                }
                catch (Exception)
                {
                    //Response.Redirect("~/Errors/PortalException.htm", true);
                }
                if (saveCookie)
                {
                    var httpCookie = Response.Cookies["PortalAlias"];
                    if (httpCookie != null) httpCookie.Path = "/";
                    var cookie = Response.Cookies["PortalAlias"];
                    if (cookie != null) cookie.Value = portalAlias;
                }
                if ((refreshSite && !PortalSetting.SingleUserBase) && 
                    ((Request.Cookies["refreshed"] == null) || ((Request.Cookies["refreshed"] != null) 
                    && (Response.Cookies["refreshed"].Value == "false"))))
                {
                    string rawUrl = HttpContext.Current.Request.RawUrl;
                    if (rawUrl.LastIndexOf("?") > 0)
                    {
                        rawUrl = rawUrl + "&init";
                    }
                    else
                    {
                        rawUrl = rawUrl + "?init";
                    }
                    Response.Cookies["refreshed"].Value = "true";
                    Response.Cookies["refreshed"].Path = "/";
                    Response.Cookies["refreshed"].Expires = DateTime.Now.AddMinutes(1.0);
                    FormsAuthentication.SignOut();
                    HttpCookie hck = HttpContext.Current.Response.Cookies["PayaPortal_" + portalAlias.ToLower()];
                    hck.Value = null;
                    hck.Expires = new DateTime(1999, 10, 12);
                    hck.Path = "/";
                    Response.Redirect(rawUrl);
                    
                }
                if (Request.Cookies["refreshed"] != null)
                {
                    Response.Cookies["refreshed"].Path = "/";
                    Response.Cookies["refreshed"].Value = "false";
                    Response.Cookies["refreshed"].Expires = DateTime.Now.AddMinutes(1.0);
                }
               

            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //TaskManager.Instance.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //LogException.HandleException(base.Server.GetLastError().GetBaseException());
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //TasksConfig.Init();
            //TaskManager.Instance.Initialize(TasksConfig.ScheduleTasks);
            //TaskManager.Instance.Start();
        }

        private static void CheckLicence()
        {
            string text1 = HttpContext.Current.Request.Url.Host.ToLower();
            string text2 = HttpContext.Current.Server.MachineName.ToLower().Trim();
            if (((text1 != text2) && (text1 != "localhost")) && (text1.IndexOf("cdesign.ir") == -1))
            {
                HttpContext.Current.Response.Redirect("~/Errors/licenseinvalid.htm", true);
            }
        }

        private static bool IsCuteEditor()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("cuteeditor/dialogs/");
        }

        private static bool IsPortalPage()
        {
            string strPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
            return (strPath.EndsWith(".aspx") &&
                    ((strPath.StartsWith("~/admin/") || strPath.StartsWith("~/modules/")) ||
                     (strPath.LastIndexOf("/") == 1)));
        }

        private static bool IsTelerikHandler()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower().EndsWith("telerik.web.ui.dialoghandler.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

    }
}
