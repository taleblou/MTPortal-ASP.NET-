using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using PayaBL.Classes;
using PayaBL.Common.PortalCach;

namespace PayaBL.Common
{
    public class PayaTools
    {
        //public static int QueryStringInt(string name, int defaultValue)
        //{
        //    string resultStr = QueryString(name).ToUpperInvariant();
        //    if (resultStr.Length > 0)
        //    {
        //        return int.Parse(resultStr);
        //    }
        //    return defaultValue;
        //}

        /// <summary>
        /// این متد یک یوزر کنترل را در یک پنل لود می کند
        ///  اگر  باید به یوزر کنترل پارامتری پاس داده شود آن را در ستینگ پاس می دهیم
        ///  دقت شود همه پارامتر ها رشته ای فرستاده می شود که در یوزر کنترل باید تبدیل شوند 
        /// اگر قرار است پارامتری فرستاده شود یوزر کنترل از کلاسی که خودش از یوزر کنترل به ارث رفته به ارث می رود 
        /// در آن کلاس پدر هر پارامتری که خواستیم تعریف می کنیم که نوع همه آنها رشته است 
        /// </summary>
        /// <param name="userCntrl">this (همین را به عنوان پارامتر می گذاریم)</param>
        /// <param name="panelName">پنلی که قرار است یوزر کنترل در آن لود شود</param>
        /// <param name="cntrlName">کنترلی که در صفحه لود می شود id</param>
        /// <param name="path">آدرس فیزیکی یوزر کنترل</param>
        /// <param name="settings">id,25;name,26 :پارامتر ها نحوه لود شدن مثال </param>
        /// <returns></returns>
        public static UserControl LoadCntrl(UserControl userCntrl, string panelName, string cntrlName, string path, string settings)
        {
            var pan = userCntrl.FindControl(panelName);
            if (pan != null)
            {

                var c = userCntrl.LoadControl(path);
                var type = c.GetType();
                c.ID = cntrlName;
                if (!string.IsNullOrEmpty(settings))
                {
                    var sets = settings.Split(';');
                    foreach (string s in sets)
                    {
                        string s1 = s.Split(',')[0];
                        type.GetProperties().Single(o => o.Name == s1).SetValue(c, s.Split(',')[1], null);
                    }
                }
                pan.Controls.Add(c);


                return (UserControl)c;
            }
            return null;
        }

        //public static string QueryString(string name)
        //{
        //    string result = string.Empty;
        //    if ((HttpContext.Current != null) && (HttpContext.Current.Request.QueryString[name] != null))
        //    {
        //        result = HttpContext.Current.Request.QueryString[name];
        //    }
        //    return result;
        //}

        //public static void RegisterCssInclude(Page page, string url)
        //{
        //    var stylesLink = new HtmlLink();
        //    stylesLink.Attributes["rel"] = "stylesheet";
        //    stylesLink.Attributes["type"] = "text/css";
        //    stylesLink.Href = url;
        //    page.Header.Controls.Add(stylesLink);
        //}

        #region helper
        // Fields
        private const string En = "en-us";
        private const string Fa = "fa-ir";

        // Methods
        public static void AccessDenied()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                // throw new HttpException(0x193, "Access Denied", 2);
            }
            HttpContext.Current.Response.Redirect(BuildUrl("~/AccessDenied.aspx"));
        }

        protected static string AddLink(string strmain, string caption, string url)
        {
            return ("<a href='" + url + "' target='_self' class='NavLink'>" + caption + "</a>&nbsp;>" + strmain);
        }

        public static string BuildUrl(string url)
        {
            string strRtrn = url;
             
            Regex oReg = new Regex(@"/Default\.aspx\?tabid=(\d+)&?(.*)", RegexOptions.IgnoreCase);
            if (oReg.Match(url).Success)
            {
                strRtrn = oReg.Replace(url, "/Default-$1.aja?$2");
            }
            //oReg = new Regex(@"/Default\.aspx\?tabid=(\d+)&?(.*)", RegexOptions.IgnoreCase);
            //else
            //{
            //    oReg = new Regex(@"/Default\.aspx", RegexOptions.IgnoreCase);
            //    if (oReg.Match(url).Success)
            //    {
            //        int tabid = PortalLanguage.GetLanguagePortalByCulture(CurrentCulture, PortalSetting.PortalId).HomeTabID;
            //        strRtrn = oReg.Replace(url, "/Default-" + tabid + ".aja");
            //    }
            //}
            oReg = new Regex(@"/PageManagment\.aspx\?ModuleID=(\d+)&Page=([^&]*)&?(.*)", RegexOptions.IgnoreCase);
            if (oReg.Match(url).Success)
            {
                strRtrn = oReg.Replace(url, "/Module/$2/PageManagment-$1.aspx?$3");
            }
            oReg = new Regex(@"/PageManagment\.aspx\?Page=(.*)&ModuleID=(\d+)&?(.*)", RegexOptions.IgnoreCase);
            if (oReg.Match(url).Success)
            {
                strRtrn = oReg.Replace(url, "/Module/$1/PageManagment-$2.aspx?$3");
            }
            if (strRtrn.EndsWith("?"))
            {
                strRtrn = strRtrn.Remove(strRtrn.LastIndexOf("?"));
            }
            if (url.StartsWith("~"))
            {
                return (PortalSetting.FullPortalPath + strRtrn.Substring(1));
            }
            return (PortalSetting.FullPortalPath + strRtrn);
        }

        public static string ConvertTo1256(string inputStr)
        {
            string strReturn = string.Empty;
            if (!string.IsNullOrEmpty(inputStr))
            {
                char[] c = inputStr.Replace("ک", "ك").ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    long cCode = c[i];
                    if (cCode == 0x6ccL)
                    {
                        c[i] = (char)int.Parse("1610");
                    }
                    strReturn = strReturn + c[i].ToString();
                }
            }
            return strReturn;
        }

        public static string ConvertToPersian(string inputStr)
        {
            string strReturn = string.Empty;
            if (!string.IsNullOrEmpty(inputStr))
            {
                char[] c = inputStr.Replace("ك", "ک").ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    var cCode = (long)c[i];
                    if (cCode == 0x64aL)
                    {
                        c[i] = (char)int.Parse("1740");
                    }
                    strReturn = strReturn + c[i].ToString();
                }
            }
            return strReturn;
        }

        public static bool CreateUoploadDirectoryForModule(int moduleDefId)
        {
            ModuleDef obj = ModuleDef.GetSingleByID(moduleDefId);
            string path = (obj == null) ? "" : obj.ModuleKey;
            return CreateUoploadDirectoryForModule(path);
        }

        public static bool CreateUoploadDirectoryForModule(string path)
        {
            try
            {
                int portalid = PortalSetting.SingleUserBase ? 0 : PortalSetting.PortalId;
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat(new object[] { "~/Upload/Portal", portalid, "/Modules/", path }))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat(new object[] { "~/Upload/Portal", portalid, "/Modules/", path })));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void FindAlias(HttpRequest request, ref string alias)
        {
            if (FindAliasFromQueryString(request.QueryString, ref alias))
            {
            }
        }

        public static bool FindAliasFromQueryString(NameValueCollection queryString, ref string alias)
        {
            if (queryString != null)
            {
                if (queryString["portalalias"] != null)
                {
                    string[] queryStringValues = queryString.GetValues("portalalias");
                    string queryStringValue = string.Empty;
                    if ((queryStringValues != null) && (queryStringValues.Length > 0))
                    {
                        queryStringValue = queryStringValues[0].Trim().ToLower(CultureInfo.InvariantCulture);
                    }
                    if (queryStringValue.Length != 0)
                    {
                        alias = queryStringValue;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public static string FormatNumber(string number)
        {
            return FormatNumber(number, 3);
        }

        public static string FormatNumber(string number, int groupLength)
        {
            if (string.IsNullOrEmpty(number))
            {
                return "";
            }
            number = number.Replace(",", "");
            int commas = (number.Length - 1) / groupLength;
            int lead = ((number.Length - 1) % groupLength) + 1;
            string formatted = number.Substring(0, lead);
            for (int i = 0; i < commas; i++)
            {
                formatted = formatted + "," + number.Substring((i * groupLength) + lead, groupLength);
            }
            return formatted;
        }

        public static string GeneratePrintScript(string title, string printArea)
        {
            var script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("function printPage()");
            script.Append("{");
            script.Append("\tvar da = (document.all) ? 1 : 0;");
            script.Append("\tvar pr = (window.print) ? 1 : 0;");
            script.Append("if(!pr)");
            script.Append("\t{");
            script.Append("\t\twindow.status = \"No print\";");
            script.Append("\t\treturn;");
            script.Append("\t}");
            script.Append("\tvar printArea = document.getElementById(\"" + printArea + "\");");
            script.Append("  if(printArea == null && da) ");
            script.Append("\t\tprintArea = document.all." + printArea + ";"); 
            script.Append("\t if(printArea) ");
            script.Append("\t{");
            script.Append("\t\tvar sStart = \"<html><head>\";");
            script.Append("\t\tvar w = window.open('about:blank','printWin','width=660,height=440,scrollbars=yes');");
            script.Append("\t\tvar wdoc = w.document;");
            script.Append("     wdoc.open();");
            script.Append("\t\twdoc.writeln( sStart+\"<link rel=\\\"stylesheet\\\" type=\\\"text/css\\\" href=\\\"" + SetStyle(HttpContext.Current.User.Identity.Name, true) + "/Style.css\\\">\");");
            script.Append("\t\twdoc.writeln(  \"<table dir=\\\"rtl\\\" border=0 class=\\\"MainBack\\\" width=\\\"100%\\\">\" );");
            script.Append("\t\twdoc.writeln(  \"<tr><td width=40>&nbsp;</td><td><br><br><br><br><br><br></td><td width=40>&nbsp;</td></tr>\" );");
            script.Append("\t\twdoc.writeln(  \"<tr><td></td><td><div id=\\\"PrintArea\\\">\" );");
            script.Append("\t\twdoc.writeln(  printArea.innerHTML );");
            script.Append("\t\twdoc.writeln(  \"</div></td><td></td></tr>\" );");
            script.Append("\t\twdoc.writeln(  \"<tr><td align='center'><br><br>چاپ از " + PortalSetting.PortalName + "<br>&copy; کلیه حقوق محفوظ است.<br><br><br></td><td></td><td></td></tr>\" );");
            script.Append("\t\twdoc.writeln(  \"</table>\" );");
            script.Append("\t\twdoc.writeln(  \"</body></html>\");");
            script.Append("\t\twdoc.close();");
            script.Append("\t\tw.print();");
            script.Append("\t}");
            script.Append("}");
            script.Append("</script>");
            return script.ToString();
        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                str = str + random.Next(10).ToString();
            }
            return str;
        }

        public static string GenerateStringWithDigit(int size)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                int num = random.Next(1, 4);
                if (num <= 2)
                {
                    int s = (num == 1) ? 0x41 : 0x61;
                    char ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26.0 * random.NextDouble()) + s)));
                    builder.Append(ch);
                }
                else
                {
                    builder.Append(random.Next(10).ToString());
                }
            }
            return builder.ToString().ToLower();
        }

        public static bool GetCookieBool(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true).ToUpperInvariant();
            return (((str1 == "TRUE") || (str1 == "YES")) || (str1 == "1"));
        }

        public static int GetCookieInt(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true);
            if (!string.IsNullOrEmpty(str1))
            {
                return Convert.ToInt32(str1);
            }
            return 0;
        }

        public static string GetCookieString(string cookieName, bool decode)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                return string.Empty;
            }
            try
            {
                string tmp = HttpContext.Current.Request.Cookies[cookieName].Value;
                if (decode)
                {
                    tmp = HttpContext.Current.Server.UrlDecode(tmp);
                }
                return tmp;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetCulture(int tabId, int moduleId)
        {
            try
            {
                if (tabId != -1)
                {
                    return Tab.GetSingleByID(tabId).Language.Culture;
                }
                //if (moduleId != -1)
                //{
                //    return Module.GetSingleByID(moduleId).ModuleTab.Lang.Culture;
                //}
                return PortalSetting.DefaultCulture;
            }
            catch
            {
                return PortalSetting.DefaultCulture;
            }
        }

        public static string GetFormString(string name)
        {
            string result = string.Empty;
            if ((HttpContext.Current != null) && (HttpContext.Current.Request[name] != null))
            {
                result = HttpContext.Current.Request[name];
            }
            return result;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            email = email.Trim();
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static void LoadCss(string cssName, Page page)
        {
            XDocument doc = Caching.GetCachedSettingsXml();
            try
            {
                var query = (from element in doc.Descendants("Setting")
                             where element.Attribute("id").Value == cssName
                             select new { id = element.Attribute("id"), value = element.Attribute("value") }).SingleOrDefault();
                if (query != null)
                {
                    string theme;
                    int tabId = QueryStringInt("TabID", -1);
                    if (tabId != -1)
                    {
                        theme = Tab.GetTabTheme(tabId);
                    }
                    else
                    {
                        theme = Tab.GetTabTheme(PortalLanguage.GetLanguagePortalByCulture(CurrentCulture, PortalSetting.PortalId).HomeTabID);
                    }
                    if (query.value.Value.ToLower().EndsWith(".css"))
                    {
                        string stylePath = SetStyle(page.User.Identity.Name, true) + "/Themes/" + theme + "/" + query.value.Value;
                        bool add = true;
                        foreach (var lnk in page.Header.Controls)
                        {
                            if ((lnk is HtmlLink) && (((HtmlLink)lnk).Href == stylePath))
                            {
                                add = false;
                            }
                        }
                        if (add)
                        {
                            RegisterCssInclude(page, stylePath);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void LoadOtherCss(Page page)
        {
            XDocument doc = Caching.GetCachedSettingsXml();
            try
            {
                var query = from element in doc.Descendants("Setting")
                            where
                                ((((element.Attribute("id").Value != "FooterCss") &&
                                   (element.Attribute("id").Value != "FooterMenuCss")) &&
                                  ((element.Attribute("id").Value != "HeaderCss") &&
                                   (element.Attribute("id").Value != "HeaderMenuCss"))) &&
                                 (element.Attribute("id").Value != "MainCss")) &&
                                (element.Attribute("id").Value != "MainMenuCss")

                            select new { id = element.Attribute("id"), value = element.Attribute("value") };
                if (query.Count() != 0)
                {
                    string theme;
                    int tabId = QueryStringInt("TabID", -1);
                    if (tabId != -1)
                    {
                        theme = Tab.GetTabTheme(tabId);
                    }
                    else
                    {
                        theme = Tab.GetTabTheme(PortalLanguage.GetLanguagePortalByCulture(CurrentCulture, PortalSetting.PortalId).HomeTabID);
                    }
                    foreach (var item in query)
                    {
                        if (item.value.Value.ToLower().EndsWith(".css"))
                        {
                            string stylePath = SetStyle(page.User.Identity.Name, true) + "/Themes/" + theme + "/" + item.value.Value;
                            bool add = true;
                            foreach (var lnk in page.Header.Controls)
                            {
                                if ((lnk is HtmlLink) && (((HtmlLink)lnk).Href == stylePath))
                                {
                                    add = false;
                                }
                            }
                            if (add)
                            {
                                RegisterCssInclude(page, stylePath);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static string ModifyQueryString(string url, string queryStringModification, string targetLocationModification)
        {
            if (url == null)
            {
                url = string.Empty;
            }
            url = url.ToLowerInvariant();
            if (queryStringModification == null)
            {
                queryStringModification = string.Empty;
            }
            queryStringModification = queryStringModification.ToLowerInvariant();
            if (targetLocationModification == null)
            {
                targetLocationModification = string.Empty;
            }
            targetLocationModification = targetLocationModification.ToLowerInvariant();
            string str = string.Empty;
            string str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#") + 1);
                url = url.Substring(0, url.IndexOf("#"));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    foreach (string str4 in queryStringModification.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            string[] strArray2 = str4.Split(new char[] { '=' });
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    StringBuilder builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(targetLocationModification))
            {
                str2 = targetLocationModification;
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
        }

        public static string PageNavigation(int moduleid, int tabId)
        {
            Tab t;
            string strReturn = string.Empty;
            string root = PortalSetting.FullPortalPath;
            if (moduleid != -1)
            {
                t = Module.GetSingleByID(moduleid).ModuleTab;
            }
            else
            {
                t = Tab.GetSingleByID(tabId);
            }
            while (t.TabID > 0)
            {
                strReturn = AddLink(strReturn, t.TabName, root + "/Default.aspx?tabid=" + t.TabID);
                if (t.ParentID == null)
                {
                    return strReturn;
                }
                t = Tab.GetSingleByID((int)t.ParentID);
            }
            return strReturn;
        }

        public static string QueryString(string name)
        {
            string result = string.Empty;
            if ((HttpContext.Current != null) && (HttpContext.Current.Request.QueryString[name] != null))
            {
                result = HttpContext.Current.Request.QueryString[name];
            }
            return result;
        }

        public static bool QueryStringBool(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            return (((resultStr == "YES") || (resultStr == "TRUE")) || (resultStr == "1"));
        }

        public static Guid? QueryStringGuid(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            Guid? result = null;
            try
            {
                result = new Guid(resultStr);
            }
            catch
            {
            }
            return result;
        }

        public static int QueryStringInt(string name)
        {
            int result;
            int.TryParse(QueryString(name).ToUpperInvariant(), out result);
            return result;
        }

        public static int QueryStringInt(string name, int defaultValue)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            if (resultStr.Length > 0)
            {
                return int.Parse(resultStr);
            }
            return defaultValue;
        }

        public static void RegisterCssBlock(Page page, string style)
        {

            var css = new HtmlGenericControl
                          {
                              TagName = "style"
                          };
            css.Attributes.Add("type", "text/css");
            css.InnerHtml = style;
            page.Header.Controls.Add(css);
            page.Header.Controls.Add(css);

        }

        public static void RegisterCssInclude(Page page, string url)
        {
            HtmlLink stylesLink = new HtmlLink();
            stylesLink.Attributes["rel"] = "stylesheet";
            stylesLink.Attributes["type"] = "text/css";
            stylesLink.Href = url;
            page.Header.Controls.Add(stylesLink);
        }

        public static void RegisterJavaScriptBlock(Page page, string name, string url, bool addScriptTag)
        {
            if (!page.ClientScript.IsClientScriptBlockRegistered(name))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), name, url, addScriptTag);
                var t = page.ClientScript.IsClientScriptIncludeRegistered(name);
            }
        }

        public static void RegisterJavaScriptInclude(Page page, string name, string url)
        {
            if (!page.ClientScript.IsClientScriptIncludeRegistered(name))
            {
                page.ClientScript.RegisterClientScriptInclude(page.GetType(), name, url);
                var t = page.ClientScript.IsClientScriptIncludeRegistered(name);
            }
        }

        public static string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
            {
                url = string.Empty;
            }
            url = url.ToLowerInvariant();
            if (queryString == null)
            {
                queryString = string.Empty;
            }
            queryString = queryString.ToLowerInvariant();
            string str = string.Empty;
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryString) && !string.IsNullOrEmpty(str))
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string str3 in str.Split(new char[] { '&' }))
                {
                    if (!string.IsNullOrEmpty(str3))
                    {
                        string[] strArray = str3.Split(new char[] { '=' });
                        if (strArray.Length == 2)
                        {
                            dictionary[strArray[0]] = strArray[1];
                        }
                        else
                        {
                            dictionary[str3] = null;
                        }
                    }
                }
                dictionary.Remove(queryString);
                StringBuilder builder = new StringBuilder();
                foreach (string str5 in dictionary.Keys)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("&");
                    }
                    builder.Append(str5);
                    if (dictionary[str5] != null)
                    {
                        builder.Append("=");
                        builder.Append(dictionary[str5]);
                    }
                }
                str = builder.ToString();
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
        }

        public static void SelectListItem(DropDownList list, object value)
        {
            if (list.Items.Count != 0)
            {
                ListItem selectedItem = list.SelectedItem;
                if (selectedItem != null)
                {
                    selectedItem.Selected = false;
                }
                if (value != null)
                {
                    selectedItem = list.Items.FindByValue(value.ToString());
                    if (selectedItem != null)
                    {
                        selectedItem.Selected = true;
                    }
                }
            }
        }

        public static bool SendEmail(MailMessage message)
        {
            try
            {
                new SmtpClient().Send(message);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SendEmail(string subject, string body, MailAddress from, MailAddress to)
        {
            return SendEmail(subject, body, from, to, new List<string>(), new List<string>());
        }

        public static bool SendEmail(string subject, string body, string from, string to)
        {
            return SendEmail(subject, body, new MailAddress(from), new MailAddress(to), new List<string>(), new List<string>());
        }

        public static bool SendEmail(string subject, string body, MailAddress from, MailAddress to, List<string> cc, List<string> bcc)
        {
            try
            {
                var message = new MailMessage
                {
                    From = from
                };

                message.To.Add(to);
                if (null != bcc)
                {
                    foreach (string address in bcc)
                    {
                        if ((address != null) && !string.IsNullOrEmpty(address.Trim()))
                        {
                            message.Bcc.Add(address.Trim());
                        }
                    }
                }
                if (null != cc)
                {
                    foreach (string address in cc)
                    {
                        if ((address != null) && !string.IsNullOrEmpty(address.Trim()))
                        {
                            message.CC.Add(address.Trim());
                        }
                    }
                }
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                
                var s = new SmtpClient();
                //s.EnableSsl = true
                s.Send(message);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SendEmail(string subject, string body, string from, string to, List<string> cc, List<string> bcc)
        {
            return SendEmail(subject, body, new MailAddress(from), new MailAddress(to), cc, bcc);
        }

        public static string ServerVariables(string name)
        {
            string tmpS = string.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[name] != null)
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[name];
                }
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }

        public static void SetCookie(string cookieName, string cookieValue, TimeSpan ts)
        {
            try
            {
                HttpCookie initLocal = new HttpCookie(cookieName)
                {
                    Value = HttpContext.Current.Server.UrlEncode(cookieValue)
                };
                HttpCookie cookie = initLocal;
                cookie.Expires = DateTime.Now.Add(ts);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        public static string SetDateTimeWithCulture(DateTime dt)
        {
            switch (Language.GetSingleLangaugeByCultureName(CurrentCulture).CalendarLanguage)
            {
                case "PersianCalendar":
                    return (new HejriShamsiDateTime(dt).Date + " " + dt.ToShortTimeString());

                case "HijriCalendar":
                    return (new HejriGhamariDateTime(dt).Date + " " + dt.ToShortTimeString());

                case "GregorianCalendar":
                    return (dt.ToShortDateString() + " " + dt.ToShortTimeString());
            }
            return "";
        }

        public static string SetDateWithCulture(DateTime dt)
        {
            switch (Language.GetSingleLangaugeByCultureName(CurrentCulture).CalendarLanguage)
            {
                case "PersianCalendar":
                    return new HejriShamsiDateTime(dt).Date;

                case "HijriCalendar":
                    return new HejriGhamariDateTime(dt).Date;

                case "GregorianCalendar":
                    return dt.ToShortDateString();
            }
            return "";
        }

        public static void SetLanguage(int tabId, int moduleId)
        {
            string cultureCode = GetCulture(tabId, moduleId).Trim();
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureCode);
            }
            catch (Exception)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(PortalSetting.DefaultCulture);
            }
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        public static void SetMetaHttpEquiv(Page page, string httpEquiv, string content)
        {
            if (page.Header != null)
            {
                HtmlMeta meta = new HtmlMeta();
                if (page.Header.FindControl("meta" + httpEquiv) != null)
                {
                    meta = (HtmlMeta)page.Header.FindControl("meta" + httpEquiv);
                    meta.Content = content;
                }
                else
                {
                    meta.ID = "meta" + httpEquiv;
                    meta.HttpEquiv = httpEquiv;
                    meta.Content = content;
                    page.Header.Controls.Add(meta);
                }
            }
        }

        public static string SetStyle(string userid, bool withDomain)
        {
            string style;
            string r = string.Empty;
            if (userid.Trim().Length > 0)
            {
                r = PortalUser.GetSingleByID(int.Parse(userid)).UserStyle;
            }
            if (string.IsNullOrEmpty(r))
            {
                r = PortalSetting.DefaultStyle;
            }
            if (withDomain)
            {
                style = PortalSetting.PortalPath + "/UI/Styles/" + r;
            }
            else
            {
                style = "~/UI/Styles/" + r;
            }
            if (!IsPersian)
            {
                string t = style;
                style = t + "/" + r + "-" + CurrentCulture;
            }
            return style;
        }

        public static string ShowDate(string datepass)
        {
            return ("<span dir='ltr'>" + datepass + "</span>");
        }

        // Properties
        public static string CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.Name;
            }
        }

        public static bool IsEnglish
        {
            get
            {
                return (CurrentCulture.ToLower() == "en-us");
            }
        }

        public static bool IsPersian
        {
            get
            {
                return (CurrentCulture.ToLower() == "fa-ir");
            }
        }

        public static string UniqueId
        {
            get
            {
                string uniquePortalId = PortalSetting.DefaultPortal;
                FindAlias(HttpContext.Current.Request, ref uniquePortalId);
                HttpContext.Current.Items["PortalAlias"] = uniquePortalId;
                return uniquePortalId;
            }
        }

        // Nested Types
        public enum PortalCss
        {
            MainCss,
            MainMenuCss,
            HeaderMenuCss,
            HeaderCss,
            FooterMenuCss,
            FooterCss
        }

        #endregion




    }
    public class CntrlControl
    {

    }
}
