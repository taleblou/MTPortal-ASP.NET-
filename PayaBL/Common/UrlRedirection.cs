using System.Collections.Generic;
using System.Text.RegularExpressions;
using PayaBL.Classes;

namespace PayaBL.Common
{
    public class UrlRedirection
    {
        // Fields
        private List<RedirectRule> _rules;

        // Methods
        public string GetMatchingRewrite(string url)
        {
            string strRtrn = "";
            foreach (RedirectRule oRule in this.Rules)
            {
                Regex oReg = new Regex(oRule.Url, RegexOptions.IgnoreCase);
                if (oReg.Match(url).Success)
                {
                    strRtrn = oReg.Replace(url, oRule.Rewrite);
                }
            }
            return strRtrn;
        }

        private static List<RedirectRule> GetRules()
        {
            List<RedirectRule> col = new List<RedirectRule>();
            List<Portal> portals = Portal.GetAll();
            for (int i = 0; i < portals.Count; i++)
            {
                Portal item = portals[i];
                if (item.PortalPath != "")
                {
                    RedirectRule initLocal0 = new RedirectRule
                    {
                        Name = item.PortalName,
                        Url = "~/(" + item.PortalAlias + @")/(.*)\.aspx",
                        Rewrite = "~/$2.aspx?portalalias=$1"
                    };
                    RedirectRule oR = initLocal0;
                    col.Add(oR);
                }
            }
            col.Add(test);
            return col;
        }

        // Properties
        private List<RedirectRule> Rules
        {
            get
            {
                return (this._rules ?? (this._rules = GetRules()));
            }
        }

        internal static RedirectRule test
        {
            get
            {
                return new RedirectRule { Name = "TabIdRule", Rewrite = "/Default.aspx?TabId=$1&$3", Url = @"/Default-(\d+).aspx(\??(.*))" };
            }
        }
    }
    
}
