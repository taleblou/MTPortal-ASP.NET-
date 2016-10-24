using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace PayaBL.Common
{
    public class UrlRewriteModule : IHttpModule
    {
        // Fields
        private const string OriginalRequestUrlTag = "PayaPortalOriginalRequestUrl";

        // Methods
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Rewrite_BeginRequest;
            context.PreRequestHandlerExecute += Rewrite_PreRequestHandlerExecute;
        }

        private void Page_PagePreInit(object sender, EventArgs e)
        {
            if (HttpContext.Current.Items.Contains("PayaPortalOriginalRequestUrl"))
            {
                string path = HttpContext.Current.Items["PayaPortalOriginalRequestUrl"] as string;
                if (path != null)
                {
                    if (path.IndexOf("?") == -1)
                    {
                        path = path + "?";
                    }
                    HttpContext.Current.RewritePath(path);
                }
            }
        }

        public void Rewrite_BeginRequest(object sender, EventArgs args)
        {
            string portalpath = ConfigurationManager.AppSettings["PortalPath"].ToLower();
            if (!HttpContext.Current.Request.Url.AbsoluteUri.ToLower().StartsWith(portalpath.ToLower()))
            {
                HttpContext.Current.Response.Redirect(portalpath +
                                                      HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.
                                                          Remove(0, 1) + HttpContext.Current.Request.Url.Query);
            }
            string strPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
            if (strPath.EndsWith(".aspx"))
            {
                int i;
                string name;
                var urlRedirection = new UrlRedirection();
                var strUrl = strPath;
                var strRewrite = urlRedirection.GetMatchingRewrite(strPath);
                if (!string.IsNullOrEmpty(strRewrite))
                {
                    strUrl = strRewrite;
                }
                if (strUrl.IndexOf("?") > -1)
                {
                    for (i = 0; i < HttpContext.Current.Request.QueryString.AllKeys.Length; i++)
                    {
                        if ((HttpContext.Current.Request.QueryString.AllKeys[i] != null) &&
                            (HttpContext.Current.Request.QueryString.AllKeys[i].ToLower() != "portalalias"))
                        {
                            name = strUrl;
                            strUrl = name + "&" + HttpContext.Current.Request.QueryString.AllKeys[i] + "=" +
                                     HttpContext.Current.Request.QueryString[i];
                        }
                    }
                }
                else
                {
                    for (i = 0; i < HttpContext.Current.Request.QueryString.AllKeys.Length; i++)
                    {
                        if ((HttpContext.Current.Request.QueryString.AllKeys[i] != null) &&
                            (HttpContext.Current.Request.QueryString.AllKeys[i].ToLower() != "portalalias"))
                        {
                            if (strUrl.IndexOf("?") > -1)
                            {
                                name = strUrl;
                                strUrl = name + "&" + HttpContext.Current.Request.QueryString.AllKeys[i] + "=" +
                                         HttpContext.Current.Request.QueryString[i];
                            }
                            else
                            {
                                name = strUrl;
                                strUrl = name + "?" + HttpContext.Current.Request.QueryString.Keys[i] + "=" +
                                         HttpContext.Current.Request.QueryString[i];
                            }
                        }
                    }
                }
                HttpContext.Current.Items["PayaPortalOriginalRequestUrl"] = HttpContext.Current.Request.RawUrl;
                HttpContext.Current.RewritePath(strUrl);
            }
        }

        private void Rewrite_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            if ((application != null) && (application.Context.CurrentHandler is Page))
            {
                var page = application.Context.CurrentHandler as Page;
                page.PreInit += Page_PagePreInit;
            }
        }
    }
    
}
