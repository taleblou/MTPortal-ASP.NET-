using System.Web;
using System.Web.UI;
using PayaBL.Classes;

namespace PayaBL.Control
{
    #region Class ModuleControl:

    public class ModuleControl : UserControl
    {
        public static int? CurrentUserId
        {
            get
            {
                if (HttpContext.Current.User.Identity.Name != "")
                {
                    return int.Parse(HttpContext.Current.User.Identity.Name);
                }
                return null;
            }
        }
        public static PortalUser CurrentUser
        {
            get
            {
                return CurrentUserId.HasValue ? PortalUser.GetSingleByID(CurrentUserId.Value) : null;
            }
        }

        public Module ModuleConfiguration { set; get; }

        //public string ModulePath
        //{
        //    get
        //    {
        //        return
        //            ModuleConfiguration.ModuleDef.ModuleDefUrl.Remove(
        //                ModuleConfiguration.TModuleDef.ModuleDefUrl.LastIndexOf("/"));
        //    }
        //}




    }

    #endregion
}
