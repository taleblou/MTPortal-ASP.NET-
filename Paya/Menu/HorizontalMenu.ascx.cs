using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;

namespace Paya.Menu
{
    public partial class HorizontalMenu : System.Web.UI.UserControl
    {
        // Fields
        protected Repeater _rpFooterMenu;

        // Methods
        protected void Page_Load(object sender, EventArgs e)
        {
           
            var lst =
                (from t in
                     Tab.GetTabsTree(PortalSetting.PortalId,
                                     Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).LanguageID)
                 where
                     ((t.ShowHorizontal && (t.IsReserved == Tab.ReservedType.NotReserved ||t.IsReserved != Tab.ReservedType.admintab )) &&
                      (t.Target != (decimal) Tab.TargetTypes.Empty)) && Role.IsInRoles(t.Roles)
                 orderby t.TabOrder
                 select
                     new
                         {
                             Name = t.TabName,
                             Url = PayaTools.BuildUrl("~/Default.aspx?TabId=" + t.TabID),
                             Target = setTarget(t.Target)
                         }).ToList();
            if (lst.Count != 0)
            {
                PayaTools.LoadCss(Enum.GetName(typeof(PayaTools.PortalCss), PayaTools.PortalCss.HeaderMenuCss), Page);
            }
            _rpFooterMenu.DataSource = lst;
            _rpFooterMenu.DataBind();
        }
        private string setTarget(sbyte tar)
        {
            switch (tar)
            {
                case (sbyte)Tab.TargetTypes.Blank:
                    return "_blank";
                    break;
                case (sbyte)Tab.TargetTypes.Self:
                    return "_Self";
                    break;
                case (sbyte)Tab.TargetTypes.Empty:
                    return "javascript:void(0);";
                    break;
            }
            return "";

        }
    }
}