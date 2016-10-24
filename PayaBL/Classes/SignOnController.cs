using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using PayaBL.Common;

namespace PayaBL.Classes
{
    public class SignOnController
    {
        // Methods
        public static void ExtendCookie(Portal portalSettings)
        {
            const int minuteAdd = 90;
            ExtendCookie(portalSettings, minuteAdd);
        }

        public static void ExtendCookie(Portal portalSetting, int minuteAdd)
        {
            var time = DateTime.Now;
            var span = new TimeSpan(0, 0, minuteAdd, 0, 0);
            string cookieName = !PortalSetting.SingleUserBase ? ("PayaPortal_" + PortalSetting.PortalAlias.ToLower()) : "PayaPortal";
            HttpContext.Current.Response.Cookies[cookieName].Expires = time.Add(span);
        }

        public static void KillSession()
        {
            SignOut(PayaTools.BuildUrl("~/Admin/Logon.aspx"), true);
        }

        public static PortalUser SignOn(string user, string password, bool persistent)
        {
            return SignOn(user, password, persistent, null);
        }

        public static PortalUser SignOn(string login, string password, bool persistent, string redirectPage)
        {
            PortalUser user = PortalUser.Login(login,
                                               FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5"),
                                               PortalSetting.PortalId);
            if ((user != null) && (user.UserID != -1) && (user.UserID != 0) &&(user.UserID != -2) && (user.UserID != -3))
            {
                if (PortalSetting.Monitoring)
                {
                    try
                    {
                        //Monitoring.AddMonitoring(new int?(user.UserId), -1, "Logon");
                    }
                    catch (Exception ex)
                    {
                        //LogException.HandleException(ex);
                    }
                }
                FormsAuthentication.SetAuthCookie(user.UserID.ToString(), persistent);
                var cookieName = !PortalSetting.SingleUserBase ? ("PayaPortal_" + PortalSetting.PortalAlias.ToLower()) : "PayaPortal";
                var hck = HttpContext.Current.Response.Cookies[cookieName];
                hck.Value = user.UserID.ToString();
                hck.Path = "/";
                if (persistent)
                {
                    hck.Expires = DateTime.Now.AddYears(50);
                }
                else
                {
                    var time = DateTime.Now;
                    var span = new TimeSpan(0, 0, 90, 0, 0);
                    hck.Expires = time.Add(span);
                }
                string roleStr = UserRole.GetUserRolesToString(user.UserID);
                string[] roles = (from t in UserRole.GetUserRoleByUserId(user.UserID) select t.RoleID.ToString()).ToArray<string>();
                var cTicket = new FormsAuthenticationTicket(1, user.UserID.ToString(), DateTime.Now,
                                                            DateTime.Now.AddMinutes(90.0), false, roleStr);
                hck.Value = FormsAuthentication.Encrypt(cTicket);
                HttpContext httpContext = HttpContext.Current;
                httpContext.User = new GenericPrincipal(new FormsIdentity(cTicket), roles);
                if (string.IsNullOrEmpty(redirectPage))
                {
                    if (httpContext.Request.UrlReferrer != null)
                    {
                        httpContext.Response.Redirect(httpContext.Request.UrlReferrer.ToString());
                    }
                    else
                    {
                        httpContext.Response.Redirect(PortalSetting.PortalPath);
                    }
                }
                HttpContext.Current.Response.Redirect(redirectPage);
            }
            return user;
        }

        public static void SignOut()
        {
            SignOut(PayaTools.BuildUrl("~/Default.aspx"), true);
        }

        public static void SignOut(string urlToRedirect, bool removeLogin)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext.User != null)
            {
                // Monitoring.AddMonitoring(new int?(int.Parse(httpContext.User.Identity.Name)), -1, "LogOut");
            }
            FormsAuthentication.SignOut();
            if (removeLogin)
            {
                if (PortalSetting.SingleUserBase)
                {
                    HttpCookie cooky = httpContext.Response.Cookies["PayaPortal"];
                    if (cooky != null)
                    {
                        cooky.Value = null;
                        cooky.Expires = new DateTime(0x7cf, 10, 12);
                        cooky.Path = "/";
                    }
                }
                else
                {
                    HttpCookie xhck = httpContext.Response.Cookies["PayaPortal_" + PortalSetting.PortalAlias.ToLower()];
                    if (xhck != null)
                    {
                        xhck.Value = null;
                        xhck.Expires = new DateTime(0x7cf, 10, 12);
                        xhck.Path = "/";
                    }
                }
            }
            if (urlToRedirect.Length > 0)
            {
                httpContext.Response.Redirect(urlToRedirect);
            }
        }
    }
    
}
