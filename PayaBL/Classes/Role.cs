using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayaBL.Common;
using PayaDB;

namespace PayaBL.Classes
{
    #region Class Role:

    /// <summary>
    /// 
    /// </summary>
    public class Role
    {
        #region Fields :
        #endregion

        #region Properties :

        #region Base:

        public int RoleID { set; get; }

        public int? PortalID { set; get; }

        public string RoleKey { set; get; }

        public string RoleName
        {
            get
            {
                return LocalResource.GetResourceValue(this.RoleKey, PortalLanguage.GetLanguagePortalByCulture(PayaTools.CurrentCulture, PortalSetting.PortalId).LanguageID);
            }
        }

        #endregion

        #region Related:

        public TPortal TPortal { set; get; }

        public IList<TUser> TUserRole = new List<TUser>();

        public IList<TPortal> RoleInPortal = new List<TPortal>();

        #endregion

        #endregion

        #region Constrauctors :
        public Role()
        {
        }

        public Role(int roleID, int? portalID, string roleKey)
        {
            RoleID = roleID;
            PortalID = portalID;
            RoleKey = roleKey;
           

        }
        #endregion

        #region Methods :

        #region Instance Methods :
        public int Add()
        {
            return Role.Add(PortalID, RoleKey);
        }

        public bool Update()
        {
            return Role.Update(RoleID, PortalID, RoleKey);
        }

        public bool Delete()
        {
            return Role.Delete(RoleID);
        }
        #endregion

        #region Static Methods :

        public static int Add(int? portalID, string roleKey)
        {
            return TRole.Add(portalID, roleKey);
        }

        public static bool Update(int roleID, int? portalID, string roleKey)
        {
            return TRole.Update(roleID, portalID, roleKey);
        }

        public static bool Delete(int id)
        {
            return TRole.Delete(id);
        }

        public static List<Role> GetAll(int portalId)
        {
            var t = GetCollectionObjectFromDbCollectionObject(TRole.GetAll(portalId));
            t.Add(new Role(15, null, "AllUser"));
            t.Add(new Role(13, null, "AuthUser"));
            t.Add(new Role(14, null, "UnAuthUser"));
            return t;
        }

        public static List<Role> GetAll(string key, int portalId)
        {
            var t =
                GetCollectionObjectFromDbCollectionObject(TRole.GetAll(portalId)).Where(o => o.RoleKey == key).ToList();
            t.Add(new Role(15, null, "AllUser"));
            t.Add(new Role(13, null, "AuthUser"));
            t.Add(new Role(14, null, "UnAuthUser"));
            return t;
        }

        public static Role GetSingleByID(int id, int? portalId)
        {
            return GetObjectFromDbObject(TRole.GetSingleByID(id,portalId));
        }

        public static Role GetObjectFromDbObject(TRole role)
        {
            return new Role(role.RoleID, role.PortalID, role.RoleKey);
        }

        public static List<Role> GetCollectionObjectFromDbCollectionObject(IEnumerable<TRole> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        /// <summary>
        /// اول نگاه می کنیم که یوزر کنونی چه رولی دارد بعد نگاه می کند که در رشته ی رول ها هست یا نه
        /// </summary>
        /// <param name="roles">یک رشته از رول ها را به این متد پاس می دهیم </param>
        /// <returns></returns>
        public static bool IsInRoles(string roles)
        {
            HttpContext context = HttpContext.Current;
            return (!string.IsNullOrEmpty(roles.Trim()) &&
                    roles.Split(new[] { ';' }).Any(role => (!string.IsNullOrEmpty(role) &&
                                                         ((((role == string.Format("{0}", 15)) ||
                                                            (context.Request.IsAuthenticated && (role == string.Format("{0}", 13)))) ||
                                                           (!context.Request.IsAuthenticated && (role == string.Format("{0}", 14)))) ||
                                                          context.User.IsInRole(role)))));
        }

        public static IEnumerable<Role> GetTabRolesByTabId(int tabId)
        {
            return GetCollectionObjectFromDbCollectionObject(TRole.GetTabRolesByTabId(tabId));
        }

        public static Role GetSingleById(int roleId)
        {
            return GetObjectFromDbObject(TRole.GetSingleById(roleId));
        }

        public static List<Role> GetRolesOfModule(int moduleID, int authId)
        {
            return GetCollectionObjectFromDbCollectionObject(TRole.GetRolesOfModule(moduleID, authId));
        }

        public static List<Role> GetRolesAllOfModule(int moduleID)
        {
            return GetCollectionObjectFromDbCollectionObject(TRole.GetRolesAllOfModule(moduleID));
        }

        ///////////////
        //توابع اضافه شده
        /////////////

        public static bool IsInRole(Role role)
        {
            HttpContext context = HttpContext.Current;
            return ((((role.RoleID == 15) || (context.Request.IsAuthenticated && (role.RoleID == 13))) || (!context.Request.IsAuthenticated && (role.RoleID == 14))) || context.User.IsInRole(role.RoleID.ToString()));
        }

        public static bool IsInRole(string role)
        {
            HttpContext context = HttpContext.Current;
            return (!string.IsNullOrEmpty(role.Trim()) && ((((role == string.Format("{0}", 15)) || (context.Request.IsAuthenticated && (role == string.Format("{0}", 13)))) || (!context.Request.IsAuthenticated && (role == string.Format("{0}", 14)))) || context.User.IsInRole(role)));
        }

        public static bool IsInRoles(List<Role> role)
        {
            HttpContext context = HttpContext.Current;
            return role.Any(o => ((((o.RoleID == 15) || (context.Request.IsAuthenticated && (o.RoleID == 13))) || (!context.Request.IsAuthenticated && (o.RoleID == 14))) || context.User.IsInRole(o.RoleID.ToString())));
        }

        public static List<Role> GetBelongaleRoles(int userId, int portalId)
        {
            portalId = PortalSetting.SingleUserBase ? 0 : portalId;
            return GetCollectionObjectFromDbCollectionObject(TRole.GetBelongaleRoles(userId, portalId));
        }

        public static List<Role> GetBelongRolesTabs(int tabId, int searchId)
        {
            return GetCollectionObjectFromDbCollectionObject(TRole.GetBelongRolesTabs(tabId, searchId));
        }

        #endregion

        #endregion

    }

    #endregion
}
