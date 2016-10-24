using System;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using PayaBL.Classes;
using PayaBL.Control;

public class CachedModuleControl : Control
{
    // Fields
    private string _cachedOutput = "";

    // Methods
    protected override void CreateChildControls()
    {
        if (ModuleConfiguration.CacheTime > 0)
        {
            _cachedOutput = (string) Context.Cache[CacheKey];
        }
        if ((_cachedOutput == null) || Context.Request.IsAuthenticated)
        {
            base.CreateChildControls();
            var module = (ModuleControl) Page.LoadControl(ModuleConfiguration.Container);
            module.ModuleConfiguration = ModuleConfiguration;
            Controls.Add(module);
        }
    }

    protected override void Render(HtmlTextWriter output)
    {
        if ((ModuleConfiguration.CacheTime == 0) || Context.Request.IsAuthenticated)
        {
            base.Render(output);
        }
        else
        {
            if ((_cachedOutput == null) || Context.Request.IsAuthenticated)
            {
                TextWriter tempWriter = new StringWriter();
                base.Render(new HtmlTextWriter(tempWriter));
                _cachedOutput = tempWriter.ToString();
                var cacheTime = ModuleConfiguration.CacheTime;
                if (cacheTime != null)
                    Context.Cache.Insert(CacheKey, _cachedOutput, null,
                                         DateTime.Now.AddSeconds((double) cacheTime),
                                         TimeSpan.Zero);
            }
            output.Write(_cachedOutput);
        }
    }

    // Properties
    public string CacheKey
    {
        get
        {
            string roles = "";
            if (this.Context.Request.IsAuthenticated)
            {
                roles = roles + "_true_";
                try
                {
                    string cookieName = !PortalSetting.SingleUserBase
                                            ? ("PayaPortal_" + PortalSetting.PortalAlias.ToLower())
                                            : "PayaPortal";
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Context.Request.Cookies[cookieName].Value);
                    roles = roles + ticket.UserData.Replace(";", "_");
                }
                catch
                {
                    roles = roles ?? "";
                }
            }
            return string.Concat(new object[] { "CachedModule", ModuleConfiguration.ModuleID, "_", roles });
        }
    }

    public Module ModuleConfiguration { get; set; }
}


 
